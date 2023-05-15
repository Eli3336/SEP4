import React, { useState, useEffect } from "react";
import styles from "../styles/Home.module.css";
import { fetchDoctorDetailsById } from "@/api";

const DoctorInfo = ({ doctorId, onClose }) => {
  const [doctorData, setDoctorData] = useState(null);

  useEffect(() => {
    async function fetchDoctorData() {
      try {
        const doctorDetails = await fetchDoctorDetailsById(doctorId);
        setDoctorData(doctorDetails);
        console.log("Doctor Info:", doctorDetails);

        const updatedDoctorData = { ...doctorData };
        setDoctorData(updatedDoctorData);
        console.log("Doctor data:", updatedDoctorData);
      } catch (error) {
        console.error("Error fetching doctor data:", error);
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
              <p>password: {doctorData.password}</p>
              <p>phoneNumber: {doctorData.phoneNumber}</p>
            </div>
          </div>
        </div>
      </div>
    )
  );
};

export default DoctorInfo;
