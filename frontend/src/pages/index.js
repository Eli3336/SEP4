// import React, { useEffect, useState } from "react";
// import jwtDecode from "jwt-decode"; // Import jwt-decode library
// import Login from "@/components/Login";
// import { useRouter } from "next/router"; // Import useRouter hook for redirection

// const HomePage = () => {
//   const router = useRouter(); // Initialize useRouter hook
//   const [authToken, setAuthToken] = useState(null);

//   // useEffect will run once the component is mounted
//   useEffect(() => {
//     // This code will run once the component is mounted, in the browser
//     setAuthToken(localStorage.getItem("authToken"));
//   }, []);

//   useEffect(() => {
//     // Redirect based on authToken presence and user role
//     if (authToken) {
//       // Decode the JWT to extract the user role
//       const decodedToken = jwtDecode(authToken);
//       const userRole = decodedToken.role; // adjust this line based on how the role is stored in your JWT

//       // Redirect to different pages based on user role
//       if (userRole === "Admin") {
//         router.push("/room-management"); // Redirect to the admin page
//       } else if (userRole === "Doctor") {
//         router.push("/doctor"); // Redirect to the doctor page
//       } else if (userRole === "Receptionist") {
//         router.push("/assign-room"); // Redirect to the receptionist page
//       } else if (userRole === "User") {
//         router.push("/user"); // Redirect to the user page
//       } else {
//         console.log("Invalid user role: " + userRole);
//       }
//     }
//   }, [authToken, router]);

//   // Display login form if no authToken is present
//   if (!authToken) {
//     return <Login />;
//   }

//   return null; // Return null as nothing should be rendered after redirection
// };

// export default HomePage;
import React, { useState } from "react";
import { useRouter } from "next/router";

const LoginPage = () => {
  const router = useRouter();
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const handleLogin = () => {
    setIsLoggedIn(true);
    router.push("/assign-room");
  };

  if (isLoggedIn) {
    router.push("/assign-room");
    return null;
  }

  return (
    <div className="min-h-screen bg-gray-100 flex justify-center items-center">
      <div className="flex flex-col items-center">
        <input
          type="text"
          placeholder="Username"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
          className="px-4 py-2 mb-4 border border-gray-300 rounded text-black bg-white"
        />
        <input
          type="password"
          placeholder="Password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          className="px-4 py-2 mb-4 border border-gray-300 rounded text-black bg-white"
        />
        <button
          className="px-4 py-2 bg-blue-500 text-white rounded"
          onClick={handleLogin}
        >
          Login
        </button>
      </div>
    </div>
  );
};

export default LoginPage;
