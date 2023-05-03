import React, { useReducer, useEffect } from "react";
import {
  Autocomplete,
  Button,
  CardContent,
  Typography,
  TextField,
} from "@mui/material";
import theme from "../theme";
import "../App.css";
const AdvisoryAdd = (props) => {
  const initialState = {
    country: [],
    countries: [],
    countryText: "",
    name: "",
    selectedCountry: null,
    buttonDisabled: true,
  };
  const reducer = (state, newState) => ({ ...state, ...newState });
  const [state, setState] = useReducer(reducer, initialState);
  useEffect(() => {
    fetchUsers();
  }, []);
  const fetchUsers = async () => {
    try {
      let response = await fetch("/graphql", {
        method: "POST",
        headers: {
          "Content-Type": "application/json; charset=utf-8",
        },
        body: JSON.stringify({
          query: "query { alerts {country,name,text,date,region,subregion}}",
        }),
      });
      let json = await response.json();
      setState({
        countries: json.data.alerts,
        country: json.data.alerts.map((a) => a.name),
      });
      props.onSnackbarMessage(`found ${json.data.alerts.length} countries`);
    } catch (error) {
      console.log(error);
      props.onSnackbarMessage(`problem loading server data - ${error.message}`);
    }
  };
  const onChange = (e, selectedOption) => {
    const selectedCountryObj = state.countries.find(
      (obj) => obj["name"] === selectedOption
    )["text"];
    setState({
      selectedCountry: selectedOption,
      countryText: selectedCountryObj,
      buttonDisabled: !(state.name && selectedCountryObj),
    });
  };
  const onClick = async () => {
    try {
      const utcDate = new Date();
      const offset = -5 * 60 * 60 * 1000; // EST offset from UTC in minutes
      const currentDate = new Date(utcDate.getTime() + offset)
        .toISOString()
        .slice(0, 19)
        .replace("T", " ");
      let response = await fetch("/graphql", {
        method: "POST",
        headers: {
          "Content-Type": "application/json; charset=utf-8",
        },
        body: JSON.stringify({
          query:
            "mutation($name: String, $country: String, $text: String, $date: String) {addadvisory(name: $name, country: $country, text: $text, date: $date){name, country, text, date}}",
          variables: {
            name: state.name,
            country: state.selectedCountry,
            text: state.countryText,
            date: currentDate,
          },
        }),
      });
      //let json = await response.json();
      props.onSnackbarMessage("Added advisory on " + currentDate);
    } catch (error) {
      console.log(error);
      props.onSnackbarMessage(`Problem loading server data - ${error.message}`);
    } finally {
      setState({
        name: "",
        selectedCountry: null,
        countryText: "",
        buttonDisabled: true,
      });
    }
  };
  return (
    <CardContent>
      <Typography variant="h6">Add Advisory</Typography>
      <hr
        style={{
          height: "1px",
          backgroundColor: "#500000",
          marginBottom: "3vh",
        }}
      />
      <TextField
        type="text"
        id="myInput"
        name="name"
        placeholder="traveler's name"
        onChange={(e) => {
          setState({
            name: e.target.value,
          });
        }}
        value={state.name}
        style={{ marginBottom: "2vh", minWidth: "85%" }}
      ></TextField>
      <Autocomplete
        id="contries"
        value={state.selectedCountry}
        options={state.country}
        getOptionLabel={(option) => option}
        onChange={onChange}
        renderInput={(params) => (
          <TextField
            {...params}
            label="countries"
            variant="outlined"
            fullWidth
          />
        )}
        style={{ maxWidth: "85%", margin: "0 auto 2vh auto" }}
      />
      <Button
        onClick={onClick}
        style={{
          backgroundColor: state.buttonDisabled
            ? "#d9d9d9"
            : theme.palette.text.secondary,
          color: "#fff",
        }}
        disabled={state.buttonDisabled}
      >
        Add Advisory
      </Button>
    </CardContent>
  );
};
export default AdvisoryAdd;
