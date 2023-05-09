import { useState } from "react";
import styles from "../styles/buildingplan.module.css";
import RoomInfo from "./RoomInfo";
import RoomSVG from "@/components/RoomSVG";

const BuildingPlanClient = () => {
  const [selectedRoom, setSelectedRoom] = useState(null);

  const handleRoomClick = (e) => {
    const roomId = e.target.getAttribute("id");
    if (roomId) {
      setSelectedRoom(roomId);
    }
  };

  const handleClose = () => {
    setSelectedRoom(null);
  };

  return (
    <div className={styles.outerContainer}>
      <div className={styles.svgContainer}>
        <RoomSVG onRoomClick={handleRoomClick} className={styles.svgClass} />
      </div>
      {selectedRoom && <RoomInfo roomId={selectedRoom} onClose={handleClose} />}
    </div>
  );
};

export default BuildingPlanClient;
