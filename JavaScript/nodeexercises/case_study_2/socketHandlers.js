//import matColours from "./matdes100colours.json" assert { type: "json" };
import moment from "moment";
import { readFile } from "fs/promises";
const clients = [];
const allrooms = [];

const loadColours = async () => {
  let rawData = await readFile("./matdes100colours.json"); // returns promise
  return JSON.parse(rawData);
};
const handleRoomName = (socket) => {
  allrooms.length = 0;
  for (let i = 0; i < clients.length; i++) {
    if (!allrooms.includes(clients[i].room)) {
      allrooms.push(clients[i].room);
    }
  }
  if (!allrooms.includes("main")) {
    allrooms.push("main");
  }
  socket.emit("allroomnames", allrooms);
};

let matColours = await loadColours();
let admincoloridx = Math.floor(Math.random() * matColours.colours.length) + 1;
const admincolor = matColours.colours[admincoloridx];
const handleJoin = (socket, client) => {
  let coloridx = Math.floor(Math.random() * matColours.colours.length) + 1;
  //socket.emit("generatecolour", matColours.colours[coloridx]);
  socket.name = client.chatName;
  socket.room = client.roomName;
  socket.colour = matColours.colours[coloridx];
  socket.join(client.roomName);
  let isInUse = isNameInRoom(client);
  if (!isInUse) {
    socket.emit("welcome", {
      text: `Welcome to ${client.roomName} room, ${socket.name}!`,
      colour: admincolor,
      timestamp: moment().format("h:mm:ss a"),
      name: "Admin",
    });
    clients.push({
      name: client.chatName,
      room: client.roomName,
      colour: matColours.colours[coloridx],
    });
    socket.to(client.roomName).emit("someonejoined", {
      text: `${socket.name} has joined this room ${client.roomName}`,
      colour: admincolor,
      timestamp: moment().format("h:mm:ss a"),
      name: "Admin",
    });
  } else {
    socket.emit("nameexists", "You cannot join with that name.");
  }
};

const isNameInRoom = (obj) => {
  for (let i = 0; i < clients.length; i++) {
    if (clients[i].name === obj.chatName && clients[i].room === obj.roomName) {
      return true;
    }
  }
  return false;
};

const handleDisconnect = (socket) => {
  for (let i = 0; i < clients.length; i++) {
    if (clients[i].name === socket.name && clients[i].room === socket.room) {
      clients.splice(i, 1);
    }
  }
  socket.to(socket.room).emit("someoneleft", {
    text: `${socket.name} has left this room ${socket.room}`,
    colour: admincolor,
    timestamp: moment().format("h:mm:ss a"),
    name: "Admin",
  });
};

const handleTyping = (socket, client) => {
  socket.to(socket.room).emit("someoneistyping", {
    from: client.from,
    text: `${client.from} is typing...`,
  });
};

const handleMessage = (io, socket, client) => {
  // send to everyone in the same room including sender
  io.in(socket.room).emit("newmessage", {
    text: client.text,
    colour: socket.colour,
    timestamp: moment().format("h:mm:ss a"),
    name: socket.name,
  });
};

const handleGetRoomsAndUsers = (io) => {
  // send to everyone in the same room including sender
  io.emit(
    "allmembers",
    clients.map((client, index) => {
      return { name: client.name, colour: client.colour, room: client.room };
    })
  );
};

export {
  handleRoomName,
  handleJoin,
  handleDisconnect,
  handleTyping,
  handleMessage,
  handleGetRoomsAndUsers,
};
