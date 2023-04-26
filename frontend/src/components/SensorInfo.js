// components/SensorInfo.js
import styles from "@/styles/sensorinfo.module.css";

const SensorInfo = ({ sensorData }) => {
  return (
    <div className={styles.container}>
      <h4>Sensor Logs:</h4>
      <ul className={styles.sensorList}>
        {sensorData?.map((sensor, index) => (
          <li key={index} className={styles.sensorListItem}>
            <strong>{sensor.Type}:</strong>
            {sensor.Values.map((value, idx) => (
              <div key={idx}>
                {value.timestamp}: {value.value}
              </div>
            ))}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default SensorInfo;
