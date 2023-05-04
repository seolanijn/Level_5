import React, { useReducer, useEffect } from "react";
import {
  Autocomplete,
  TableContainer,
  Table,
  TableHead,
  TableBody,
  TableRow,
  TableCell,
  Paper,
  RadioGroup,
  FormControlLabel,
  Radio,
  CardContent,
  Typography,
  TextField,
} from "@mui/material";
import "../App.css";
const AdvisoryList = (props) => {
  const initialState = {
    radioValue: "travelers",
    values: [],
    autoCompletePlaceHolder: "",
    rows: [],
  };
  const reducer = (state, newState) => ({ ...state, ...newState });
  const [state, setState] = useReducer(reducer, initialState);
  useEffect(() => {
    onRadioButtonChange();
  }, []);
  const fetchValues = async (query) => {
    try {
      let valuesResponse = await fetch("/graphql", {
        method: "POST",
        headers: {
          "Content-Type": "application/json; charset=utf-8",
        },
        body: JSON.stringify({
          query: `query { ${query} }`,
        }),
      });
      return await valuesResponse.json();
    } catch (error) {
      console.log(error);
      props.onSnackbarMessage(`problem loading server data - ${error.message}`);
    }
  };
  const fetchResults = async (variable) => {
    try {
      let resultResponse;
      if (state.radioValue === "travelers") {
        resultResponse = await fetch("/graphql", {
          method: "POST",
          headers: {
            "Content-Type": "application/json; charset=utf-8",
          },
          body: JSON.stringify({
            query:
              "query ($name: String) { advisoriesforname(name: $name) { name, country, text, date, }}",
            variables: {
              name: variable,
            },
          }),
        });
        const travelersresult = await resultResponse.json();
        return travelersresult.data.advisoriesforname;
      } else if (state.radioValue === "regions") {
        resultResponse = await fetch("/graphql", {
          method: "POST",
          headers: {
            "Content-Type": "application/json; charset=utf-8",
          },
          body: JSON.stringify({
            query: `query ($region: String) {
                      alertsforregion(region: $region) {
                        country,
                        name,
                        text,
                        date,
                        region,
                        subregion
                      }
                    }`,
            variables: {
              region: variable,
            },
          }),
        });
        const regionsresult = await resultResponse.json();
        return regionsresult.data.alertsforregion;
      } else {
        resultResponse = await fetch("/graphql", {
          method: "POST",
          headers: {
            "Content-Type": "application/json; charset=utf-8",
          },
          body: JSON.stringify({
            query: `query ($subregion: String) {
                      alertsforsubregion(subregion: $subregion) {
                        country,
                        name,
                        text,
                        date,
                        region,
                        subregion
                      }
                    }`,
            variables: {
              subregion: variable,
            },
          }),
        });
        const subregionsresult = await resultResponse.json();
        return subregionsresult.data.alertsforsubregion;
      }
    } catch (error) {
      console.log(error);
      props.onSnackbarMessage(`problem loading server data - ${error.message}`);
    }
  };
  const onAutoCompleteChange = async (e, selectedOption) => {
    if (selectedOption == null) {
      setState({ rows: [] });
      return;
    }
    let selectedData = await fetchResults(selectedOption);
    let tempRows;
    if (state.radioValue === "travelers") {
      tempRows = selectedData.map((value, index) => {
        return {
          id: index + 1,
          country: value.country,
          info: value.text,
          date: value.date,
        };
      });
    } else {
      tempRows = selectedData.map((value, index) => {
        return {
          key: index + 1,
          country: value.name,
          info: value.text,
          date: value.date == "" ? "N/A" : value.date,
        };
      });
    }
    setState({ rows: tempRows });
    props.onSnackbarMessage(
      `found ${tempRows.length} alert(s) for ${selectedOption}`
    );
  };
  const onRadioButtonChange = async (event) => {
    let val;
    if (event === undefined) val = "travelers";
    else val = event.target.value;
    setState({ radioValue: val });
    let json = await fetchValues(val);
    let tempVals = json.data[val];
    setState({
      values: tempVals,
      autoCompletePlaceHolder: val,
    });
    setState({ rows: [] });
    props.onSnackbarMessage(`found ${tempVals.length} ${val}`);
  };
  const labelStyle = {
    marginRight: "0px",
    fontSize: "11px",
  };
  return (
    <CardContent>
      <Typography variant="h6">List Advisories By</Typography>
      <RadioGroup
        value={state.radioValue}
        onChange={onRadioButtonChange}
        style={{ display: "flex", flexDirection: "row", marginBottom: "1vh" }}
      >
        <FormControlLabel
          value="travelers"
          control={<Radio />}
          label={<Typography style={labelStyle}>Traveler</Typography>}
        />
        <FormControlLabel
          value="regions"
          control={<Radio />}
          label={<Typography style={labelStyle}>Region</Typography>}
        />
        <FormControlLabel
          value="subregions"
          control={<Radio />}
          label={<Typography style={labelStyle}>Sub-Region</Typography>}
        />
      </RadioGroup>
      <Autocomplete
        id="contries"
        value={state.selectedCountry}
        options={state.values}
        getOptionLabel={(option) => option}
        onChange={onAutoCompleteChange}
        renderInput={(params) => (
          <TextField
            {...params}
            label={state.autoCompletePlaceHolder}
            variant="outlined"
            fullWidth
          />
        )}
        style={{ marginBottom: "1vh" }}
      />
      <div
        style={{
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
        }}
      >
        <TableContainer
          component={Paper}
          style={{
            height: "300px",
            overflowY: "auto",
          }}
        >
          <Table aria-label="simple table">
            <TableHead>
              <TableRow>
                <TableCell style={{ fontWeight: "bold", fontSize: "13px" }}>
                  Country
                </TableCell>
                <TableCell style={{ fontWeight: "bold", fontSize: "13px" }}>
                  Advisory
                </TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {state.rows.map((row) => (
                <>
                  <TableRow key={row.key}>
                    <TableCell
                      rowSpan={2}
                      style={{
                        fontSize: "12px",
                      }}
                    >
                      {row.country}
                    </TableCell>
                    <TableCell
                      style={{
                        fontSize: "12px",
                      }}
                    >
                      {row.info}
                    </TableCell>
                  </TableRow>
                  <TableRow>
                    <TableCell
                      style={{
                        fontSize: "12px",
                      }}
                    >
                      {row.date}
                    </TableCell>
                  </TableRow>
                </>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      </div>
    </CardContent>
  );
};
export default AdvisoryList;
