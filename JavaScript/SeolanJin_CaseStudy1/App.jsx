/* import { useState } from "react";
import reactLogo from "./assets/react.svg";
import "./App.css";

function App() {
  const [count, setCount] = useState(0);

  return (
    <div className="App">
      <div>
        <a href="https://vitejs.dev" target="_blank">
          <img src="/vite.svg" className="logo" alt="Vite logo" />
        </a>
        <a href="https://reactjs.org" target="_blank">
          <img src={reactLogo} className="logo react" alt="React logo" />
        </a>
      </div>
      <h1>Seolan Jin's React Exercises</h1>
      <div className="card">
        <button onClick={() => setCount((count) => count + 1)}>
          count is {count}
        </button>
        <p>
          Edit <code>src/App.jsx</code> and save to test HMR
        </p>
      </div>
      <p className="read-the-docs">
        Click on the Vite and React logos to learn more
      </p>
    </div>
  );
}

export default App;
 */

/* import "./App.css";
import React from "react";
class App extends React.Component {
  render() {
    return React.createElement("h1", { className: "bigred" }, "Hello World!");
  }
}
export default App; */

/* import React from "react";
import FunctionalJSX from "./week6/functional_component.jsx";
const App = () => {
  return (
    <FunctionalJSX somedata="stuff for functional component in JSX syntax" />
  );
};
export default App; */

/* import React from "react";
import ClassyComponent from "./week6/classy_component";
const App = () => <ClassyComponent />;
export default App; */

/* import React from "react";
import FunctionalStateHookComponent from "./week6/functional_statehook";
const App = () => <FunctionalStateHookComponent />;
export default App; */

/* import React from "react";
import MaterialUIEx1Component from "./week6/materialuiexample1";
const App = () => <MaterialUIEx1Component />;
export default App; */

/* import React from "react";
import Lab11_function from "./week6/lab11";
const App = () => <Lab11_function />;
export default App; */

/* import React from "react";
import MaterialUIEx2Component from "./week7/class1/materialuiexample2";
const App = () => <MaterialUIEx2Component />;
export default App;
 */

/* import React from "react";
import MaterialUIEx3Component from "./week7/class1/materialuiexample3";
const App = () => <MaterialUIEx3Component />;
export default App; */

/* import React from "react";
import MaterialUIEx4Component from "./week7/class1/materialuiexample4";
const App = () => <MaterialUIEx4Component />;
export default App; */

/* import React from "react";
import Lab12 from "./week7/class1/lab12";
const App = () => <Lab12 />;
export default App; */

/* import React from "react";
import Project1 from "./project1/project1component";
const App = () => <Project1 />;
export default App; */

/* import React, { useState } from "react";
import { Routes, Route, NavLink } from "react-router-dom";
import MenuIcon from "@mui/icons-material/Menu";
import { ThemeProvider } from "@mui/material/styles";
import theme from "./theme";
import {
  Toolbar,
  AppBar,
  Menu,
  MenuItem,
  IconButton,
  Typography,
} from "@mui/material";
import MaterialUIEx3Component from "./week7/class1/materialuiexample3";
import MaterialUIEx5Component from "./week7/class2/materialuiexample5";
import MaterialUIEx6Component from "./week7/class2/materialuiexample6";
import MaterialUIEx7a from "./week7/class2/materialuiexample7a";
import Lab13 from "./week7/class2/lab13";
const App = () => {
  const [anchorEl, setAnchorEl] = useState(null);
  const handleClose = () => {
    setAnchorEl(null);
  };
  const handleClick = (event) => {
    setAnchorEl(event.currentTarget);
  };
  return (
    <ThemeProvider theme={theme}>
      <AppBar>
        <Toolbar>
          <Typography variant="h6" color="inherit">
            INFO3139 - MaterialUI
          </Typography>
          <IconButton
            id="menubtn"
            onClick={handleClick}
            color="inherit"
            style={{ marginLeft: "auto", paddingRight: "1vh" }}
          >
            <MenuIcon />
          </IconButton>
          <Menu
            id="simple-menu"
            anchorEl={anchorEl}
            open={Boolean(anchorEl)}
            onClose={handleClose}
          >
            <MenuItem component={NavLink} to="/home" onClick={handleClose}>
              Home
            </MenuItem>
            <MenuItem component={NavLink} to="/ex3" onClick={handleClose}>
              Exercise #3
            </MenuItem>
            <MenuItem component={NavLink} to="/ex6" onClick={handleClose}>
              Exercise #6
            </MenuItem>
            <MenuItem component={NavLink} to="/ex7" onClick={handleClose}>
              Exercise #7
            </MenuItem>
            <MenuItem component={NavLink} to="/lab13" onClick={handleClose}>
              Lab 13
            </MenuItem>
          </Menu>
        </Toolbar>
      </AppBar>
      <Routes>
        <Route path="/" element={<MaterialUIEx5Component />} />
        <Route path="/home" element={<MaterialUIEx5Component />} />
        <Route path="/ex3" element={<MaterialUIEx3Component />} />
        <Route path="/ex6" element={<MaterialUIEx6Component />} />
        <Route path="/ex7" element={<MaterialUIEx7a />} />
        <Route path="/lab13" element={<Lab13 />} />
      </Routes>
    </ThemeProvider>
  );
};
export default App; */

