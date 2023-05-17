import React, { useState } from "react";
import { updateDoctorInfo } from "../api"; // Import the updateDoctorInfo function

const DoctorManagement = ({ doctor }) => {
  const [doctorInfo, setDoctorInfo] = useState({
    id: doctor.id,
    name: doctor.name,
    phoneNumber: doctor.phoneNumber,
  });

  const [newPhoneNumber, setNewPhoneNumber] = useState(doctor.phoneNumber);

  const handleDoctorUpdate = async () => {
    try {
      console.log("Updating doctor info with:", {
        doctorId: doctorInfo.id,
        newPhoneNumber,
      });

      await updateDoctorInfo(doctorInfo.id, doctorInfo.name, newPhoneNumber);
      setDoctorInfo({
        ...doctorInfo,
        phoneNumber: newPhoneNumber,
      });
      alert("Doctor info updated successfully");
    } catch (error) {
      console.error("Error updating doctor info:", error);
      alert("Failed to update doctor info ");
    }
  };

  return (
    <div>
      <h2>Doctor Information Management</h2>
      <div>
        <p>Doctor Name: {doctorInfo.name}</p>
        <p>Phone Number: {doctorInfo.phoneNumber}</p>
      </div>
      <div>
        <label htmlFor="phoneNumber">New Phone Number: </label>
        <input
          type="number"
          id="phoneNumber"
          value={newPhoneNumber}
          onChange={(e) => setNewPhoneNumber(e.target.value)}
        />
      </div>
      <button onClick={handleDoctorUpdate}>Update Doctor Information</button>
    </div>
  );
};

export default DoctorManagement;
