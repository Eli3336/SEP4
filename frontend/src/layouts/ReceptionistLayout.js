// src/layouts/ReceptionistLayout.js
import React from "react";
import Head from "next/head";
import BuildingPlanClient from "@/components/BuildingPlanClient";
import NavBar from "@/components/NavBar";
import ReceptionistNavBar from "@/components/ReceptionistNavBar"; // import the new NavBar
import MovePatient from "@/components/MovePatient";

const ReceptionistLayout = ({ children }) => {
  return (
    <>
      <Head>
        <title>My Building Plan</title>
        <meta name="description" content="Interactive building plan" />
        <meta name="viewport" content="width=device-width, initial-scale=1" />
        <link rel="icon" href="/favicon.ico" />
      </Head>
      <NavBar />
      <ReceptionistNavBar /> {/* add the new NavBar */}
      <main>
        <h1>Building Plan</h1>
        <BuildingPlanClient />
        <h2>Assign Patient to Room</h2>
        {children}
        <MovePatient />
      </main>
    </>
  );
};

export default ReceptionistLayout;