/* import { QueryClient, QueryClientProvider } from "react-query";
import ReactQueryExample from "./week8/class1/reactqueryexample";
const queryClient = new QueryClient();
const App = () => {
  return (
    <QueryClientProvider client={queryClient}>
      <ReactQueryExample />
    </QueryClientProvider>
  );
};
export default App; */
import { QueryClient, QueryClientProvider } from "react-query";
import React, { useState } from "react";
import { Routes, Route, NavLink } from "react-router-dom";
import MenuIcon from "@mui/icons-material/Menu";
import { ThemeProvider } from "@mui/material/styles";
import theme from "./theme";
import {
  Card,
  Toolbar,
  AppBar,
  Menu,
  MenuItem,
  IconButton,
  Typography,
  Snackbar,
  CardContent,
} from "@mui/material";
import Project1 from "./project1/project1component";
import AlertComponent from "./project1/alertsetupcomponent";
import AdvisoryAdd from "./project1/advisoryaddcomponent";
import AdvisoryList from "./project1/advisorylistcomponent";
const queryClient = new QueryClient();
const App = () => {
  const [anchorEl, setAnchorEl] = useState(null);
  const [snackbarMessage, setSnackbarMessage] = useState("");
  const [contactServer, setcontactServer] = useState(false);
  const handleClose = () => {
    setAnchorEl(null);
  };
  const handleClick = (event) => {
    setAnchorEl(event.currentTarget);
  };
  const handleSnackbarMessage = (message) => {
    setSnackbarMessage(message);
    setcontactServer(true);
    setTimeout(() => {
      setcontactServer(false);
    }, 3000);
  };
  const onClose = (event, reason) => {
    if (reason === "clickaway") {
      return;
    }
    setcontactServer(false);
  };
  return (
    <ThemeProvider theme={theme}>
      <AppBar color="secondary" style={{ marginBottom: "1vh" }}>
        <Toolbar>
          <Typography variant="h6" color="inherit">
            INFO3139 - Case #1
          </Typography>
          <IconButton
            id="menubtn"
            onClick={handleClick}
            color="inherit"
            style={{ marginLeft: "auto", paddingRight: "1vh" }}
          >
            <MenuIcon />
          </IconButton>
          <Menu
            id="simple-menu"
            anchorEl={anchorEl}
            open={Boolean(anchorEl)}
            onClose={handleClose}
          >
            <MenuItem component={NavLink} to="/home" onClick={handleClose}>
              Home
            </MenuItem>
            <MenuItem component={NavLink} to="/reset" onClick={handleClose}>
              Reset Data
            </MenuItem>
            <MenuItem component={NavLink} to="/add" onClick={handleClose}>
              Add Advisory
            </MenuItem>
            <MenuItem component={NavLink} to="/list" onClick={handleClose}>
              List Advisories
            </MenuItem>
          </Menu>
        </Toolbar>
      </AppBar>
      <Card
        className="card"
        style={{
          margin: "0 !important",
          padding: "3vh 0 0 0",
          minWidth: "100%",
        }}
      >
        <Routes>
          <Route path="/" element={<Project1 />}></Route>
          <Route path="/home" element={<Project1 />}></Route>
          <Route
            path="/reset"
            element={
              <>
                <Project1 />
                <QueryClientProvider client={queryClient}>
                  <AlertComponent onSnackbarMessage={handleSnackbarMessage} />
                </QueryClientProvider>
              </>
            }
          ></Route>
          <Route
            path="/add"
            element={
              <>
                <Project1 />
                <AdvisoryAdd onSnackbarMessage={handleSnackbarMessage} />
              </>
            }
          />
          <Route
            path="/list"
            element={
              <>
                <Project1 />
                <AdvisoryList onSnackbarMessage={handleSnackbarMessage} />
              </>
            }
          />
        </Routes>
        <CardContent style={{ marginTop: "1vh" }}>
          <Typography
            color="secondary"
            style={{ float: "right", fontSize: "smaller" }}
          >
            &copy;Info3139 - 2023
          </Typography>
        </CardContent>
      </Card>
      <Snackbar
        open={contactServer}
        autoHideDuration={5000}
        onClose={onClose}
        message={snackbarMessage}
      />
    </ThemeProvider>
  );
};
export default App;

// npm run dev
