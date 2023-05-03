import React, { useEffect, useState } from "react";
import { useQuery } from "react-query";
import {
  TableContainer,
  Table,
  TableBody,
  TableRow,
  TableCell,
  CardContent,
  Typography,
  Paper,
} from "@mui/material";
import "../App.css";
const AlertComponent = (props) => {
  const [rows, setRows] = useState([]);
  useEffect(() => {
    onProgress();
  }, []);
  const fetchResults = async () => {
    try {
      props.onSnackbarMessage("Running setup...");
      let response = await fetch("/graphql", {
        method: "POST",
        headers: {
          "Content-Type": "application/json; charset=utf-8",
        },
        body: JSON.stringify({ query: "query { project1_setup {results}}" }),
      });
      return await response.json();
    } catch (error) {
      console.log(error);
      props.onSnackbarMessage(`problem loading server data - ${error.message}`);
    }
  };
  const onProgress = async () => {
    const data = await fetchResults();
    const resultSet = data.data.project1_setup.results;
    let resArr = [];
    resArr = resultSet.replace(/([.])\s*(?=[A-Z])/g, "$1|").split("|");
    setRows(
      resArr.map((value, index) => {
        return {
          id: index + 1,
          name: value.trim(),
        };
      })
    );
    props.onSnackbarMessage("Alerts collection setup completed");
  };
  /* const { isLoading, error, data } = useQuery("querykeyname", async () => {
    let response = await fetch("http://localhost:5000/graphql", {
      method: "POST",
      headers: {
        "Content-Type": "application/json; charset=utf-8",
      },
      body: JSON.stringify({ query: "query { project1_setup {results}}" }),
    });
    return await response.json();
  });
  if (isLoading) {
    props.onSnackbarMessage("Running setup...");
    return "Loading...";
  }
  if (error) {
    props.onSnackbarMessage("An error has occurred: " + error.message);
    return "An error has occurred";
  } */

  return (
    <CardContent style={{ margin: 0, padding: 0 }}>
      <Typography variant="h6">Alert Setup - Details</Typography>
      <hr style={{ height: "1px", backgroundColor: "#500000", width: "90%" }} />
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
            width: "90%",
          }}
        >
          <Table aria-label="simple table">
            <TableBody>
              {rows.map((row) => (
                <TableRow
                  key={row.id}
                  sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
                >
                  <TableCell
                    component="th"
                    scope="row"
                    style={{
                      fontSize: "12px",
                    }}
                  >
                    {row.name}
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      </div>
    </CardContent>
  );
};
export default AlertComponent;
