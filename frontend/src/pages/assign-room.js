// src/pages/room-assign.js
import React from "react";
import ReceptionistLayout from "@/layouts/ReceptionistLayout";
import AssignPatient from "@/components/AssignPatient";

const AssignRoom = () => {
  return (
    <ReceptionistLayout>
      <AssignPatient />
    </ReceptionistLayout>
  );
};

export default AssignRoom;
