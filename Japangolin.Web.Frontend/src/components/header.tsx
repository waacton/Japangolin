import { Link, Stack, Theme, Typography } from "@mui/material";
import Logo from "./logo";
import { useState } from "react";
import { SxProps } from "@mui/system";
import packageJson from "../../package.json";

function Header() {
  const [hovering, setHovering] = useState(false);

  const normalTextStyle: SxProps = {
    color: "text.primary",
  };

  // typescript has issues with "textFillColor"
  // which seems to work, but not be typed
  // for "as const", see https://next--material-ui-docs.netlify.app/system/the-sx-prop/#typescript-usage
  // @ts-ignore
  const gradientTextStyle: SxProps<Theme> = {
    background: theme => theme.custom.gradient,
    backgroundClip: "text",
    textFillColor: "transparent",
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
