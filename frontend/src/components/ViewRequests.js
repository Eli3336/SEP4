import React, { useEffect, useState } from "react";
import { getAllRequestsToMovePatients, getAllAdditionalRequests } from "../api";
import styles from "../styles/viewrequest.module.css";

const ViewRequests = () => {
  const [requestsToMove, setRequestsToMove] = useState([]);
  const [additionalRequests, setAdditionalRequests] = useState([]);
  const [showMoveRequests, setShowMoveRequests] = useState(false);
  const [showAdditionalRequests, setShowAdditionalRequests] = useState(false);

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

  return (
    <div>
      <h2>
        Requests to Move Patients
        <span
          className={styles.circle}
          onClick={() => setShowMoveRequests(!showMoveRequests)}
        >
          {requestsToMove.length}
        </span>
      </h2>
      {showMoveRequests && (
        <ul>
          {requestsToMove.map((request) => (
            <li key={request.id}>{request.content}</li>
          ))}
        </ul>
      )}

      <h2>
        Additional Requests
        <span
          className={styles.circle}
          onClick={() => setShowAdditionalRequests(!showAdditionalRequests)}
        >
          {additionalRequests.length}
        </span>
      </h2>
      {showAdditionalRequests && (
        <ul>
          {additionalRequests.map((request) => (
            <li key={request.id}>{request.content}</li>
          ))}
        </ul>
      )}
    </div>
  );
};

export default ViewRequests;
