// pages/DoctorPage.js
import React from "react";
import DoctorLayout from "@/layouts/DoctorLayout";
import CreateRequest from "@/components/CreateRequest";

const DoctorPage = () => {
  return (
    <DoctorLayout>
      <CreateRequest />
    </DoctorLayout>
  );
};

export default DoctorPage;
