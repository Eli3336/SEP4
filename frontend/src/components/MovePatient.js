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
import { Button } from "react-bootstrap";

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
    <div className="text-dark">
      <h2 className="my-3">Move Patient to another room</h2>
      <Dropdown
        options={patientNames}
        onChange={(e) => setPatientId(e.value)}
        placeholder="Select a patient"
        className="my-3"
      />
      <Dropdown
        options={availableRooms}
        onChange={(e) => setRoomId(e.value)}
        placeholder="Select a room"
        className="my-3"
      />
      <Button
        onClick={handleMovePatient}
        className="my-3 bg-blue-500 text-white hover:bg-blue-700"
      >
        Move Patient
      </Button>
      <RoomInfo roomId={roomId} />
    </div>
  );
};

export default MovePatient;
