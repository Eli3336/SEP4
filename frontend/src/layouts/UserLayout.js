// src/layouts/UserLayout.js
import React from "react";
import Head from "next/head";
import BuildingPlanClient from "@/components/BuildingPlanClient";
import NavBar from "@/components/NavBar";

const UserLayout = ({ children }) => {
  return (
    <>
      <Head>
        <title>My Building Plan</title>
        <meta name="description" content="Interactive building plan" />
        <meta name="viewport" content="width=device-width, initial-scale=1" />
        <link rel="icon" href="/favicon.ico" />
      </Head>
      <NavBar />
      <main>
        <h1>Building Plan</h1>
        <BuildingPlanClient />
        {children}
      </main>
    </>
  );
};

export default UserLayout;
