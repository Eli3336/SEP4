// DoctorInfo.js
import React, { useState, useEffect } from "react";
import { getDoctorById } from "@/api";

const DoctorInfo = ({ doctorId, onClose }) => {
  const [doctorData, setDoctorData] = useState(null);

  useEffect(() => {
    async function fetchDoctorData() {
      try {
        const doctorDetails = await getDoctorById(doctorId);
        setDoctorData(doctorDetails);
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
      <div onClick={handleClose}>
        <div
          onClick={(e) => {
            e.stopPropagation();
          }}
        >
          <div>
            <div>
              <h2>{doctorData.name}</h2>
              <p>Password: {doctorData.password}</p>
              <p>Phone Number: {doctorData.phoneNumber}</p>
            </div>
          </div>
        </div>
      </div>
    )
  );
};

export default DoctorInfo;
