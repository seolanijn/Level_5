import SocketClient from "./trafficlight";
import "./street.css";
// note - using a functional component here
const AllRooms = () => (
  <div>
    <h2 style={{ textAlign: "center" }}>Lab 17</h2>
    <div className="flex-container">
      <SocketClient street="Seolan" />
      <SocketClient street="Jin" />
      <SocketClient street="Info3139" />
    </div>
  </div>
);
export default AllRooms;
