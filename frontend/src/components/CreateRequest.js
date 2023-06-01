import React, { useState } from "react";
import { createRequest } from "../api";
import { Button, Form, Container } from "react-bootstrap";

const CreateRequest = () => {
  const [requestType, setRequestType] = useState("");
  const [content, setContent] = useState("");

  const handleCreateRequest = async () => {
    try {
      await createRequest({ type: requestType, content: content });
      alert("Request created successfully");

      setRequestType("");
      setContent("");
    } catch (error) {
      console.error("Error creating request:", error);
      alert("Failed to create request");
    }
  };

  return (
    <Container className="text-dark">
      <h2 className="my-3">Create Request</h2>
      <Form>
        <Form.Group controlId="requestType">
          <Form.Label>Request Type</Form.Label>
          <Form.Control
            as="select"
            value={requestType}
            onChange={(e) => setRequestType(e.target.value)}
          >
            <option value="">--Select a request type--</option>
            <option value="Move">Move</option>
            <option value="Additional">Additional</option>
          </Form.Control>
        </Form.Group>
        <Form.Group controlId="content">
          <Form.Label>Content</Form.Label>
          <Form.Control
            type="text"
            value={content}
            onChange={(e) => setContent(e.target.value)}
          />
        </Form.Group>
        <button
          className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
          onClick={handleCreateRequest}
        >
          Create Request
        </button>
      </Form>
    </Container>
  );
};

export default CreateRequest;
