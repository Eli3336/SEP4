// pages/AdminPage.js
import React from "react";
import AdminLayout from "@/layouts/AdminLayout";
import RoomManagement from "@/components/RoomManagement";
import DoctorManagement from "@/components/DoctorManagement";
import DoctorInfo from "@/components/DoctorInfo";
import ReceptionistManagement from "@/components/ReceptionistManagement";
import ReceptionistInfo from "@/components/ReceptionistInfo";

const AdminPage = () => {
  return (
    <AdminLayout>
      <div className="container mx-auto px-4">
        <div className="my-5">
          <h2 className="my-3">Room Management</h2>
          <RoomManagement />
        </div>
        <div className="my-5">
          <h2 className="my-3">Doctor Management</h2>
          <DoctorManagement />
        </div>
        <div className="my-5">
          <h2 className="my-3">Doctor Info</h2>
          <DoctorInfo />
        </div>
        <div className="my-5">
          <h2 className="my-3">Receptionist Management</h2>
          <ReceptionistManagement />
        </div>
        <div className="my-5">
          <h2 className="my-3">Receptionist Info</h2>
          <ReceptionistInfo />
        </div>
      </div>
    </AdminLayout>
  );
};

export default AdminPage;
