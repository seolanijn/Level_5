import { useEffect, useRef } from "react";
import { ListItem } from "@mui/material";
import Bubble from "./bubble";
import Triangle from "./triangle";
const UserMessage = (props) => {
  const userRef = useRef(null);
  useEffect(() => {
    userRef.current.scrollIntoView(true);
  }, []);
  return (
    <div>
      <ListItem
        ref={userRef}
        style={{ textAlign: "left", marginBottom: "2vh" }}
      >
        <Bubble
          msg={props.msg}
          room={props.room}
          isThisUser={props.isThisUser}
        />
        <Triangle color={props.msg.colour} isThisUser={props.isThisUser} />
      </ListItem>
      <p></p>
    </div>
  );
};
export default UserMessage;
