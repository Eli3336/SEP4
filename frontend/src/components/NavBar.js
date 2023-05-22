// src/components/NavBar.js
import React from "react";
import Link from "next/link";

const NavBar = () => {
  return (
    <nav>
      <ul>
        <li>
          <Link href="/user">
            <a>User</a>
          </Link>
        </li>
        <li>
          <Link href="/assign-room">
            <a>Receptionist</a>
          </Link>
        </li>
        <li>
          <Link href="/room-management">
            <a>Admin</a>
          </Link>
        </li>
        <li>
          <Link href="/DoctorManagementPage">
            <a>Doctor</a>
          </Link>
        </li>
      </ul>
      <style jsx>{`
        nav {
          display: flex;
          justify-content: center;
          background-color: #f8f9fa;
          padding: 1rem 0;
        }
        ul {
          display: flex;
          list-style: none;
          margin: 0;
          padding: 0;
        }
        li {
          margin: 0 1rem;
        }
        a {
          text-decoration: none;
          color: #0070f3;
        }
        a:hover {
          color: #0056b3;
        }
      `}</style>
    </nav>
  );
};

export default NavBar;
