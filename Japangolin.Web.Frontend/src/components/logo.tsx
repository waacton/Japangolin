import logo from "../Japangolin-swirl.png";

import { Stack } from "@mui/material";

function Logo() {
  return (
    <Stack direction={"row"} justifyContent={"center"} alignItems={"center"} sx={{ bgcolor: "#404046", height: 40, width: 40 }}>
      <img src={logo} className="App-logo" alt="logo" height={24} />
    </Stack>
  );
}

export default Logo;
