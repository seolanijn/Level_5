import SocketClient from "./socketclient";
// note - using a functional component here
const AllRooms = () => (
  <div>
    <SocketClient name="seolan" room="geeks" />
    <SocketClient name="jin" room="nerds" />
    <SocketClient name="junyeong" room="nerds" />
    <SocketClient name="soohwan" room="geeks" />
  </div>
);
export default AllRooms;
