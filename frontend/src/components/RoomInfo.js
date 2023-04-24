import { useState } from "react";
import styles from "@/styles/roominfo.module.css";
import SensorInfo from "./SensorInfo";

const RoomInfo = ({ roomData, onClose }) => {
  const [show, setShow] = useState(true);

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
            <p>Temperature: {roomData.temperature}°C</p>
            <p>Humidity: {roomData.humidity}%</p>
            <p>CO2: {roomData.co2} ppm</p>
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
