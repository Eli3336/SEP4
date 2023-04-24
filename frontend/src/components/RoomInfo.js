import { useState } from "react";
import styles from "@/styles/roominfo.module.css";
import SensorInfo from "./SensorInfo";

const RoomInfo = ({ roomData, onClose }) => {
  const [show, setShow] = useState(true);
  const sensorData = [
    roomData.timestamp,
    roomData.temperature,
    roomData.humidity,
    roomData.co2,
  ];
  const handleClose = () => {
    setShow(false);
    onClose();
  };

  // Debugging
  console.log("Room data:", roomData);

  return (
    show &&
    roomData && ( // Add this check
      <div className={styles.container} onClick={handleClose}>
        <div
          className={styles.modal}
          onClick={(e) => {
            e.stopPropagation();
          }}
        >
          <div className={styles.header}>
            <h3>Room Information</h3>
            <button className={styles.close} onClick={handleClose}>
              X
            </button>
          </div>
          <div className={styles.content}>
            <tbody>
              <tr>
                <th>Time</th>
                <th>Temperature</th>
                <th>Humidity</th>
                <th>CO2</th>
              </tr>
              {sensorData.map((sensor, i) => (
                <tr key={i}>
                  <td>{sensor.timestamp}</td>
                  <td>{sensor.temperature}°C</td>
                  <td>{sensor.humidity}%</td>
                  <td>{sensor.co2} ppm</td>
                </tr>
              ))}
            </tbody>
            <h4>Patients:</h4>
            <ul>
              {roomData.patients.length === 0 ? (
                <li>No patients assigned</li>
              ) : (
                roomData.patients.map((patient, index) => (
                  <li key={index}>{patient}</li>
                ))
              )}
            </ul>
            {/* Add the SensorInfo component */}
            {/* <SensorInfo sensorData={roomData.Sensors} /> */}
          </div>
        </div>
      </div>
    )
  );
};

export default RoomInfo;
