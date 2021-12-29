import { Link, Stack, Typography } from "@mui/material";
import Logo from "./logo";
import { useState } from "react";
import { SxProps } from "@mui/system";
import packageJson from "../../package.json";

function Header() {
  const [hovering, setHovering] = useState(false);

  // see https://next--material-ui-docs.netlify.app/system/the-sx-prop/#typescript-usage
  // for notes about the "as const" cast
  const normalTextStyle: SxProps = {
    color: "text.primary",
  } as const;

  // TODO: make gradient more accessible throughout the app
  // @ts-ignore - don't know why textFillColor causes problems
  const gradientTextStyle: SxProps = {
    background: "linear-gradient(to right, #E004DD 0%, #F63D96 100%)",
    backgroundClip: "text",
    textFillColor: "transparent",
  } as const;

  const gradientBackground: SxProps = {
    background: "linear-gradient(to right, #FFFFFF 50%, #F07AEE 75%, #E004DD 87.5%, #F63D96 100%)",
  } as const;

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
        sx={hovering ? gradientBackground : {}}
      >
        <Logo />
        <Typography sx={hovering ? gradientTextStyle : normalTextStyle}>
          Wacton.Japangolin · {packageJson.version}
        </Typography>
      </Stack>
    </Link>
  );
}

export default Header;
