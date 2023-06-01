import React from "react";
import Head from "next/head";
import BuildingPlanClient from "@/components/BuildingPlanClient";
import { Container } from "react-bootstrap";

const DoctorLayout = ({ children }) => {
  return (
    <div className="bg-white text-black min-h-screen">
      <Head>
        <title>My Building Plan</title>
        <meta name="description" content="Interactive building plan" />
        <meta name="viewport" content="width=device-width, initial-scale=1" />
        <link rel="icon" href="/favicon.ico" />
      </Head>

      <main className="flex flex-col justify-center items-center py-5">
        <h1 className="my-3">Building Plan</h1>
        <BuildingPlanClient />
        <Container className="my-5">{children}</Container>
      </main>
    </div>
  );
};

export default DoctorLayout;
