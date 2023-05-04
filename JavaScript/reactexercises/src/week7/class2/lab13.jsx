import React, { useState, useReducer, useEffect } from "react";
import { ThemeProvider } from "@mui/material/styles";
import {
  Autocomplete,
  Card,
  CardHeader,
  CardContent,
  Typography,
  Snackbar,
  TextField,
} from "@mui/material";
import theme from "../../theme";
import "../../App.css";
const Lab13 = () => {
  //const [selection, setSelection] = useState(" ");
  const initialState = {
    msg: "",
    snackBarMsg: "",
    contactServer: false,
    names: "",
    users: [],
  };
  const reducer = (state, newState) => ({ ...state, ...newState });
  const [state, setState] = useReducer(reducer, initialState);
  useEffect(() => {
    fetchUsers();
  }, []);
  const fetchUsers = async () => {
    try {
      setState({
        contactServer: true,
        snackBarMsg: "Attempting to load users from server...",
      });
      let response = await fetch("http://localhost:5000/graphql", {
        method: "POST",
        headers: {
          "Content-Type": "application/json; charset=utf-8",
        },
        body: JSON.stringify({ query: "query { users{name,age,email} }" }),
      });
      let json = await response.json();
      setState({
        snackBarMsg: `User data loaded`,
        users: json.data.users,
        contactServer: true,
        names: json.data.users.map((a) => a.name),
      });
    } catch (error) {
      console.log(error);
      setState({
        msg: `Problem loading server data - ${error.message}`,
      });
    }
  };
  const onChange = (e, selectedOption) => {
    let userEmail = state.users.find((obj) => obj["name"] === selectedOption)[
      "email"
    ];

    selectedOption
      ? setState({
          msg: `You selected ${selectedOption}. This user can be contacted at ${userEmail}`,
        })
      : setState({ msg: `Something went wrong` });
  };
  const snackbarClose = (event, reason) => {
    if (reason === "clickaway") {
      return;
    }
    setState({
      msg: `${state.users.length} users loaded`,
      contactServer: false,
    });
  };
  return (
    <ThemeProvider theme={theme}>
      <Card className="card">
        <CardHeader
          title="Lab 13 - Search For User"
          style={{ textAlign: "center" }}
        />
        <CardContent>
          <Autocomplete
            id="users"
            options={state.names}
            getOptionLabel={(option) => option}
            onChange={onChange}
            renderInput={(params) => (
              <TextField
                {...params}
                label="available users"
                variant="outlined"
                fullWidth
              />
            )}
          />
          <div>
            <Typography color="error">{state.msg}</Typography>
          </div>
        </CardContent>
      </Card>
      <Snackbar
        open={state.contactServer}
        message={state.snackBarMsg}
        autoHideDuration={5000}
        onClose={snackbarClose}
      />
    </ThemeProvider>
  );
};
export default Lab13;
