import React, { useState } from "react";
import { ThemeProvider } from "@mui/material/styles";
import {
  Toolbar,
  Card,
  AppBar,
  CardHeader,
  CardContent,
  Typography,
  TextField,
  Button,
} from "@mui/material";
import theme from "../theme";
import "../App.css";
const Lab11_function = () => {
  const [message, SetMessage] = useState("");
  const [word, SetWord] = useState("");
  return (
    <ThemeProvider theme={theme}>
      <AppBar color="secondary" style={{ marginBottom: "5vh" }}>
        <Toolbar>
          <Typography variant="h6" color="inherit">
            INFO3139 - Lab 11
          </Typography>
        </Toolbar>
      </AppBar>
      <Card className="card">
        <CardHeader className="cardHeader" title="Sentence Builder" />
        <CardContent>The Message is:</CardContent>
        <CardContent>{message}</CardContent>
        <TextField
          placeholder="Add Word"
          className="textfield"
          onChange={(newValue) => SetWord(newValue.target.value)}
          value={word}
        ></TextField>
        <Button
          className="buttons"
          onClick={() => {
            SetMessage(message + " " + word);
            SetWord("");
          }}
          data-testid="addbutton"
        >
          Submit
        </Button>
        <Button
          className="buttons"
          onClick={() => {
            SetMessage("");
            SetWord("");
          }}
        >
          Clear
        </Button>
      </Card>
    </ThemeProvider>
  );
};
export default Lab11_function;
