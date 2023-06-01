import React, { useState } from "react";
import { createAndAddPatientToRoom, deletePatientById } from "../api";
import { Button } from "react-bootstrap";

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
    <div className="my-5">
      <h2 className="my-3">Assign Patient</h2>
      <div className="flex justify-between border-b-2 border-gray-200">
        <div>
          <h3>Add Patient</h3>
          <div>
            <label htmlFor="patientName">Patient Name: </label>
            <input
              type="text"
              id="patientName"
              value={patientName}
              onChange={(e) => setPatientName(e.target.value)}
              className="border-2 border-black p-2 my-2 bg-white"
            />
          </div>
          <div>
            <label htmlFor="roomId">Room ID: </label>
            <input
              type="number"
              id="roomId"
              value={roomId}
              onChange={(e) => setRoomId(e.target.value)}
              className="border-2 border-black p-2 my-2 bg-white"
            />
          </div>
          <Button
            onClick={handleAddPatient}
            className="my-3 bg-blue-500 text-white hover:bg-blue-700"
          >
            Add Patient
          </Button>
        </div>
        <div>
          <h3>Remove Patient</h3>
          <div>
            <label htmlFor="patientId">Patient ID: </label>
            <input
              type="number"
              id="patientId"
              value={patientId}
              onChange={(e) => setPatientId(e.target.value)}
              className="border-2 border-black p-2 my-2 bg-white"
            />
          </div>
          <Button
            onClick={handleRemovePatient}
            className="my-3 bg-red-500 text-white hover:bg-red-700"
          >
            Remove Patient
          </Button>
        </div>
      </div>
    </div>
  );
};

export default AssignPatient;
