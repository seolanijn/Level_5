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
  Autocomplete,
  Dialog,
  DialogTitle,
  DialogContent,
  IconButton,
} from "@mui/material";
import theme from "../theme";
import "../App.css";
import io from "socket.io-client";
import RememberMeOutlinedIcon from "@mui/icons-material/RememberMeOutlined";
import logoImg from "./chatLogo.png";
import TopBar from "./topbar";
import UserMessage from "./usermessage";

const CaseStudy = () => {
  const initialState = {
    messages: [],
    rooms: [],
    users: [],
    nameStatus: "enter a chat name",
    roomStatus: "choose a room name",
    roomnameStatus: "enter a room name",
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
  const [showTextField, setShowTextField] = useState(false);
  const [open, setOpen] = useState(false);
  const handleOpenDialog = () => setOpen(true);
  const handleCloseDialog = () => setOpen(false);

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
      /* const socket = io.connect("localhost:5000", {
        forceNew: true,
        transports: ["websocket"],
        autoConnect: true,
        reconnection: false,
        timeout: 5000,
      }); */
      const socket = io.connect();
      socket.onerror = (error) => {
        return;
      };
      setState({ socket: socket });

      socket.emit("someonetryngtologin");
      socket.on("allroomnames", addRoomToList);
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
  // generic handler for all other messages:
  const addUserToList = (users) => {
    //if (isDuplicate(msg)) return;
    setState({
      users: users,
    });
  };
  const addRoomToList = (roomnames) => {
    //if (isDuplicate(roomname)) return;
    /* let rooms = [];
    for (let i = 0; i < state.rooms.length - 1; i++) {
      rooms.push(state.rooms[i]);
    }
    rooms.push(roomname); */
    let rooms = roomnames;
    rooms.push("< Create a room >");
    setState({
      rooms: rooms,
    });
  };
  const onExists = (msg) => {
    setState({ nameStatus: msg });
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
    setShowTextField(false);
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
    state.socket.on("allmembers", addUserToList);
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

  const onRoomChange = (e, selectedOption) => {
    if (selectedOption === "< Create a room >") {
      setShowTextField(true);
    } else {
      setState({
        roomName: selectedOption,
      });
    }
    setState({
      roomStatus: "",
    });
  };

  return (
    <ThemeProvider theme={theme}>
      {state.showjoinfields ? (
        <>
          <AppBar>
            <Toolbar color="primary">
              <Typography variant="h6" color="inherit">
                Chat It Up! - Info 3139
              </Typography>
            </Toolbar>
          </AppBar>
          <Card className="card">
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
                error={state.nameStatus !== ""}
                helperText={state.nameStatus}
                onChange={(e) => {
                  setState({ chatName: e.target.value, nameStatus: "" });
                }}
                style={{ marginBottom: "2vh", width: "85%" }}
              ></TextField>
              <Autocomplete
                id="words"
                options={state.rooms}
                getOptionLabel={(option) => option}
                onChange={onRoomChange}
                required
                renderInput={(params) => (
                  <TextField
                    error={state.roomStatus !== ""}
                    helperText={state.roomStatus}
                    {...params}
                    label="room name"
                    variant="outlined"
                    fullWidth
                  />
                )}
                style={{
                  marginBottom: "2vh",
                  width: "85%",
                  marginLeft: "2.2vh",
                }}
              />
              {showTextField ? (
                <CardContent>
                  <TextField
                    type="text"
                    name="roomName"
                    placeholder="enter room name"
                    required
                    error={state.roomnameStatus !== ""}
                    helperText={state.roomnameStatus}
                    value={state.roomName}
                    onChange={(e) => {
                      setState({
                        roomName: e.target.value,
                        roomnameStatus: "",
                      });
                      //setShowTextField(false);
                    }}
                    style={{ marginBottom: "2vh", width: "85%" }}
                  ></TextField>
                </CardContent>
              ) : null}
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
          </Card>
        </>
      ) : null}
      {!state.showjoinfields ? (
        <>
          <TopBar viewDialog={handleOpenDialog} />
          <Dialog open={open} onClose={handleCloseDialog} fullWidth={true}>
            <DialogTitle style={{ textAlign: "center", fontWeight: "bold" }}>
              Who's On?
            </DialogTitle>
            {state.users.map((user, index) => (
              <>
                <DialogContent>
                  <div
                    style={{
                      display: "flex",
                      //justifyContent: "center",
                      alignItems: "center",
                    }}
                  >
                    <IconButton>
                      <RememberMeOutlinedIcon
                        style={{
                          color: user.colour,
                          paddingRight: 20,
                        }}
                      />
                    </IconButton>
                    <Typography style={{ fontSize: 13 }}>
                      {user.name} is in room {user.room}
                    </Typography>
                  </div>
                </DialogContent>
              </>
            ))}
          </Dialog>
          <div
            className="card"
            style={{
              height: "590px",
              overflowY: "auto",
              paddingTop: 0,
            }}
          >
            <div className="scenario-container">
              {state.messages.map((message, index) => (
                <UserMessage
                  msg={message}
                  room={state.roomName}
                  isThisUser={message.name === state.chatName}
                  key={index}
                />
              ))}
            </div>
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
              style={{
                display: "flex",
                justifyContent: "flex-end",
                marginTop: "20px",
                width: "250px",
                position: "absolute",
                bottom: 65,
              }}
            />
            <div>
              <Typography
                color="primary"
                style={{ textAlign: "left", position: "absolute", bottom: 40 }}
              >
                {state.typingMsg}
              </Typography>
            </div>
          </div>
        </>
      ) : null}
    </ThemeProvider>
  );
};
export default CaseStudy;
