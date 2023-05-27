import React, { useState, useEffect } from "react";
import AdminLayout from "@/layouts/AdminLayout";
import RoomManagement from "@/components/RoomManagement";
import DoctorInfo from "@/components/DoctorInfo";
import ReceptionistManagement from "@/components/ReceptionistManagement";
import ReceptionistInfo from "@/components/ReceptionistInfo";
import {
  fetchRoomDetailsById,
  fetchDoctorDetailsById,
  getReceptionistById,
} from "../api";

const AdminPage = () => {
  const [roomData, setRoomData] = useState(null);
  const [roomIdInput, setRoomIdInput] = useState("");
  const [doctorData, setDoctorData] = useState(null);
  const [doctorIdInput, setDoctorIdInput] = useState("");
  const [receptionistData, setReceptionistData] = useState(null);
  const [receptionistIdInput, setReceptionistIdInput] = useState("");

  useEffect(() => {
    if (roomIdInput.trim() === "") return;
    const fetchRoomData = async () => {
      try {
        const room = await fetchRoomDetailsById(roomIdInput);
        setRoomData(room);
      } catch (error) {
        console.error("Error fetching room data:", error);
      }
    };
    fetchRoomData();
  }, [roomIdInput]);

  useEffect(() => {
    if (doctorIdInput.trim() === "") return;
    const fetchDoctorData = async () => {
      try {
        const doctor = await fetchDoctorDetailsById(doctorIdInput);
        setDoctorData(doctor);
      } catch (error) {
        console.error("Error fetching doctor data:", error);
      }
    };
    fetchDoctorData();
  }, [doctorIdInput]);

  useEffect(() => {
    if (receptionistIdInput.trim() === "") return;
    const fetchReceptionistData = async () => {
      try {
        const receptionist = await getReceptionistById(receptionistIdInput);
        setReceptionistData(receptionist);
      } catch (error) {
        console.error("Error fetching receptionist data:", error);
      }
    };
    fetchReceptionistData();
  }, [receptionistIdInput]);

  return (
    <AdminLayout>
      <div>
        <label htmlFor="roomId">Room ID: </label>
        <input
          type="text"
          id="roomId"
          value={roomIdInput}
          onChange={(e) => setRoomIdInput(e.target.value)}
        />
      </div>
      {roomData ? <RoomManagement room={roomData} /> : <p>Loading...</p>}

      <div>
        <label htmlFor="doctorId">Doctor ID: </label>
        <input
          type="text"
          id="doctorId"
          value={doctorIdInput}
          onChange={(e) => setDoctorIdInput(e.target.value)}
        />
      </div>
      {doctorData ? <DoctorInfo doctor={doctorData} /> : <p>Loading...</p>}

      <div>
        <label htmlFor="receptionistId">Receptionist ID: </label>
        <input
          type="text"
          id="receptionistId"
          value={receptionistIdInput}
          onChange={(e) => setReceptionistIdInput(e.target.value)}
        />
      </div>
      {receptionistData ? (
        <ReceptionistInfo receptionist={receptionistData} />
      ) : (
        <p>Loading...</p>
      )}
      <ReceptionistManagement />
    </AdminLayout>
  );
};

export default AdminPage;
