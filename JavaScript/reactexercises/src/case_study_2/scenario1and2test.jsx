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

const Scenario2Test = () => {
  const initialState = {
    messages: [],
    status: "",
    showjoinfields: true,
    chatName: "",
    roomName: "",
  };
  const reducer = (state, newState) => ({ ...state, ...newState });
  const [state, setState] = useReducer(reducer, initialState);
  const [contactServer, setcontactServer] = useState(false);
  const effectRan = useRef(false);
  useEffect(() => {
    if (effectRan.current) return; // React 18 Strictmode runs useEffects twice in development`
    serverConnect();
    effectRan.current = true;
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

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
      setState({ socket: socket });
    } catch (err) {
      console.log(err);
      setState({ snackmsg: "some other problem occurred" });
    }
  };
  // generic handler for all other messages:
  const addMessageToList = (msg) => {
    let messages = state.messages; // declared earlier in reducer or state hook
    messages.push(msg);
    setState({
      messages: messages,
      showjoinfields: false,
    });
  };
  const onExists = (msg) => {
    setState({ status: msg });
  };

  // handler for join button click
  const handleJoin = () => {
    state.socket.emit("join", {
      chatName: state.chatName,
      roomName: state.roomName,
    });
    state.socket.on("nameexists", (msg) => {
      setState({ status: msg });
    });
    state.socket.on("welcome", (msg) => {
      let isFound = false;
      let messages = state.messages; // declared earlier in reducer or state hook
      for (let i = 0; i < state.messages.length; i++) {
        if (msg === state.messages[i]) {
          isFound = true;
          break;
        }
      }
      if (!isFound) {
        messages.push(msg);
        setState({
          messages: messages,
          showjoinfields: false,
        });
      }
    });
    state.socket.on("someonejoined", (msg) => {
      let messages = state.messages; // declared earlier in reducer or state hook
      messages.push(msg);
      setState({
        messages: messages,
        showjoinfields: false,
      });
    });
    state.socket.on("someoneleft", (msg) => {
      let messages = state.messages; // declared earlier in reducer or state hook
      messages.push(msg);
      setState({
        messages: messages,
        showjoinfields: false,
      });
    });
  };

  return (
    <ThemeProvider theme={theme}>
      <Card className="card">
        <CardHeader
          title="Lab 18 - Scenarios 1 and 2 Test"
          style={{ textAlign: "center" }}
        />
        {state.showjoinfields ? (
          <CardContent>
            <TextField
              type="text"
              name="chatName"
              placeholder="Enter unique name"
              autoFocus={true}
              required
              value={state.chatName}
              error={state.status !== ""}
              helperText={state.status}
              onChange={(e) => {
                setState({ chatName: e.target.value, status: "" });
              }}
              style={{ marginBottom: "2vh", minWidth: "85%" }}
            ></TextField>
            <TextField
              type="text"
              name="roomName"
              placeholder="room name"
              required
              value={state.roomName}
              onChange={(e) => {
                setState({
                  roomName: e.target.value,
                });
              }}
              style={{ marginBottom: "2vh", minWidth: "85%" }}
            ></TextField>
            <Button
              variant="contained"
              data-testid="submit"
              color="primary"
              onClick={() => handleJoin()}
              disabled={state.chatName === "" || state.roomName === ""}
            >
              Join
            </Button>
          </CardContent>
        ) : null}
        {!state.showjoinfields ? (
          <CardContent>
            <Typography style={{ marginBottom: "5vw", fontWeight: "bold" }}>
              Current Messages
            </Typography>
            {state.messages.map((message, index) => (
              <Typography
                style={{ marginLeft: "-10vw", marginRight: "-10vw" }}
                key={index}
              >
                {message}
              </Typography>
            ))}
          </CardContent>
        ) : null}
      </Card>
    </ThemeProvider>
  );
};
export default Scenario2Test;
