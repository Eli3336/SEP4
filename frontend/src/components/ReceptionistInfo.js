// ReceptionistInfo.js
import React, { useState, useEffect } from "react";
import { getReceptionistById } from "@/api";

const ReceptionistInfo = ({ receptionistId, onClose }) => {
  const [receptionistData, setReceptionistData] = useState(null);

  useEffect(() => {
    async function fetchReceptionistData() {
      try {
        const receptionistDetails = await getReceptionistById(receptionistId);
        setReceptionistData(receptionistDetails);
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
      <div onClick={handleClose}>
        <div
          onClick={(e) => {
            e.stopPropagation();
          }}
        >
          <div>
            <div>
              <h2>{receptionistData.name}</h2>
              <p>Password: {receptionistData.password}</p>
              <p>Phone Number: {receptionistData.phoneNumber}</p>
            </div>
          </div>
        </div>
      </div>
    )
  );
};

export default ReceptionistInfo;
