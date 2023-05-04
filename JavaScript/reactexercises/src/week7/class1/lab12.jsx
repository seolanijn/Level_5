import React, { useState } from "react";
import { ThemeProvider } from "@mui/material/styles";
import {
  Autocomplete,
  Card,
  CardHeader,
  CardContent,
  Typography,
  TextField,
  AppBar,
  Toolbar,
} from "@mui/material";
import theme from "../../theme";
import "../../App.css";
const Lab12 = () => {
  const [selection, setSelection] = useState("");
  const onChange = (e, selectedOption) => {
    selectedOption
      ? setSelection(`${selection} ${selectedOption}`)
      : setSelection("");
  };
  return (
    <ThemeProvider theme={theme}>
      <Card className="card">
        <AppBar color="secondary" style={{ marginBottom: "5vh" }}>
          <Toolbar>
            <Typography variant="h6" color="inherit">
              INFO3139 - Lab 12
            </Typography>
          </Toolbar>
        </AppBar>
        <CardHeader
          title="Sentence Builder using Autocomplete"
          style={{ textAlign: "center" }}
        />
        <CardContent>
          <Autocomplete
            id="words"
            options={words}
            getOptionLabel={(option) => option}
            onChange={onChange}
            renderInput={(params) => (
              <TextField
                {...params}
                label="pick a word"
                variant="outlined"
                fullWidth
              />
            )}
          />
          <p />
          <Typography variant="h6" color="error">
            {selection}
          </Typography>
        </CardContent>
      </Card>
    </ThemeProvider>
  );
};
const words = ["Hey", "I", "built", "a", "sentence.", "Seolan Jin"];
export default Lab12;
