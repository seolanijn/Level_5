import { useReducer, useEffect, useRef, useState } from "react";
import io from "socket.io-client";
import "./trafficlight.css";

const SocketClient = (props) => {
  const initialState = {
    streetLight: {},
  };
  const reducer = (state, newState) => ({ ...state, ...newState });
  const [state, setState] = useReducer(reducer, initialState);
  const effectRan = useRef(false);
  const [colour, setColor] = useState("red");
  const [status, setStatus] = useState("");
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

      // connect to server on Elastic Beanstalk
      const socket = io.connect();

      if (socket.io._readyState === "opening") setStatus("connecting...");

      socket.emit("join", { street: props.street }, (err) => {});
      socket.on("turnLampOn", async (lampData) => {
        socket.disconnect(); // don't need server anymore once we have data
        setStatus("disconnected");
        while (true) {
          // loop until browser closes
          // wait on current colour, then set next color
          await waitSomeSeconds(lampData.red, "green");
          await waitSomeSeconds(lampData.green, "yellow");
          await waitSomeSeconds(lampData.yellow, "red");
        }
      });
    } catch (err) {
      console.log(err);
      setState({ msg: "some other problem occurred" });
    }
  };

  const waitSomeSeconds = (waitTime, nextColorToIlluminate) => {
    return new Promise((resolve, reject) => {
      setTimeout(() => {
        setColor(nextColorToIlluminate); // update state variable
        resolve();
      }, waitTime);
    });
  };

  const getStateColor = (c) => (colour === c ? colour : "white");
  return (
    <div>
      <div style={{ marginBottom: "1em" }}>{status}</div>
      <div className="light">
        <div
          className="lamp"
          style={{ backgroundColor: getStateColor("red"), margin: ".5rem" }}
        />
        <div
          className="lamp"
          style={{ backgroundColor: getStateColor("yellow"), margin: ".5rem" }}
        />
        <div
          className="lamp"
          style={{ backgroundColor: getStateColor("green"), margin: ".5rem" }}
        />
        <div style={{ textAlign: "center", fontName: "Helvetica" }}>
          {props.street}
        </div>
      </div>
    </div>
  );
};
export default SocketClient;
