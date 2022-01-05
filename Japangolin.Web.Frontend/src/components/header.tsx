import { Link, Stack, Typography } from "@mui/material";
import Logo from "./logo";
import { useState } from "react";
import { SxProps } from "@mui/system";
import packageJson from "../../package.json";
import { gradientTextStyle } from "../utils/gradientUtils";

function Header() {
  const [hovering, setHovering] = useState(false);

  const normalTextStyle: SxProps = {
    color: "text.primary",
  };

  return (
    <Link
      href={"https://gitlab.com/Wacton/Japangolin"}
      target={"_blank"}
      rel={"noreferrer"}
      underline={"hover"}
      color={"text.secondary"}
    >
      <Stack
        direction={"row"}
        justifyContent={"flex-start"}
        alignItems={"center"}
        spacing={2}
        onMouseEnter={() => setHovering(true)}
        onMouseLeave={() => setHovering(false)}
      >
        <Logo />
        <Typography sx={hovering ? gradientTextStyle() : normalTextStyle}>
          Wacton.Japangolin · {packageJson.version}
        </Typography>
      </Stack>
    </Link>
  );
}

export default Header;
