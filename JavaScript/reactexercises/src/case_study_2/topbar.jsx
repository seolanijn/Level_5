import Accessibility from "@mui/icons-material/Accessibility";
import { AppBar, IconButton, Toolbar, Typography } from "@mui/material";
const TopBar = (props) => {
  const onIconClicked = () => props.viewDialog(); // notify the parent
  return (
    <AppBar>
      <Toolbar color="primary">
        <Typography variant="h6" color="inherit">
          Chat It Up! - Info 3139
        </Typography>
        <section style={{ height: 70, width: "auto", marginLeft: "auto" }}>
          <IconButton onClick={onIconClicked}>
            <Accessibility
              style={{
                color: "white",
                height: 60,
                width: 60,
                paddingTop: 5,
              }}
            />
          </IconButton>
        </section>
      </Toolbar>
    </AppBar>
  );
};
export default TopBar;
