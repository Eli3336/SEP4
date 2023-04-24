// import { useState } from "react";
// import styles from "@/styles/buildingplan.module.css";
// import RoomInfo from "./RoomInfo";
// // import mockData from "@/data/mockData";
// import RoomSVG from "@/components/RoomSVG";
// import { fetchRoomDetailsById } from "@/api";
// const BuildingPlanClient = () => {
//   const [selectedRoom, setSelectedRoom] = useState(null);
//   const [roomInfo, setRoomInfo] = useState(null);

//   const handleRoomClick = async (e) => {
//     const roomId = e.target.getAttribute("id");

//     if (roomId) {
//       setSelectedRoom(roomId);
//       try {
//         const roomData = await fetchRoomDetailsById(roomId);
//         setRoomInfo(roomData);
//         console.log("Selected room:", roomId, "Data:", roomData);
//       } catch (error) {
//         console.error("Error fetching room details:", error);
//       }
//     }
//   };

//   const handleClose = () => {
//     setSelectedRoom(null);
//     setRoomInfo(null);
//   };

//   return (
//     <div className={styles.outerContainer}>
//       <div className={styles.svgContainer}>
//         <RoomSVG onRoomClick={handleRoomClick} className={styles.svgClass} />
//       </div>
//       {selectedRoom && <RoomInfo roomData={roomInfo} onClose={handleClose} />}
//     </div>
//   );
// };

// export default BuildingPlanClient;

import { useState } from "react";
import styles from "@/styles/buildingplan.module.css";
import RoomInfo from "./RoomInfo";
import RoomSVG from "@/components/RoomSVG";
import { fetchRoomDetailsById,fetchSensorData } from "@/api";

const BuildingPlanClient = () => {
  const [selectedRoom, setSelectedRoom] = useState(null);
  const [roomInfo, setRoomInfo] = useState(null);

  const handleRoomClick = async (e) => {
    const roomId = e.target.getAttribute("id");

    if (roomId) {
      setSelectedRoom(roomId);
      
      try {
        const fetchedRoomData = await fetchRoomDetailsById(roomId);
        if (fetchedRoomData.error) {
          console.error("Error fetching room details:", fetchedRoomData.error);
          return;
        }

        const fetchedSensorData = await fetchSensorData(roomId)
        if(fetchedSensorData.error){
          console.error("Error fetching sensor data:",error)
          return
        }

        const roomData = {
          ...fetchedRoomData,
          // temperature: fetchedRoomData.Sensors[0].Values[0].value,
          // humidity: fetchedRoomData.Sensors[1].Values[0].value,
          // co2: fetchedRoomData.Sensors[2].Values[0].value,
          temperature: fetchedSensorData[0].value,
          humidity:fetchedSensorData[1].value,
          co2: fetchedSensorData[2].value,
          patients: fetchedRoomData.patients || [],
          Sensors: fetchedRoomData.Sensors,
        };
        setRoomInfo(roomData);
        console.log("Selected room:", roomId, "Data:", roomData);
      } catch (error) {
        console.error("Error fetching room details:", error);
      }

      // try{

      //   const fetchedSensorData = await fetchSensorData(roomId)
      //   if(fetchedSensorData.error){
      //     console.error("Error fetching sensor data:",error)
      //     return
      //   }



      // }catch (error){
      //   console.error("Error fetching sensor data:", error)
      // }
    }
  };

  const handleClose = () => {
    setSelectedRoom(null);
    setRoomInfo(null);
  };

  return (
    <div className={styles.outerContainer}>
      <div className={styles.svgContainer}>
        <RoomSVG onRoomClick={handleRoomClick} className={styles.svgClass} />
      </div>
      {selectedRoom && <RoomInfo roomData={roomInfo} onClose={handleClose} />}
    </div>
  );
};

export default BuildingPlanClient;
