// components/SensorInfo.js
import styles from "@/styles/sensorinfo.module.css";

const SensorInfo = ({ sensorData }) => {
  return (
    <div className={styles.container}>
      <h4>Sensors:</h4>
      <ul className={styles.sensorList}>
        {sensorData?.map((sensor, index) => (
          <li key={index} className={styles.sensorListItem}>
            {sensor.Type}: {sensor.Values.slice(-1)[0].value}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default SensorInfo;
