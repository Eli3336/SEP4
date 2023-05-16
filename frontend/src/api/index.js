import axios from "axios";

const API_BASE_URL = "https://localhost:7216";

const instance = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    Accept: "application/json",
    "Content-Type": "application/json",
  },
});

export async function fetchRoomDetailsById(roomId) {
  try {
    const roomDataUrl = `/Rooms/${roomId}`;
    console.log("fetchRoomDetailsById URL:", roomDataUrl);

    const roomDataResponse = await instance.get(roomDataUrl);

    console.log("fetchRoomDetailsById response:", roomDataResponse);

    const roomData = roomDataResponse.data;
    console.log("fetchRoomDetailsById data:", roomData);

    const sensorsWithValues = await Promise.all(
      roomData.sensors.map(async (sensor) => {
        const sensorResponse = await instance.get(`/Sensors/${sensor.id}`);
        return {
          ...sensor,
          values: sensorResponse.data.values,
        };
      })
    );

    return {
      ...roomData,
      sensors: sensorsWithValues,
    };
  } catch (error) {
    console.error("Error in fetchRoomDetailsById:", error);
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

// receptionist ------------------------------------------------------------
export async function createReceptionist(receptionistInfo) {
  try {
    const response = await instance.post(
      `/Receptionists`,
      receptionistInfo
    );
    return response.data;
  } catch (error) {
    console.error("Error in createReceptionist:", error);
    alert(`Failed to create receptionist: ${error.response.data}`);
    throw error;
  }
}

export async function getReceptionistById(receptionistId) {
  try {
    const response = await instance.get(`/Receptionists/${receptionistId}`);
    return response.data;
  } catch (error) {
    console.error("Error in getReceptionistById:", error);
    alert(`Failed to get Receptionist: ${error.response.data}`);
    throw error;
  }
}

export const updateReceptionist = async (id, name, phoneNumber) => {
  try {

    console.log(
      `Updating receotionist ${id} with name ${name} and phone number ${phoneNumber}`
    );
    const response = await instance.patch(
      `/Receptionists/${id}?name=${name}&number=${phoneNumber}`
    );
    console.log("Updated receptionist:", response.data);
    return response.data;
  } catch (error) {
    console.error("Error updating receptionist:", error);
    throw error;
  }
};

export async function deleteReceptionistById(receptionistId) {
  try {
    const response = await instance.delete(`/Receptionists/${receptionistId}`);
    return response;
  } catch (error) {
    console.error("Error in deleteReceptionistById:", error);
    alert(`Failed to delete receptionist: ${error.response.data}`);
    throw error;
  }
}


export async function createAndAddPatientToRoom(patientInfo, roomId) {
  try {
    const response = await instance.post(
      `/Patients?roomId=${roomId}`,
      patientInfo
    );
    return response.data;
  } catch (error) {
    console.error("Error in createAndAddPatientToRoom:", error);
    alert(`Failed to add patient: ${error.response.data}`);
    throw error;
  }
}

export async function deletePatientById(patientId) {
  try {
    const response = await instance.delete(`/Patients/${patientId}`);
    return response;
  } catch (error) {
    console.error("Error in deletePatientById:", error);
    alert(`Failed to delete patient: ${error.response.data}`);
    throw error;
  }
}

export async function getPatientById(patientId) {
  try {
    const response = await instance.get(`/Patients/${patientId}`);
    return response.data;
  } catch (error) {
    console.error("Error in getPatientById:", error);
    alert(`Failed to get patient: ${error.response.data}`);
    throw error;
  }
}

export const updateRoom = async (id, capacity, availability) => {
  try {
    if (availability !== "Available" && availability !== "Under maintenance") {
      throw new Error(
        'Invalid availability status. Must be "Available" or "Under maintenance"'
      );
    }

    console.log(
      `Updating room ${id} with capacity ${capacity} and availability ${availability}`
    );
    const response = await instance.patch(
      `/Rooms/${id}?capacity=${capacity}&availability=${availability}`
    );
    console.log("Updated room:", response.data);
    return response.data;
  } catch (error) {
    console.error("Error updating room:", error);
    throw error;
  }
};
