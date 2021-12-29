import { Link, Stack, Typography } from "@mui/material";
import Logo from "./logo";
import { useState } from "react";
import { SxProps } from "@mui/system";

function Header() {
  const [hovering, setHovering] = useState(false);

  // see https://next--material-ui-docs.netlify.app/system/the-sx-prop/#typescript-usage
  // for notes about the "as const" cast
  const normalTextStyle: SxProps = {
    color: "text.primary",
  } as const;

  // TODO: make gradient more accessible throughout the app
  const gradientTextStyle: SxProps = {
    background: "linear-gradient(to right, #E004DD 0%, #F63D96 100%)",
    backgroundClip: "text",
    WebkitTextFillColor: "transparent",
  } as const;

  const gradientBackground: SxProps = {
    background: "linear-gradient(to right, #FFFFFF 25%, #F07AEE 50%, #E004DD 75%, #F63D96 100%)",
    transition: "background-color 2s ease",
  } as const;

  return (
    <Link href={"https://gitlab.com/Wacton/Japangolin"} target={"_blank"} rel={"noreferrer"} underline={"hover"} color={"text.secondary"}>
      <Stack
        direction={"row"}
        justifyContent={"flex-start"}
        alignItems={"center"}
        spacing={2}
        onMouseEnter={() => setHovering(true)}
        onMouseLeave={() => setHovering(false)}
        sx={hovering ? gradientBackground : {}}
      >
        <Logo />
        <Typography sx={hovering ? gradientTextStyle : normalTextStyle}>Wacton.Japangolin</Typography>
      </Stack>
    </Link>
  );
}

export default Header;
