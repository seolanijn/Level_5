import React, { useState, useReducer, useEffect, useRef } from "react";
import { ThemeProvider } from "@mui/material/styles";
import {
  Card,
  CardHeader,
  CardContent,
  Snackbar,
  TextField,
  Button,
  Typography,
} from "@mui/material";
import theme from "../theme";
import "../App.css";
import io from "socket.io-client";

const Lab15 = (props) => {
  const initialState = {
    msg: "",
    roomMsg: "",
    isOff: true,
    name: "",
    room: "",
    snackmsg: "",
  };
  const reducer = (state, newState) => ({ ...state, ...newState });
  const [state, setState] = useReducer(reducer, initialState);
  const [contactServer, setcontactServer] = useState(false);
  /*  const effectRan = useRef(false);
  useEffect(() => {
    if (effectRan.current) return; // React 18 Strictmode runs useEffects twice in development`
    effectRan.current = true;
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []); */
  const serverConnect = () => {
    try {
      // connect to server locally
      const socket = io.connect("localhost:5000", {
        forceNew: true,
        transports: ["websocket"],
        autoConnect: true,
        reconnection: false,
        timeout: 5000,
      });
      socket.onerror = (error) => {
        return;
      };
      socket.emit("join", { name: state.name, room: state.room }, (err) => {});
      socket.on("welcome", onWelcome);
      socket.on("newclient", newClientJoined);
      setState({ socket: socket.io._readyState });
      if (socket.io._readyState === "opening") {
        setState({
          snackmsg: "can't get connection - try later!",
        });
      }
    } catch (err) {
      console.log(err);
      setState({ snackmsg: "some other problem occurred" });
    }
  };
  const onWelcome = (welcomeMsgFromServer) => {
    setState({ snackmsg: welcomeMsgFromServer });
  };
  const newClientJoined = (joinMsgFromServer) => {
    setState({ roomMsg: joinMsgFromServer });
  };
  const onClose = (event, reason) => {
    if (reason === "clickaway") {
      return;
    }
    setcontactServer(false);
  };
  const onClick = () => {
    serverConnect();
    setcontactServer(true);
  };
  return (
    <ThemeProvider theme={theme}>
      <Card className="card">
        <CardHeader
          title="Lab 15 - Socket.io"
          style={{ textAlign: "center" }}
        />
        <CardContent>
          <TextField
            type="text"
            name="name"
            placeholder="user's name"
            onChange={(e) => {
              setState({
                name: e.target.value,
              });
            }}
            //value={state.name}
            style={{ marginBottom: "2vh", minWidth: "85%" }}
          ></TextField>
          <TextField
            type="text"
            name="room"
            placeholder="room name"
            onChange={(e) => {
              setState({
                room: e.target.value,
                isOff: false,
              });
            }}
            //value={state.room}
            style={{ marginBottom: "2vh", minWidth: "85%" }}
          ></TextField>
          <Button
            onClick={onClick}
            style={{
              backgroundColor: state.isOff ? "grey" : "green",
              color: "#fff",
            }}
            disabled={state.isOff}
          >
            Join
          </Button>
        </CardContent>
      </Card>
      <Typography>{state.roomMsg}</Typography>
      <Snackbar
        open={contactServer}
        message={state.snackmsg}
        autoHideDuration={2000}
        onClose={onClose}
      />
    </ThemeProvider>
  );
};
export default Lab15;
