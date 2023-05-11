const API_BASE_URL = "https://localhost:7216";

export async function fetchRoomDetailsById(roomId) {
  try {
    const roomDataUrl = `${API_BASE_URL}/Rooms/${roomId}`;
    console.log("fetchRoomDetailsById URL:", roomDataUrl);

    const roomDataResponse = await fetch(roomDataUrl);
    console.log("fetchRoomDetailsById response:", roomDataResponse);

    if (!roomDataResponse.ok) {
      throw new Error(
        `Error fetching room data: ${roomDataResponse.statusText}`
      );
    }
    const roomData = await roomDataResponse.json();
    console.log("fetchRoomDetailsById data:", roomData);

    return roomData;
  } catch (error) {
    console.error("Error in fetchRoomDetailsById:", error);
    throw error;
  }
}

export async function fetchDoctorDetailsById(doctorId) {
  try {
    const doctorDataUrl = `${API_BASE_URL}/Doctors/${doctorId}`;
    console.log("fetchDoctorsDetailsById URL:", doctorDataUrl);

    const doctorDataResponse = await fetch(doctorDataUrl);
    console.log("fetchDoctorDetailsById response:", doctorDataResponse);

    if (!doctorataResponse.ok) {
      throw new Error(
        `Error fetching doctor data: ${doctorDataResponse.statusText}`
      );
    }
    const doctorData = await doctorDataResponse.json();
    console.log("fetchDoctorDetailsById data:", doctorData);

    return doctorData;
  } catch (error) {
    console.error("Error in fetchDoctorDetailsById:", error);
    throw error;
  }
}

export async function fetchSensorDataByRoomId(roomId) {
  try {
    const sensorDataUrl = `${API_BASE_URL}/Sensors?roomId=${roomId}`;
    console.log("fetchSensorDataByRoomId URL:", sensorDataUrl);

    const sensorDataResponse = await fetch(sensorDataUrl);
    console.log("fetchSensorDataByRoomId response:", sensorDataResponse);

    if (!sensorDataResponse.ok) {
      throw new Error(
        `Error fetching sensor data: ${sensorDataResponse.statusText}`
      );
    }
    const sensorData = await sensorDataResponse.json();
    console.log("fetchSensorDataByRoomId data:", sensorData);

    return sensorData;
  } catch (error) {
    console.error("Error in fetchSensorDataByRoomId:", error);
    throw error;
  }
}

export async function fetchSensorLogById(sensorId) {
  try {
    const sensorLogUrl = `${API_BASE_URL}/Sensors/${sensorId}`;
    console.log("fetchSensorLogById URL:", sensorLogUrl);

    const sensorLogResponse = await fetch(sensorLogUrl);
    console.log("fetchSensorLogById response:", sensorLogResponse);

    if (!sensorLogResponse.ok) {
      throw new Error(
        `Error fetching sensor logs: ${sensorLogResponse.statusText} | URL: ${sensorLogUrl}`
      );
    }
    const sensorLogs = await sensorLogResponse.json();
    console.log("fetchSensorLogById data:", sensorLogs);

    return sensorLogs;
  } catch (error) {
    console.error("Error in fetchSensorLogById:", error);
    throw error;
  }
}
