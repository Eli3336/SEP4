import React, { useState, useEffect } from "react";
import SensorInfo from "./SensorInfo";
import {
  fetchRoomDetailsById,
  fetchSensorDataByRoomId,
  updateSensor,
} from "@/api";

const RoomInfo = ({ roomId, onClose }) => {
  const [roomData, setRoomData] = useState(null);
  const [showSensorLog, setShowSensorLog] = useState(false);
  const [selectedSensor, setSelectedSensor] = useState(null);
  const [upbreakpoint, setUpbreakpoint] = useState("");
  const [downbreakpoint, setDownbreakpoint] = useState("");
  const [updateMessage, setUpdateMessage] = useState("");
  const [showUpdateForm, setShowUpdateForm] = useState(false);

  const fetchRoomData = async () => {
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
  };

  useEffect(() => {
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

  const handleSelectSensor = (sensor) => {
    setSelectedSensor(sensor);
    setUpbreakpoint("");
    setDownbreakpoint("");
    setShowUpdateForm(true);
  };

  const handleUpdateSensor = async () => {
    try {
      if (selectedSensor && upbreakpoint !== "" && downbreakpoint !== "") {
        let sensorId;
        switch (selectedSensor.type) {
          case "Temperature":
            sensorId = 1;
            break;
          case "Humidity":
            sensorId = 2;
            break;
          case "CO2":
            sensorId = 3;
            break;
          default:
            break;
        }

        if (sensorId) {
          await updateSensor(sensorId, upbreakpoint, downbreakpoint);
          setUpdateMessage("Sensor updated successfully");
          fetchRoomData();
        }
      }
    } catch (error) {
      console.error("Error updating sensor data:", error);
      setUpdateMessage("Error updating sensor");
    }
  };

  const handleUpdateFormClose = () => {
    setShowUpdateForm(false);
  };

  return (
    roomData && (
      <div
        className="fixed top-0 left-0 w-full h-full bg-black bg-opacity-50 z-50 flex justify-center items-center"
        onClick={handleClose}
      >
        <div
          className="w-80% max-h-80% bg-gray-200 rounded-lg p-8 flex flex-row overflow-y-auto"
          onClick={(e) => {
            e.stopPropagation();
          }}
        >
          <div className="flex-1 pr-8">
            <div className="font-bold text-xl">{roomData.name}</div>
            <p>Capacity: {roomData.capacity}</p>
            <p>Availability: {roomData.availability}</p>
            <h4 className="text-xl font-bold">Sensor Data:</h4>
            {roomData.sensors &&
              roomData.sensors.map((sensor) => (
                <div key={sensor.id}>
                  <p>
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
                  <button
                    onClick={() => handleSelectSensor(sensor)}
                    className="bg-green-500 text-white text-center text-sm px-4 py-2 rounded-md mt-2"
                  >
                    Update {sensor.type} Sensor
                  </button>
                </div>
              ))}
            <h4 className="text-xl font-bold">Patients:</h4>
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
          <div className="flex-1 pl-8">
            <button
              onClick={handleToggleSensorLog}
              className="bg-green-500 text-white text-center text-sm px-4 py-2 rounded-md mt-2"
            >
              {showSensorLog ? "Hide Sensor Logs" : "Show Sensor Logs"}
            </button>
            {showSensorLog && <SensorInfo sensors={roomData.sensors} />}
          </div>
        </div>
        {selectedSensor && showUpdateForm && (
          <div
            className="fixed top-0 left-0 w-full h-full bg-black bg-opacity-50 z-50 flex justify-center items-center"
            onClick={(e) => e.stopPropagation()}
          >
            <div className="w-80% max-h-80% bg-white rounded-lg p-8 flex flex-col relative">
              <button
                onClick={handleUpdateFormClose}
                className="absolute top-2 right-2 text-gray-500 text-xl"
                style={{ cursor: "pointer" }}
              >
                &times;
              </button>
              <h2 className="font-bold text-xl mb-4">
                Update {selectedSensor.type} Sensor
              </h2>
              <div className="flex flex-col items-center">
                <input
                  type="text"
                  placeholder={`${selectedSensor.type} upbreakpoint`}
                  value={upbreakpoint}
                  onChange={(e) => setUpbreakpoint(e.target.value)}
                  className="border border-gray-400 rounded-md p-2 mb-4 bg-white text-black"
                />
                <input
                  type="text"
                  placeholder={`${selectedSensor.type} downbreakpoint`}
                  value={downbreakpoint}
                  onChange={(e) => setDownbreakpoint(e.target.value)}
                  className="border border-gray-400 rounded-md p-2 mb-4 bg-white text-black"
                />
                <button
                  onClick={handleUpdateSensor}
                  className="bg-green-500 text-white text-center text-sm px-4 py-2 rounded-md"
                >
                  Update
                </button>
                {updateMessage && (
                  <p className="text-sm mt-2">{updateMessage}</p>
                )}
              </div>
            </div>
          </div>
        )}
      </div>
    )
  );
};

export default RoomInfo;
