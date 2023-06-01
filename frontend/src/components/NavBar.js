import React from "react";
import Link from "next/link";

const NavBar = () => {
  return (
    <nav>
      <ul>
        <li>
          <Link href="/user">
            <span>User</span>
          </Link>
        </li>
        <li>
          <Link href="/assign-room">
            <span>Receptionist</span>
          </Link>
        </li>
        <li>
          <Link href="/room-management">
            <span>Admin</span>
          </Link>
        </li>
        <li>
          <Link href="/doctor">
            <span>Doctor</span>
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
        a,
        span {
          cursor: pointer;
          text-decoration: none;
          color: #0070f3;
        }
        a:hover,
        span:hover {
          color: #0056b3;
        }
      `}</style>
    </nav>
  );
};

export default NavBar;
