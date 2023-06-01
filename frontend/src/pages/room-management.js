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
      <RoomManagement />
      <DoctorManagement />
      <DoctorInfo />
      <ReceptionistManagement />
      <ReceptionistInfo />
    </AdminLayout>
  );
};

export default AdminPage;
