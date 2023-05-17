import React from "react";
import ReceptionistLayout from "@/layouts/ReceptionistLayout";
import AssignPatient from "@/components/AssignPatient";
import ViewRequests from "@/components/ViewRequests"; // import the new component

const AssignRoom = () => {
  return (
    <ReceptionistLayout>
      <AssignPatient />
      <ViewRequests />
    </ReceptionistLayout>
  );
};

export default AssignRoom;
