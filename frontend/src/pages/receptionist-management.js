import React, { useState, useEffect } from "react";
import AdminLayout from "@/layouts/AdminLayout";
import ReceptionistManagement from "@/components/ReceptionistManagement";
import { fetchRoomDetailsById } from "../api";
import ReceptionistInfo from "@/components/ReceptionistInfo";

const ReceptionistManagementPage = () => {
  //const [receptionistData, setReceptionistData] = useState(null);
  const [receptionistIdInput, setReceptionistIdInput] = useState("");


  return (
    <AdminLayout>
      <div>
        <label htmlFor="receptionistId">Receptionist ID: </label>
        <input
          type="text"
          id="receptionistId"
          value={receptionistIdInput}
          onChange={(e) => setReceptionistIdInput(e.target.value)}
        />
      </div>
      <ReceptionistInfo receptionistId={receptionistIdInput}/>
      {/* {<ReceptionistManagement/>} */}
    </AdminLayout>
  );
};

export default ReceptionistManagementPage;
