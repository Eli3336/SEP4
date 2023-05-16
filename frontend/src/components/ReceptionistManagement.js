import React, { useState } from "react";
import { createReceptionist, deleteReceptionistById, updateReceptionist} from "../api";

const ReceptionistManagement = ({receptionist}) => {
  
  //sort of a receptionist DTO
  const [receptionistInfo, setReceptionistInfo] = useState({
    name : receptionist.name,
    password : receptionist.password,
    phoneNumber : receptionist.phoneNumber,
  })

  //temp value holders
  const [receptionistName, setReceptionistName] = useState(receptionist.name);
  const [receptionistPassword, setReceptionistPassword] = useState(receptionis.password);
  const [receptionistPhoneNumber, setReceptionistPhoneNumber] = useState(receptionist.phoneNumber);
  const [receptionistId, setReceptionistId] = useState("");

  const [newReceptionistName, setNewReceptionistName] = useState(receptionist.password);
  const [newReceptionistPhoneNumber, setNewReceptionistPhoneNumber] = useState(receptionist.phoneNumber);


  const handleAddReceptionist = async () => {
   
    setReceptionistInfo({
        name: receptionistName,
        password : receptionistPassword,
        phoneNumber : receptionistPhoneNumber,
    })

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
      await updateReceptionist(receptionistId, newReceptionistName, newReceptionistPhoneNumber);
      setReceptionistInfo({
        ...receptionistInfo,
        name: newReceptionistName,
        phoneNumber: newReceptionistPhoneNumber,
      });

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
      <div>
        <label htmlFor="receptionistName">Receptionist's Name: </label>
        <input
          type="text"
          id="receptionistName"
          value={receptionistName}
          onChange={(e) => setReceptionistName(e.target.value)}
        />
      </div>
      <div>
        <label htmlFor="password">Password: </label>
        <input
          type="text"
          id="password"
          value={receptionistPassword}
          onChange={(e) => setReceptionistPassword(e.target.value)}
        />
      </div>
      <div>
        <label htmlFor="phoneNumber">Phone Number: </label>
        <input
          type="number"
          id="phoneNumber"
          value={receptionistPhoneNumber}
          onChange={(e) => setReceptionistPhoneNumber(e.target.value)}
        />
      </div>
      <button onClick={handleAddReceptionist}>Add Receptionist</button>

      <div>
        <h3>Update Receptionist</h3>
        <div>
          <label htmlFor="receptionistId">Receptionist ID: </label>
          <input
            type="number"
            id="receptionistId"
            value={receptionistId}
            onChange={(e) => setReceptionistId(e.target.value)}
          />
        </div>
        <div>
        <label htmlFor="newReceptionistName">New Name: </label>
        <input
          type="text"
          id="newReceptionistName"
          value={newReceptionistName}
          onChange={(e) => setNewReceptionistName(e.target.value)}
        />
        </div>
        <div>
        <label htmlFor="newPhoneNumber">Phone Number: </label>
        <input
          type="number"
          id="newPhoneNumber"
          value={NewReceptionistPhoneNumber}
          onChange={(e) => setNewReceptionistPhoneNumber(e.target.value)}
        />
      </div>
      </div>
      <button onClick={handleUpdateReceptionist}>Update Receptionist</button>

      <div>
        <h3>Remove Receptionist</h3>
        <div>
          <label htmlFor="receptionistId">Receptionist ID: </label>
          <input
            type="number"
            id="receptionistId"
            value={receptionistId}
            onChange={(e) => setReceptionistId(e.target.value)}
          />
        </div>
        <button onClick={handleRemoveReceptionist}>Remove Receptionist</button>
      </div>
    </div>
  );
};

export default ReceptionistManagement;
