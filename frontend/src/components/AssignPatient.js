import React, { useState } from "react";
import { createAndAddPatientToRoom, deletePatientById } from "../api";

const AssignPatient = () => {
  const [patientName, setPatientName] = useState("");
  const [roomId, setRoomId] = useState("");
  const [patientId, setPatientId] = useState("");

  const handleAddPatient = async () => {
    try {
      await createAndAddPatientToRoom({ name: patientName }, roomId);
      alert("Patient added successfully");
    } catch (error) {
      console.error("Error adding patient:", error);
      alert("Failed to add patient");
    }
  };

  const handleRemovePatient = async () => {
    try {
      await deletePatientById(patientId);
      alert("Patient removed successfully");
    } catch (error) {
      console.error("Error removing patient:", error);
      alert("Failed to remove patient");
    }
  };

  return (
    <div>
      <h2>Assign Patient</h2>
      <div>
        <label htmlFor="patientName">Patient Name: </label>
        <input
          type="text"
          id="patientName"
          value={patientName}
          onChange={(e) => setPatientName(e.target.value)}
        />
      </div>
      <div>
        <label htmlFor="roomId">Room ID: </label>
        <input
          type="number"
          id="roomId"
          value={roomId}
          onChange={(e) => setRoomId(e.target.value)}
        />
      </div>
      <button onClick={handleAddPatient}>Add Patient</button>
      <div>
        <h3>Remove Patient</h3>
        <div>
          <label htmlFor="patientId">Patient ID: </label>
          <input
            type="number"
            id="patientId"
            value={patientId}
            onChange={(e) => setPatientId(e.target.value)}
          />
        </div>
        <button onClick={handleRemovePatient}>Remove Patient</button>
      </div>
    </div>
  );
};

export default AssignPatient;
