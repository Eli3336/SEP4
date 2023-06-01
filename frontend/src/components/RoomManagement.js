import React, { useState } from "react";
import { createRoom, deleteRoomById, updateRoomInfo } from "../api";

const RoomManagement = () => {
  const [roomId, setRoomId] = useState("");
  const [roomName, setRoomName] = useState("");
  const [roomCapacity, setRoomCapacity] = useState(0);
  const [roomAvailability, setRoomAvailability] = useState(true);

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

  const handleUpdateRoom = async () => {
    const updatedRoomInfo = {
      name: roomName,
      capacity: roomCapacity,
      availability: roomAvailability,
    };
    try {
      await updateRoomInfo(roomId, updatedRoomInfo);
      alert("Room updated successfully");
    } catch (error) {
      console.error("Error updating room:", error);
      alert("Failed to update room");
    }
  };

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
        type="number"
        placeholder="Room Capacity"
        value={roomCapacity}
        onChange={(e) => setRoomCapacity(parseInt(e.target.value))}
      />
      <input
        type="checkbox"
        checked={roomAvailability}
        onChange={(e) => setRoomAvailability(e.target.checked)}
      />
      Room Available
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
        placeholder="Room Name"
        value={roomName}
        onChange={(e) => setRoomName(e.target.value)}
      />
      <input
        type="number"
        placeholder="Room Capacity"
        value={roomCapacity}
        onChange={(e) => setRoomCapacity(parseInt(e.target.value))}
      />
      <input
        type="checkbox"
        checked={roomAvailability}
        onChange={(e) => setRoomAvailability(e.target.checked)}
      />
      Room Available
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
