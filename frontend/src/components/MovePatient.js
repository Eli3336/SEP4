import React, { useState, useEffect } from "react";
import Dropdown from "react-dropdown";
import "react-dropdown/style.css";
import {
  getPatientById,
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
  const [patientNames, setPatientNames] = useState([]);
  const [availableRooms, setAvailableRooms] = useState([]);

  useEffect(() => {
    async function fetchData() {
      try {
        const patients = await getAllPatients();
        const rooms = await getAllAAvailableRooms();
        setPatientNames(
          patients.map((patient) => ({
            value: patient.id,
            label: patient.name,
          }))
        );
        setAvailableRooms(
          rooms.map((room) => ({ value: room.id, label: room.name }))
        );
      } catch (error) {
        console.error("Error fetching data:", error);
      }
    }
    fetchData();
  }, []);

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
          onChange={(e) => setPatientId(e.value)}
          placeholder="Select a patient"
        />
      </div>
      <div>
        <Dropdown
          options={availableRooms}
          onChange={(e) => setRoomId(e.value)}
          placeholder="Select a room"
        />
      </div>
      <button onClick={handleMovePatient}>Move Patient</button>
      <RoomInfo roomId={roomId} />
    </div>
  );
};

export default MovePatient;
