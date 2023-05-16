import React, { useState, useEffect } from "react";
import DoctorLayout from "@/layouts/DoctorLayout";
import DoctorInfo from "@/components/DoctorInfo";
import { fetchDoctorDetailsById } from "../api";
import { addDoctor, deleteDoctorById } from "../api";
import { updateDoctor } from "../api"; // Import the updateDoctor function
const DoctorPage = ({ doctor }) => {
  const [doctorInfo, setDoctorInfo] = useState({
    id: doctor.id,
    name: doctor.name,
    password: doctor.password,
    phoneNumber: doctor.phoneNumber,
  });

  const [newPassword, setNewPassword] = useState(doctor.password);

  const [newPhoneNumber, setNewPhoneNumber] = useState(doctor.phoneNumber);

  const handleDoctorUpdate = async () => {
    try {
      console.log("Updating doctor info with:", {
        doctorId: doctorInfo.id,
        newPassword,
        newPhoneNumber,
      });

      await updateDoctor(doctorInfo.id, newPassword, newPhoneNumber);
      setDoctorInfo({
        ...doctorInfo,
        password: newPassword,
        phoneNumber: newPhoneNumber,
      });
      alert("Doctor info updated successfully");
    } catch (error) {
      console.error("Error updating doctor info:", error);
      alert("Failed to update doctor info ");
    }
  };

  const addDoctor = async () => {
    try {
      await addDoctor(
        doctorInfo.id,
        doctorInfo.name,
        doctorInfo.password,
        doctorInfo.phoneNumber
      );
      alert("Doctor added successfully");
    } catch (error) {
      console.error("Error adding doctor:", error);
      alert("Failed to add doctor");
    }
  };
  const handleRemoveDoctor = async () => {
    try {
      await deleteDoctorById(doctorInfo.id);
      alert("doctor removed successfully");
    } catch (error) {
      console.error("Error removing doctor:", error);
      alert("Failed to remove doctor");
    }
  };
  return (
    <DoctorLayout>
      <div>
        <h2>Doctor Information Management</h2>
        <div>
          <p>Doctor Name: {doctorInfo.name}</p>
          <p>Password: {doctorInfo.password}</p>
          <p>Phone Number: {doctorInfo.phoneNumber}</p>
        </div>
        <div>
          <label htmlFor="password">New Password: </label>
          <input
            type="string"
            id="password"
            value={newPassword}
            onChange={(e) => setNewPassword(e.target.value)}
          />
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
    </DoctorLayout>
  );
};

export default DoctorPage;
