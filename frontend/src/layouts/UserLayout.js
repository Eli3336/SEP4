import React from "react";
import Head from "next/head";
import { Container } from "react-bootstrap";
import BuildingPlanClient from "@/components/BuildingPlanClient";

const UserLayout = ({ children }) => {
  return (
    <div className="d-flex flex-column min-vh-100 text-dark bg-white">
      <Head>
        <title>My Building Plan</title>
        <meta name="description" content="Interactive building plan" />
        <meta name="viewport" content="width=device-width, initial-scale=1" />
      </Head>

      <Container className="flex-grow-1">
        <h1 className="mt-3">Building Plan</h1>
        <BuildingPlanClient />
        <div className="overflow-auto">{children}</div>
      </Container>
    </div>
  );
};

export default UserLayout;
