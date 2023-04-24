// const API_BASE_URL = "https://localhost:7216";

// export async function fetchRoomDetailsById(id) {
//   const response = await fetch(`${API_BASE_URL}/Rooms/${id}`);
//   if (!response.ok) {
//     throw new Error(`Error fetching room details: ${response.statusText}`);
//   }
//   return response.json();
// }

const API_BASE_URL = "https://localhost:7216";

export async function fetchRoomDetailsById(id) {
  const response = await fetch(`${API_BASE_URL}/Rooms/${id}`);
  if (!response.ok) {
    throw new Error(`Error fetching room details: ${response.statusText}`);
  }
  return response.json();
}

export async function fetchSensorData(id){
  const response = await fetch(`${API_BASE_URL}/Sensors?roomId=${id}`);
  if (!response.ok) {
    throw new Error(`Error fetching sensor values: ${response.statusText}`);
  }
  return response.json();
}
