import React, { useState, useEffect } from "react";
import styles from "../styles/Home.module.css";
import { fetchDoctorDetailsById } from "@/api";

const DoctorInfo = ({ doctorId, onClose }) => {
  const [doctorData, setDoctorData] = useState(null);

  useEffect(() => {
    async function fetchDoctorData() {
      try {
        const doctorDetails = await fetchDoctorDetailsById(doctorId);
        setRoomData(doctorDetails);
        console.log("Doctor Info:", doctorDetails);
      } catch (error) {
        console.error("Error fetching doctor Info:", error);
      }
    }

    if (doctorId) {
      fetchDoctorData();
    }
  }, [doctorId]);

  const handleClose = () => {
    onClose();
  };

  return (
    doctorData && (
      <div className={styles.container} onClick={handleClose}>
        <div
          className={styles.modal}
          onClick={(e) => {
            e.stopPropagation();
          }}
        >
          <div className={styles.centerSection}>
            <div className={styles.content}>
              <h2>{doctorData.name}</h2>
              <p>phoneNumber: {doctorData.phoneNumber}</p>
            </div>
          </div>
        </div>
      </div>
    )
  );
};

export default DoctorInfo;
