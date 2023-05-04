import "../App.css";
const Bubble = (props) => {
  return (
    <div
      className="userBubble"
      style={{
        backgroundColor: props.msg.colour,
        left: props.isThisUser ? "10vw" : "-10vw",
        width: "200px",
        minHeight: "40px",
      }}
    >
      <div style={{ fontWeight: "bold", fontSize: 11 }}>
        {props.msg.name} Says to {props.room}
      </div>
      <div style={{ fontWeight: "bold", marginBottom: "1vh", fontSize: 11 }}>
        @ {props.msg.timestamp}:
      </div>
      <div>{props.msg.text}</div>
    </div>
  );
};
export default Bubble;
