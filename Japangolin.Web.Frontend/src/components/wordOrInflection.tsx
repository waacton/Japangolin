import { Box, Typography } from "@mui/material";
import { Theme } from "@mui/material/styles";
import { Fragment } from "react";
import { SxProps } from "@mui/system";
import UppercaseLabel from "./uppercaseLabel";
import { gradientBorderStyle, gradientFillStyle } from "../utils/gradientHelper";

interface Props {
  label: "word" | "inflection";
  text: string;
  selected?: boolean;
  onSelect?: () => void;
}

function WordOrInflection(props: Props) {
  const hoverStyle = props.selected ? gradientFillStyle() : gradientBorderStyle();
  // @ts-ignore - I don't know why "border" isn't a known type here
  const border = hoverStyle!.border;

  const boxStyle: SxProps<Theme> = {
    width: "fit-content",
    lineHeight: "unset",
    padding: 0.5,
    border: border,
    borderRadius: 1,
    background: (theme) => (props.selected ? theme.custom.gradient : "transparent"),
    color: props.selected ? "white" : "text.primary",
    cursor: "pointer",
    "&:hover": hoverStyle,
  };

  return (
    <Fragment>
      <UppercaseLabel sx={{ marginLeft: 0.5 }}>{props.label}</UppercaseLabel>

      <Box sx={boxStyle} onClick={props.onSelect}>
        <Typography variant={"body1"}>{props.text}</Typography>
      </Box>
    </Fragment>
  );
}

export default WordOrInflection;
