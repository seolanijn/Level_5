import React, { useState, useReducer, useEffect, useRef } from "react";
import { ThemeProvider } from "@mui/material/styles";
import {
  Card,
  CardHeader,
  CardContent,
  AppBar,
  Toolbar,
  TextField,
  Button,
  Typography,
} from "@mui/material";
import theme from "../theme";
import "../App.css";
import io from "socket.io-client";
import ChatMsg from "./chatmsg";
import logoImg from "./chatLogo.png";

const ScenarioEnhanced = () => {
  const initialState = {
    messages: [],
    status: "",
    showjoinfields: true,
    chatName: "",
    roomName: "",
    isTyping: false,
    typingMsg: "",
    message: "",
    colour: "",
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
    if (isDuplicate(msg)) return;
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

  const onTyping = (msg) => {
    if (msg.from !== state.chatName) {
      setState({
        typingMsg: msg.text,
      });
    }
  };
  const onNewMessage = (msg) => {
    if (isDuplicate(msg)) return;
    addMessageToList(msg);
    setState({ typingMsg: "" });
  };

  const isDuplicate = (msg) => {
    for (let i = 0; i < state.messages.length; i++) {
      if (msg === state.messages[i]) {
        return true;
      }
    }
    return false;
  };

  // handler for join button click
  const handleJoin = () => {
    state.socket.emit("join", {
      chatName: state.chatName,
      roomName: state.roomName,
    });
    state.socket.on("nameexists", onExists);
    state.socket.on("welcome", (msg) => {
      addMessageToList(msg);
    });
    state.socket.on("someonejoined", addMessageToList);
    state.socket.on("someoneleft", addMessageToList);
    state.socket.on("someoneistyping", onTyping);
    state.socket.on("newmessage", onNewMessage);
  };

  // keypress handler for message TextField
  const onMessageChange = (e) => {
    setState({ message: e.target.value });
    if (state.isTyping === false) {
      state.socket.emit("typing", { from: state.chatName }, (err) => {});
      setState({ isTyping: true }); // flag first byte only
    }
  };

  // enter key handler to send message
  const handleSendMessage = (e) => {
    if (state.message !== "") {
      state.socket.emit(
        "message",
        { from: state.chatName, text: state.message },
        (err) => {}
      );
      setState({ isTyping: false, message: "" });
    }
  };

  return (
    <ThemeProvider theme={theme}>
      <AppBar>
        <Toolbar color="primary">
          <Typography variant="h6" color="inherit">
            Chat It Up! - Info 3139
          </Typography>
        </Toolbar>
      </AppBar>
      <Card className="card">
        {state.showjoinfields ? (
          <>
            <img src={logoImg} alt="ChatLogo" width={80} />
            <CardHeader
              title="Sign In"
              style={{
                textAlign: "center",
                fontWeight: "bold",
                paddingTop: 0,
              }}
            />
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
          </>
        ) : null}
        {!state.showjoinfields ? (
          <CardContent>
            <TextField
              onChange={onMessageChange}
              placeholder="type something here"
              autoFocus={true}
              required
              value={state.message}
              onKeyPress={(e) => {
                if (e.key === "Enter") {
                  handleSendMessage();
                  e.target.blur();
                }
              }}
            />
            <div className="scenario-container">
              Messages in {state.roomName}
              {state.messages.map((message, index) => (
                <ChatMsg name={message.name} msg={message} key={index} />
              ))}
            </div>
            <div>
              <Typography color="primary">{state.typingMsg}</Typography>
            </div>
          </CardContent>
        ) : null}
      </Card>
    </ThemeProvider>
  );
};
export default ScenarioEnhanced;
