import React, { useState } from "react";
import { createRequest } from "../api";

const CreateRequest = () => {
  const [requestType, setRequestType] = useState("");
  const [content, setContent] = useState("");

  const handleCreateRequest = async () => {
    try {
      await createRequest({ type: requestType, content: content });
      alert("Request created successfully");
      // Clear the input fields after successful request creation
      setRequestType("");
      setContent("");
    } catch (error) {
      console.error("Error creating request:", error);
      alert("Failed to create request");
    }
  };

  return (
    <div>
      <h2>Create Request</h2>
      <div>
        <label htmlFor="requestType">Request Type: </label>
        <select
          id="requestType"
          value={requestType}
          onChange={(e) => setRequestType(e.target.value)}
        >
          <option value="">--Select a request type--</option>
          <option value="Move">Move</option>
          <option value="Additional">Additional</option>
        </select>
      </div>
      <div>
        <label htmlFor="content">Content: </label>
        <input
          type="text"
          id="content"
          value={content}
          onChange={(e) => setContent(e.target.value)}
        />
      </div>
      <button onClick={handleCreateRequest}>Create Request</button>
    </div>
  );
};

export default CreateRequest;
