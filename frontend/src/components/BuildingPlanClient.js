import React, { useState, useEffect } from "react";
import { Row, Col, Button } from "react-bootstrap";
import RoomInfo from "./RoomInfo";
import { getRooms } from "../api";
import BaseLayer from "./BuildingPlan/hospital.png";

const BuildingPlanClient = () => {
  const [selectedRoom, setSelectedRoom] = useState(null);
  const [roomCount, setRoomCount] = useState(0);

  const handleRoomClick = (roomId) => {
    setSelectedRoom(roomId);
  };

  const handleClose = () => {
    setSelectedRoom(null);
  };

  useEffect(() => {
    const fetchRooms = async () => {
      try {
        const rooms = await getRooms();
        setRoomCount(rooms.length);
      } catch (error) {
        console.error("Error fetching rooms:", error);
      }
    };

    fetchRooms();
  }, []);

  return (
    <div className="text-dark">
      <Row className="mb-3">
        <Col xs="auto" className="pl-0">
          <div className="flex flex-col items-start">
            {Array.from({ length: roomCount }, (_, i) => i + 1).map(
              (roomId) => (
                <Button
                  key={roomId}
                  className="px-4 py-2 mb-2 text-xl font-bold text-white bg-green-500 rounded hover:bg-green-700"
                  onClick={() => handleRoomClick(roomId)}
                >
                  Room {roomId}
                </Button>
              )
            )}
          </div>
        </Col>
      </Row>
      <Row>
        <Col>
          <div className="relative">
            <img
              src={BaseLayer}
              alt="Base layer"
              className="w-full h-auto max-h-[75vh] max-w-[75vw] mx-auto"
            />
          </div>
        </Col>
      </Row>
      {selectedRoom && <RoomInfo roomId={selectedRoom} onClose={handleClose} />}
    </div>
  );
};

export default BuildingPlanClient;
