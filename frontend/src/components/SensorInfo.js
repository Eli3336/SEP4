import React, { useState, useEffect, useCallback } from "react";
import styles from "../styles/sensorinfo.module.css";
import { fetchSensorLogById } from "@/api";

const SensorInfo = ({ sensors = [] }) => {
  const [logs, setLogs] = useState([]);
  const [sensorRows, setSensorRows] = useState([]);

  useEffect(() => {
    async function fetchLogs() {
      try {
        const sensorLogs = await Promise.all(
          sensors.map(async (sensor) => {
            const values = await fetchSensorLogById(sensor.id);
            return { id: sensor.id, values: values };
          })
        );
        setLogs(sensorLogs);
      } catch (error) {
        console.error("Error fetching sensor logs:", error);
      }
    }

    if (sensors.length > 0) {
      fetchLogs();
    }
  }, [sensors]);

  const buildSensorRows = useCallback(
    (sensor) => {
      const sensorLog = logs.find((log) => log.id === sensor.id);
      if (!sensorLog || !sensorLog.values || sensorLog.values.length === 0) {
        return null;
      }
      return sensorLog.values.map((value, idx) => {
        const timeStamp = new Date(value.timeStamp).toLocaleString();
        return (
          <tr key={`${sensor.type}-${idx}`}>
            {idx === 0 && (
              <td rowSpan={sensorLog.values.length}>{sensor.type}</td>
            )}
            <td>{timeStamp}</td>
            <td>{value.value}</td>
          </tr>
        );
      });
    },
    [logs]
  );

  useEffect(() => {
    if (logs.length > 0) {
      setSensorRows(sensors.flatMap((sensor) => buildSensorRows(sensor)));
    }
  }, [logs, sensors, buildSensorRows]);

  return (
    <div className={styles.container}>
      <h4>Sensor Logs:</h4>
      <table className={styles.sensorTable}>
        <thead>
          <tr>
            <th>Type</th>
            <th>Timestamp</th>
            <th>Value</th>
          </tr>
        </thead>
        <tbody>{sensorRows}</tbody>
      </table>
    </div>
  );
};

export default SensorInfo;
