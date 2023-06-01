import React, { useEffect, useState } from "react";
import {
  getAllRequestsToMovePatients,
  getAllAdditionalRequests,
  deleteRequestById,
} from "../api";
import { Button } from "react-bootstrap";

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

  const handleDeleteRequest = async (id) => {
    try {
      await deleteRequestById(id);
      setRequestsToMove(requestsToMove.filter((request) => request.id !== id));
      setAdditionalRequests(
        additionalRequests.filter((request) => request.id !== id)
      );
    } catch (error) {
      console.error("Error deleting request:", error);
    }
  };

  return (
    <div className="text-dark">
      <h2 className="my-3">
        Requests to Move Patients
        <Button
          variant="info"
          onClick={() => setShowMoveRequests(!showMoveRequests)}
        >
          {requestsToMove.length}
        </Button>
      </h2>
      {showMoveRequests && (
        <ul>
          {requestsToMove.map((request) => (
            <li key={request.id}>
              {request.content}
              <Button
                variant="danger"
                onClick={() => handleDeleteRequest(request.id)}
              >
                Delete
              </Button>
            </li>
          ))}
        </ul>
      )}

      <h2 className="my-3">
        Additional Requests
        <Button
          variant="info"
          onClick={() => setShowAdditionalRequests(!showAdditionalRequests)}
        >
          {additionalRequests.length}
        </Button>
      </h2>
      {showAdditionalRequests && (
        <ul>
          {additionalRequests.map((request) => (
            <li key={request.id}>
              {request.content}
              <Button
                variant="danger"
                onClick={() => handleDeleteRequest(request.id)}
              >
                Delete
              </Button>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
};

export default ViewRequests;
