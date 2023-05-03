import React from "react";
import { ThemeProvider } from "@mui/material/styles";
import { Card, CardHeader, CardContent, Typography } from "@mui/material";
import logoImg from "./logo.png";
import theme from "../theme";
import "../App.css";
const Project1 = () => {
  return (
    <ThemeProvider theme={theme}>
      <img src={logoImg} alt="TravelLogo" />
      <CardHeader
        title={
          <Typography style={{ fontWeight: "bold" }} variant="h5">
            World Wide Travel Alerts
          </Typography>
        }
      />
    </ThemeProvider>
  );
};
export default Project1;

/* return (
    <ThemeProvider theme={theme}>
      <AppBar color="secondary" style={{ marginBottom: "5vh" }}>
        <Toolbar>
          <Typography variant="h6" color="inherit">
            INFO3139 - Project1
          </Typography>
        </Toolbar>
      </AppBar>
      <Card className="card">
        <img src={logoImg} alt="TravelLogo" />
         <CardHeader
          title={"World Wide Travel Alerts"}
          style={{ fontWeight: "bold" }}
        }
        <CardHeader
          title={
            <Typography style={{ fontWeight: "bold" }} variant="h5">
              World Wide Travel Alerts
            </Typography>
          }
        />
      </Card>
    </ThemeProvider>
  ); */
