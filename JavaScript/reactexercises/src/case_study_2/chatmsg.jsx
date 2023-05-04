import React from "react";
import "../App.css";
const ChatMsg = (props) => {
  return (
    <div
      className="scenario-message"
      style={{ backgroundColor: props.msg.colour }}
    >
      <div style={{ fontSize: "13px" }}>
        {props.name} Says @ {props.msg.timestamp}:
      </div>
      <div style={{ fontSize: "16px" }}>{props.msg.text}</div>
    </div>
  );
};
export default ChatMsg;
