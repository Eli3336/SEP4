import React, { useState } from "react";
import { createRoom, deleteRoomById, updateRoomInfo } from "../api";

const RoomManagement = () => {
  const [roomId, setRoomId] = useState("");
  const [roomName, setRoomName] = useState("");
  const [roomCapacity, setRoomCapacity] = useState("");
  const [roomAvailability, setRoomAvailability] = useState("");

  const [newRoomName, setNewRoomName] = useState("");
  const [newRoomCapacity, setNewRoomCapacity] = useState("");
  const [newRoomAvailability, setNewRoomAvailability] = useState("");

  // Handle Add Room
  const handleAddRoom = async () => {
    const roomInfo = {
      name: roomName,
      capacity: roomCapacity,
      availability: roomAvailability,
    };

    try {
      await createRoom(roomInfo);
      alert("Room created successfully");
    } catch (error) {
      console.error("Error creating room", error);
      alert("Failed to create room");
    }
  };

  // Handle Update Room
  const handleUpdateRoom = async () => {
    try {
      await updateRoomInfo(
        roomId,
        newRoomName,
        newRoomCapacity,
        newRoomAvailability
      );
      alert("Room updated successfully");
    } catch (error) {
      console.error("Error updating room:", error);
      alert("Failed to update room");
    }
  };

  // Handle Remove Room
  const handleRemoveRoom = async () => {
    try {
      await deleteRoomById(roomId);
      alert("Room deleted successfully");
    } catch (error) {
      console.error("Error deleting room:", error);
      alert("Failed to delete room");
    }
  };

  return (
    <div>
      <h2>Create Room</h2>
      <input
        type="text"
        placeholder="Room Name"
        value={roomName}
        onChange={(e) => setRoomName(e.target.value)}
      />
      <input
        type="text"
        placeholder="Room Capacity"
        value={roomCapacity}
        onChange={(e) => setRoomCapacity(e.target.value)}
      />
      <input
        type="text"
        placeholder="Room Availability"
        value={roomAvailability}
        onChange={(e) => setRoomAvailability(e.target.value)}
      />
      <button onClick={handleAddRoom}>Add Room</button>

      <h2>Update Room</h2>
      <input
        type="text"
        placeholder="Room ID"
        value={roomId}
        onChange={(e) => setRoomId(e.target.value)}
      />
      <input
        type="text"
        placeholder="New Room Name"
        value={newRoomName}
        onChange={(e) => setNewRoomName(e.target.value)}
      />
      <input
        type="text"
        placeholder="New Room Capacity"
        value={newRoomCapacity}
        onChange={(e) => setNewRoomCapacity(e.target.value)}
      />
      <input
        type="text"
        placeholder="New Room Availability"
        value={newRoomAvailability}
        onChange={(e) => setNewRoomAvailability(e.target.value)}
      />
      <button onClick={handleUpdateRoom}>Update Room</button>

      <h2>Delete Room</h2>
      <input
        type="text"
        placeholder="Room ID"
        value={roomId}
        onChange={(e) => setRoomId(e.target.value)}
      />
      <button onClick={handleRemoveRoom}>Delete Room</button>
    </div>
  );
};

export default RoomManagement;
