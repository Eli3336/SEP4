import React, { useState, useEffect } from "react";
import AdminLayout from "@/layouts/AdminLayout";
import RoomManagement from "@/components/RoomManagement";
import { fetchRoomDetailsById } from "../api";

const RoomManagementPage = () => {
  const [roomData, setRoomData] = useState(null);
  const [roomIdInput, setRoomIdInput] = useState("");

  useEffect(() => {
    if (roomIdInput.trim() === "") return;

    const fetchData = async () => {
      try {
        const room = await fetchRoomDetailsById(roomIdInput);
        setRoomData(room);
      } catch (error) {
        console.error("Error fetching room data:", error);
      }
    };

    fetchData();
  }, [roomIdInput]);

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
    </AdminLayout>
  );
};

export default RoomManagementPage;
