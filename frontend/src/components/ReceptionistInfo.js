import React, { useState, useEffect } from "react";
import styles from "../styles/Home.module.css";
import { getReceptionistById } from "@/api";
import ReceptionistManagement from "./ReceptionistManagement";

const ReceptionistInfo = ({ receptionistId, onClose }) => {
  const [receptionistData, setReceptionistData] = useState(null);

  useEffect(() => {
    async function fetchReceptionistData() {
      try {
        const receptionistDetails = await getReceptionistById(receptionistId);
        setReceptionistData(receptionistDetails);
        console.log("Receptionist Info:", receptionistDetails);

        const updatedReceptionistData = { ...receptionistData };
        setReceptionistData(updatedReceptionistData);
        console.log("Receptionist data:", updatedReceptionistData);
      } catch (error) {
        console.error("Error fetching receptionist data:", error);
      }
    }
    if (receptionistId) {
      fetchReceptionistData();
    }
  }, [receptionistId]);

  const handleClose = () => {
    onClose();
  };

  return (
    receptionistData && (
      <div className={styles.container} onClick={handleClose}>
        <div
          className={styles.modal}
          onClick={(e) => {
            e.stopPropagation();
          }}
        >
          <div className={styles.centerSection}>
            <div className={styles.content}>
              <h2>{receptionistData.name}</h2>
              <p>password: {receptionistData.password}</p>
              <p>phoneNumber: {receptionistData.phoneNumber}</p>
            </div>
          </div>
        </div>
      </div>
    )
  );
};

export default ReceptionistInfo;