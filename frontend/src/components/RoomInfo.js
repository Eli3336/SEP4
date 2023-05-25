import React, { useState, useEffect } from "react";
import styles from "../styles/RoomInfo.module.css";
import SensorInfo from "./SensorInfo";
import {
  fetchRoomDetailsById,
  fetchSensorDataByRoomId,
  updateSensor, // Import updateSensor method here
} from "@/api";

const RoomInfo = ({ roomId, onClose }) => {
  const [roomData, setRoomData] = useState(null);
  const [showSensorLog, setShowSensorLog] = useState(false);
  const [humidity, setHumidity] = useState("");
  const [temperature, setTemperature] = useState("");
  const [co2, setCo2] = useState("");

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

  const handleUpdateSensors = async () => {
    try {
      // Update each sensor value that has an entered value
      if (roomData && roomData.sensors) {
        for (let sensor of roomData.sensors) {
          let sensorValue;
          switch (sensor.type) {
            case "Humidity":
              sensorValue = humidity;
              break;
            case "Temperature":
              sensorValue = temperature;
              break;
            case "CO2":
              sensorValue = co2;
              break;
            default:
              break;
          }

          if (sensorValue) {
            // Use updateSensor method from API here
            await updateSensor(sensor.id, sensorValue, sensorValue);
          }
        }
      }

      // Re-fetch room data after updating sensors
      fetchRoomData();
    } catch (error) {
      console.error("Error updating sensor data:", error);
    }
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
              <div>
                <input
                  type="text"
                  placeholder="New humidity value"
                  onChange={(e) => setHumidity(e.target.value)}
                />
                <input
                  type="text"
                  placeholder="New temperature value"
                  onChange={(e) => setTemperature(e.target.value)}
                />
                <input
                  type="text"
                  placeholder="New CO2 value"
                  onChange={(e) => setCo2(e.target.value)}
                />
                <button onClick={handleUpdateSensors}>Update Sensors</button>
              </div>
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
