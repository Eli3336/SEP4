import React from "react";
import Link from "next/link";

const HomePage = () => {
  return (
    <div>
      <h1>Welcome to the Building Plan Application</h1>
      <p>
        Please select a role to continue: <Link href="/user">User</Link>,{" "}
        <Link href="/assign-room">Receptionist</Link>,{" "}
        <Link href="/room-management">Admin</Link> or{" "}
        <Link href="/DoctorManagementPage">Doctor</Link>
      </p>
    </div>
  );
};

export default HomePage;
