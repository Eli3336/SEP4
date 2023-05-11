import React, { useState } from "react";
import { updateRoom } from "../api"; // Import the updateRoom function

const RoomManagement = ({ room }) => {
  const [roomInfo, setRoomInfo] = useState({
    id: room.id,
    name: room.name,
    capacity: room.capacity,
    availability: room.availability,
  });

  const [newCapacity, setNewCapacity] = useState(room.capacity || 0);

  const [newAvailability, setNewAvailability] = useState(room.availability);

  const handleRoomUpdate = async () => {
    try {
      console.log("Updating room with:", {
        roomId: roomInfo.id,
        newCapacity: parseInt(newCapacity, 10),
        newAvailability,
      });

      await updateRoom(roomInfo.id, parseInt(newCapacity, 10), newAvailability);
      setRoomInfo({
        ...roomInfo,
        capacity: parseInt(newCapacity, 10),
        availability: newAvailability,
      });
      alert("Room updated successfully");
    } catch (error) {
      console.error("Error updating room:", error);
      alert("Failed to update room");
    }
  };

  return (
    <div>
      <h2>Room Management</h2>
      <div>
        <p>Room ID: {roomInfo.id}</p>
        <p>Room Name: {roomInfo.name}</p>
        <p>Room Capacity: {roomInfo.capacity}</p>
        <p>Room Availability: {roomInfo.availability}</p>
      </div>
      <div>
        <label htmlFor="capacity">New Capacity: </label>
        <input
          type="number"
          id="capacity"
          value={newCapacity}
          onChange={(e) =>
            setNewCapacity(e.target.value ? parseInt(e.target.value, 10) : 0)
          }
        />
      </div>
      <div>
        <label htmlFor="availability">New Availability: </label>
        <select
          id="availability"
          value={newAvailability}
          onChange={(e) => setNewAvailability(e.target.value)}
        >
          <option value="Available">Available</option>
          <option value="Under maintenance">Under maintenance</option>
        </select>
      </div>
      <button onClick={handleRoomUpdate}>Update Room</button>
    </div>
  );
};

export default RoomManagement;
