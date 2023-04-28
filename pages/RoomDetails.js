import React, { useState, useEffect } from "react";
import { useRouter } from "next/router";
import { roomDetails } from "/src/data/mockData.js";
import styles from "../styles/Home.module.css";

const RoomDetails = () => {
  const router = useRouter();
  const { roomId } = router.query;
  const [details, setDetails] = useState({});

  useEffect(() => {
    fetch("http://localhost:5113/rooms/${roomId}")
      .then(response => response.json())
      .then(roomDetails => {
        setDetails(roomDetails[roomId])
      })
      .catch(error => console.error(error));

    if (roomId) {
      setDetails(roomDetails[roomId]);
    }
  }, [roomId]);

  return (
    <div className={styles.roomDetailsContainer}>
      <h1>{`Room ${roomId}`}</h1>
      <h2>Patients</h2>
      <ul className={styles.roomDetailsList}>
        {details.patients &&
          details.patients.map((patient) => (
            <li key={patient.id}>{patient.name}</li>
          ))}
      </ul>
      <h2>Temperature</h2>
      <p>{details.temperature && `${details.temperature}°C`}</p>
      <h2>Humidity</h2>
      <p>{details.humidity && `${details.humidity}%`}</p>
    </div>
  );
};

export default RoomDetails;
