import { port } from "./config.js";
import express from "express";
import http from "http";
import { Server } from "socket.io";
import {
  handleRoomName,
  handleJoin,
  handleDisconnect,
  handleTyping,
  handleMessage,
  handleGetRoomsAndUsers,
} from "./socketHandlers.js";
const app = express();
app.use(express.urlencoded({ extended: true }));
app.use(express.json()); // To parse the incoming requests with JSON payloads
app.use(express.static("public"));
let httpServer = http.createServer(app);
app.get("/", (req, res) => res.send("<h1>Hello World From Express</h1>"));

//const socketHandlers = require("./socketHandlers");

// set EST
process.env.TZ = "America/New_York";
// Socket.io server
const io = new Server(httpServer, {});
// main socket routine
io.on("connection", (socket) => {
  console.log("new connection established");
  // loads all existing rooms
  socket.on("someonetryngtologin", () => {
    handleRoomName(socket);
  });
  // client has joined
  socket.on("join", (client) => {
    handleJoin(socket, client);
    handleGetRoomsAndUsers(io);
  });
  // scenario 2 - client disconnects from server
  socket.on("disconnect", () => {
    handleDisconnect(socket);
    handleGetRoomsAndUsers(io);
  });
  // scenario 3 - client sends notification that user started typing
  socket.on("typing", (client) => {
    handleTyping(socket, client);
  });
  // scenario 4 - client sends message to room including self
  socket.on("message", (client) => {
    handleMessage(io, socket, client);
  });
});
// will pass 404 to error handler
app.use((req, res, next) => {
  const error = new Error("No such route found");
  error.status = 404;
  next(error);
});
// error handler middleware
app.use((error, req, res, next) => {
  res.status(error.status || 500).send({
    error: {
      status: error.status || 500,
      message: error.message || "Internal Server Error",
    },
  });
});
httpServer.listen(port, () => {
  console.log(`listening on port ${port}`);
});
