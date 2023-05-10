import React, { useState, useEffect } from "react";
import styles from "../styles/RoomInfo.module.css";
import SensorInfo from "./SensorInfo";
import { fetchRoomDetailsById } from "@/api";
import { fetchSensorDataByRoomId } from "@/api";

const RoomInfo = ({ roomId, onClose }) => {
  const [roomData, setRoomData] = useState(null);
  const [showSensorLog, setShowSensorLog] = useState(false);

  useEffect(() => {
    async function fetchRoomData() {
      try {
        const [roomData, sensorsData] = await Promise.all([
          fetchRoomDetailsById(roomId),
          fetchSensorDataByRoomId(roomId),
        ]);

        const sensorsWithValues = roomData.sensors.map((sensor, index) => {
          return {
            ...sensor,
            values: [{ value: sensorsData[index]?.value ?? null }],
          };
        });

        const updatedRoomData = { ...roomData, sensors: sensorsWithValues };
        setRoomData(updatedRoomData);
        console.log("Room data:", updatedRoomData);
      } catch (error) {
        console.error("Error fetching room data:", error);
      }
    }

    if (roomId) {
      fetchRoomData();
    }
  }, [roomId]);

  const handleClose = () => {
    onClose();
  };

  const handleToggleSensorLog = () => {
    setShowSensorLog(!showSensorLog);
  };

  return (
    roomData && (
      <div className={styles.container} onClick={handleClose}>
        <div
          className={styles.modal}
          onClick={(e) => {
            e.stopPropagation();
          }}
        >
          <div className={styles.leftSection}>
            <div className={styles.content}>
              <h2>{roomData.name}</h2>
              <p>Capacity: {roomData.capacity}</p>
              <p>Availability: {roomData.availability}</p>
              <h4>Sensor Data:</h4>
              {roomData.sensors &&
                roomData.sensors.map((sensor) => (
                  <p key={sensor.id}>
                    {sensor.type}:{" "}
                    {sensor.values && sensor.values.length > 0
                      ? sensor.values[sensor.values.length - 1].value
                      : "N/A"}
                    {sensor.type === "Temperature"
                      ? "°C"
                      : sensor.type === "Humidity"
                      ? "%"
                      : "ppm"}
                  </p>
                ))}
              <h4>Patients:</h4>
              <ul>
                {roomData.patients && roomData.patients.length === 0 ? (
                  <li>No patients assigned</li>
                ) : (
                  roomData.patients &&
                  roomData.patients.map((patient, index) => (
                    <li key={index}>{patient.name}</li>
                  ))
                )}
              </ul>
            </div>
          </div>
          <div className={styles.rightSection}>
            <button
              onClick={handleToggleSensorLog}
              className={styles.sensorLogButton}
            >
              {showSensorLog ? "Hide Sensor Logs" : "Show Sensor Logs"}
            </button>
            {showSensorLog && <SensorInfo sensors={roomData.sensors} />}
          </div>
        </div>
      </div>
    )
  );
};

export default RoomInfo;
