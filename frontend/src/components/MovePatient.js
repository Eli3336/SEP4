import React, { useState, useEffect } from "react";
import Dropdown from "react-dropdown";
import "react-dropdown/style.css";
import {
  getAllPatients,
  getAllAAvailableRooms,
  createAndAddPatientToRoom,
  deletePatientById,
} from "../api";
import RoomInfo from "./RoomInfo";

const MovePatient = () => {
  const [roomId, setRoomId] = useState("");
  const [patientId, setPatientId] = useState("");
  const [patientData, setPatientData] = useState(null);
  const patientNames = getAllPatients.map((patient) => patient.name);
  const availableRooms = getAllAAvailableRooms.map((room) => room.name);
  useEffect(() => {
    async function fetchPatientData() {
      try {
        const patientDetails = await getPatientById(patientId);
        setPatientData(patientDetails);
      } catch (error) {
        console.error("Error fetching patient data:", error);
      }
    }
    if (patientId) {
      fetchPatientData();
    }
  }, [patientId]);
  const handleMovePatient = async () => {
    try {
      await createAndAddPatientToRoom(patientData, roomId);
      await deletePatientById(patientId);
      alert("Patient moved successfully");
    } catch (error) {
      console.error("Error moving patient:", error);
      alert("Failed to move patient");
    }
  };

  return (
    <div>
      <h2>Move Patient to another room</h2>
      <div>
        <Dropdown
          options={patientNames}
          value={patientNames[0]}
          onChange={(e) => setPatientId(e.target.value)}
          placeholder="Select a patient"
        />
        ;
      </div>
      <div>
        <Dropdown
          options={availableRooms}
          onChange={(e) => setRoomId(e.target.value)}
          value={availableRooms}
          placeholder="Select a room"
        />
      </div>
      <button onClick={handleMovePatient}>Move Patient</button>
    </div>
  );
};

export default MovePatient;
