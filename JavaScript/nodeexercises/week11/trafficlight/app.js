import { port } from "./config.js";
import express from "express";
import http from "http";
import { Server } from "socket.io";
const streetLights = [
  { streetName: "Seolan", red: 7500, green: 12000, yellow: 3000 },
  { streetName: "Jin", red: 5000, green: 8000, yellow: 2000 },
  { streetName: "Info3139", red: 6000, green: 9000, yellow: 2000 },
];
const app = express();
app.use(express.urlencoded({ extended: true }));
app.use(express.json()); // To parse the incoming requests with JSON payloads
app.use(express.static("public"));
let httpServer = http.createServer(app);
app.get("/", (req, res) => res.send("<h1>Hello World From Express</h1>"));

// Socket.io server
const io = new Server(httpServer, {});
// main socket routine
io.on("connection", (socket) => {
  console.log("new connection established");
  // client has joined
  socket.on("join", (client) => {
    const streetLight = streetLights.find(
      (light) => light.streetName === client.street
    );
    if (streetLight !== null) {
      // use the room property to create a room
      socket.join(client.street);
      console.log(`streetname: ${client.street}`);
      console.log(`red: ${streetLight.red}`);
      console.log(`green: ${streetLight.green}`);
      console.log(`yellow: ${streetLight.yellow}`);
      // send message to joining client
      socket.emit("turnLampOn", streetLight);
    } else {
      console.log("Street name doesn't exist");
    }
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
