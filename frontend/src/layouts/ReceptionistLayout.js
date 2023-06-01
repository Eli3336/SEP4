import React, { useState } from "react";
import Head from "next/head";
import BuildingPlanClient from "@/components/BuildingPlanClient";
import MovePatient from "@/components/MovePatient";
import AssignPatient from "@/components/AssignPatient";
import ViewRequests from "@/components/ViewRequests";
import { Container } from "react-bootstrap";

const ReceptionistLayout = () => {
  const [showAssignPatient, setShowAssignPatient] = useState(false);
  const [showMovePatient, setShowMovePatient] = useState(false);
  const [showViewRequests, setShowViewRequests] = useState(false);

  const handleAssignPatientClick = () => {
    setShowAssignPatient(!showAssignPatient);
  };

  const handleMovePatientClick = () => {
    setShowMovePatient(!showMovePatient);
  };

  const handleViewRequestsClick = () => {
    setShowViewRequests(!showViewRequests);
  };

  return (
    <div className="bg-white text-black min-h-screen">
      <Head>
        <title>My Building Plan</title>
        <meta name="description" content="Interactive building plan" />
        <meta name="viewport" content="width=device-width, initial-scale=1" />
        <link rel="icon" href="/favicon.ico" />
      </Head>

      <main className="flex flex-col justify-center items-center py-5">
        <h1>Building Plan</h1>

        <Container className="my-5">
          <div className="flex justify-center items-center">
            <button
              className="px-4 py-2 bg-blue-500 text-white rounded-md"
              onClick={handleMovePatientClick}
            >
              {showMovePatient ? "Hide Move Patient" : "Move Patient"}
            </button>
            <button
              className="px-4 py-2 bg-blue-500 text-white rounded-md"
              onClick={handleAssignPatientClick}
            >
              {showAssignPatient ? "Hide Assign Patient" : "Assign Patient"}
            </button>
            <button
              className="px-4 py-2 bg-blue-500 text-white rounded-md"
              onClick={handleViewRequestsClick}
            >
              {showViewRequests ? "Hide View Requests" : "View Requests"}
            </button>
          </div>
          {showMovePatient && (
            <>
              <hr className="my-4" />
              <div className="flex justify-center">
                <MovePatient />
              </div>
            </>
          )}
          {showAssignPatient && (
            <>
              <hr className="my-4" />
              <div className="flex justify-center">
                <AssignPatient />
              </div>
            </>
          )}
          {showViewRequests && (
            <>
              <hr className="my-4" />
              <div className="flex justify-center">
                <ViewRequests />
              </div>
            </>
          )}
        </Container>

        <div className="flex justify-center items-center">
          <BuildingPlanClient />
        </div>
      </main>
    </div>
  );
};

export default ReceptionistLayout;
