import React, { useState } from "react";
import {
  createReceptionist,
  deleteReceptionistById,
  updateReceptionist,
} from "../api";

const ReceptionistManagement = () => {
  const [receptionistName, setReceptionistName] = useState("");
  const [receptionistPassword, setReceptionistPassword] = useState("");
  const [receptionistPhoneNumber, setReceptionistPhoneNumber] = useState("");
  const [receptionistId, setReceptionistId] = useState("");

  const [newReceptionistName, setNewReceptionistName] = useState("");
  const [newReceptionistPhoneNumber, setNewReceptionistPhoneNumber] =
    useState("");

  const handleAddReceptionist = async () => {
    const receptionistInfo = {
      name: receptionistName,
      password: receptionistPassword,
      phoneNumber: receptionistPhoneNumber,
    };

    try {
      await createReceptionist(receptionistInfo);
      alert("Receptionist created successfully");
    } catch (error) {
      console.error("Error creating receptionist", error);
      alert("Failed to create receptionist");
    }
  };

  const handleUpdateReceptionist = async () => {
    try {
      await updateReceptionist(
        receptionistId,
        newReceptionistName,
        newReceptionistPhoneNumber
      );
      alert("Receptionist updated successfully");
    } catch (error) {
      console.error("Error updating receptionist:", error);
      alert("Failed to update receptionist");
    }
  };

  const handleRemoveReceptionist = async () => {
    try {
      await deleteReceptionistById(receptionistId);
      alert("Receptionist deleted successfully");
    } catch (error) {
      console.error("Error deleting receptionist:", error);
      alert("Failed to delete receptionist");
    }
  };

  return (
    <div>
      <h2>Create Receptionist</h2>
      <input
        type="text"
        placeholder="Receptionist Name"
        value={receptionistName}
        onChange={(e) => setReceptionistName(e.target.value)}
      />
      <input
        type="password"
        placeholder="Receptionist Password"
        value={receptionistPassword}
        onChange={(e) => setReceptionistPassword(e.target.value)}
      />
      <input
        type="text"
        placeholder="Receptionist Phone Number"
        value={receptionistPhoneNumber}
        onChange={(e) => setReceptionistPhoneNumber(e.target.value)}
      />
      <button onClick={handleAddReceptionist}>Add Receptionist</button>

      <h2>Update Receptionist</h2>
      <input
        type="text"
        placeholder="Receptionist ID"
        value={receptionistId}
        onChange={(e) => setReceptionistId(e.target.value)}
      />
      <input
        type="text"
        placeholder="New Receptionist Name"
        value={newReceptionistName}
        onChange={(e) => setNewReceptionistName(e.target.value)}
      />
      <input
        type="text"
        placeholder="New Receptionist Phone Number"
        value={newReceptionistPhoneNumber}
        onChange={(e) => setNewReceptionistPhoneNumber(e.target.value)}
      />
      <button onClick={handleUpdateReceptionist}>Update Receptionist</button>

      <h2>Delete Receptionist</h2>
      <input
        type="text"
        placeholder="Receptionist ID"
        value={receptionistId}
        onChange={(e) => setReceptionistId(e.target.value)}
      />
      <button onClick={handleRemoveReceptionist}>Delete Receptionist</button>
    </div>
  );
};

export default ReceptionistManagement;
