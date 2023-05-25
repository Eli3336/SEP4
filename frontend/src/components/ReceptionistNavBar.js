// src/components/ReceptionistNavBar.js
import React, { useState, useEffect } from "react";
import { getAllRequestsToMovePatients, getAllAdditionalRequests } from "../api";

const ReceptionistNavBar = () => {
  const [requestsToMove, setRequestsToMove] = useState([]);
  const [additionalRequests, setAdditionalRequests] = useState([]);
  const [showRequests, setShowRequests] = useState(false);

  useEffect(() => {
    const fetchRequests = async () => {
      try {
        const toMove = await getAllRequestsToMovePatients();
        setRequestsToMove(toMove);
        const additional = await getAllAdditionalRequests();
        setAdditionalRequests(additional);
      } catch (error) {
        console.error("Error fetching requests:", error);
      }
    };
    fetchRequests();
  }, []);

  const totalRequests = requestsToMove.length + additionalRequests.length;

  return (
    <nav>
      <ul>
        <li>
          <a href="/assign-room">Assign Room</a>
        </li>
        <li>
          <a href="#" onClick={() => setShowRequests(!showRequests)}>
            View Requests
            <span>{totalRequests}</span>
          </a>
        </li>
      </ul>

      {showRequests && (
        <div>
          <h2>Requests to Move Patients</h2>
          <ul>
            {requestsToMove.map((request) => (
              <li key={request.id}>{request.content}</li>
            ))}
          </ul>

          <h2>Additional Requests</h2>
          <ul>
            {additionalRequests.map((request) => (
              <li key={request.id}>{request.content}</li>
            ))}
          </ul>
        </div>
      )}

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
        span {
          background-color: #0070f3;
          border-radius: 50%;
          color: #fff;
          padding: 0.5rem;
          margin-left: 0.5rem;
        }
      `}</style>
    </nav>
  );
};

export default ReceptionistNavBar;
