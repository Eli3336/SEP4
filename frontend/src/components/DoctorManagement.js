import React, { useState } from "react";
import { createDoctor, deleteDoctorById, updateDoctorInfo } from "../api";

const DoctorManagement = () => {
  const [doctorId, setDoctorId] = useState("");
  const [doctorName, setDoctorName] = useState("");
  const [doctorPassword, setDoctorPassword] = useState("");
  const [doctorPhoneNumber, setDoctorPhoneNumber] = useState("");

  const [newDoctorName, setNewDoctorName] = useState("");
  const [newDoctorPhoneNumber, setNewDoctorPhoneNumber] = useState("");

  // Handle Add Doctor
  const handleAddDoctor = async () => {
    const doctorInfo = {
      name: doctorName,
      password: doctorPassword,
      phoneNumber: doctorPhoneNumber,
    };

    try {
      await createDoctor(doctorInfo);
      alert("Doctor created successfully");
    } catch (error) {
      console.error("Error creating doctor", error);
      alert("Failed to create doctor");
    }
  };

  // Handle Update Doctor
  const handleUpdateDoctor = async () => {
    try {
      await updateDoctorInfo(doctorId, newDoctorName, newDoctorPhoneNumber);
      alert("Doctor updated successfully");
    } catch (error) {
      console.error("Error updating doctor:", error);
      alert("Failed to update doctor");
    }
  };

  // Handle Remove Doctor
  const handleRemoveDoctor = async () => {
    try {
      await deleteDoctorById(doctorId);
      alert("Doctor deleted successfully");
    } catch (error) {
      console.error("Error deleting doctor:", error);
      alert("Failed to delete doctor");
    }
  };

  return (
    <div>
      <h2>Create Doctor</h2>
      <input
        type="text"
        placeholder="Doctor Name"
        value={doctorName}
        onChange={(e) => setDoctorName(e.target.value)}
      />
      <input
        type="password"
        placeholder="Doctor Password"
        value={doctorPassword}
        onChange={(e) => setDoctorPassword(e.target.value)}
      />
      <input
        type="text"
        placeholder="Doctor Phone Number"
        value={doctorPhoneNumber}
        onChange={(e) => setDoctorPhoneNumber(e.target.value)}
      />
      <button onClick={handleAddDoctor}>Add Doctor</button>

      <h2>Update Doctor</h2>
      <input
        type="text"
        placeholder="Doctor ID"
        value={doctorId}
        onChange={(e) => setDoctorId(e.target.value)}
      />
      <input
        type="text"
        placeholder="New Doctor Name"
        value={newDoctorName}
        onChange={(e) => setNewDoctorName(e.target.value)}
      />
      <input
        type="text"
        placeholder="New Doctor Phone Number"
        value={newDoctorPhoneNumber}
        onChange={(e) => setNewDoctorPhoneNumber(e.target.value)}
      />
      <button onClick={handleUpdateDoctor}>Update Doctor</button>

      <h2>Delete Doctor</h2>
      <input
        type="text"
        placeholder="Doctor ID"
        value={doctorId}
        onChange={(e) => setDoctorId(e.target.value)}
      />
      <button onClick={handleRemoveDoctor}>Delete Doctor</button>
    </div>
  );
};

export default DoctorManagement;
