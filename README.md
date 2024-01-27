# IoT Hub

IoT Hub is a full-stack application that mimics the functionality of Azure IoT Hub. It consists of a C# .NET Core 6.0 backend and a React frontend. The backend is containerized with Docker, along with MQTTNet for message queuing.

## Table of Contents

- [IoT Hub](#iot-hub)
  - [Description](#description)
  - [Project Structure](#project-structure)
  - [Prerequisites](#prerequisites)
  - [Getting Started](#getting-started)
    - [Backend](#backend)
    - [Frontend](#frontend)
  - [Usage](#usage)

## Description

IoT Hub is a versatile platform for managing Internet of Things (IoT) devices and data. It provides the core functionalities of Azure IoT Hub, making it suitable for IoT application development, experimentation, and testing.

## Project Structure

The project is organized as follows:

- `iot-hub-backend`: Contains the C# .NET Core 6.0 backend.
- `iot-hub-frontend`: Holds the React frontend.
- Docker: The backend and RabbitMQ are containerized using Docker.

## Prerequisites

Before you begin, ensure you have the following prerequisites installed:

- Docker
- .NET Core 6.0 SDK
- Node.js
- npm or yarn

## Getting Started

To get the project up and running, follow these steps:

### Backend

1. Navigate to the `iot-hub-backend` directory.
2. Build the Docker image for the backend:

   ```bash
   docker build -t iot-hub-backend .
   ```

3. Run the Docker container for the backend:

   ```bash
   docker run -p 5000:5000 iot-hub-backend
   ```

4. The backend should now be accessible at `http://localhost:3000`.

### Frontend

1. Navigate to the `iot-hub-frontend` directory.
2. Install frontend dependencies:

   ```bash
   npm install
   ```

3. Start the frontend development server:

   ```bash
   npm start
   ```

4. The frontend should be accessible at `http://localhost:3000`.

