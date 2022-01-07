import { Box, Typography } from "@mui/material";
import { Theme } from "@mui/material/styles";
import { Fragment } from "react";
import { SxProps } from "@mui/system";
import UppercaseLabel from "./uppercaseLabel";
import { gradientBorderStyle, gradientFillStyle } from "../utils/gradientUtils";

interface Props {
  label: "word" | "inflection";
  text: string;
  selected?: boolean;
  onSelect?: () => void;
  disabled?: boolean;
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

  const disabledBoxStyle: SxProps<Theme> = {
    width: "fit-content",
    lineHeight: "unset",
    padding: 0.5,
    border: "1px solid transparent",
    borderRadius: 1,
    background: "transparent",
    opacity: 0.5,
  };

  // rgba(0, 0, 0, 0.38) taken from FormControlLabel disabled state (used in <Filter>)
  return (
    <Fragment>
      <UppercaseLabel sx={{ marginLeft: 0.5, lineHeight: "unset", color: props.disabled ? "rgba(0, 0, 0, 0.38)" : {} }}>
        {props.label}
      </UppercaseLabel>

      <Box sx={props.disabled ? disabledBoxStyle : boxStyle} onClick={props.disabled ? () => {} : props.onSelect}>
        <Typography variant={"body1"}>{props.text}</Typography>
      </Box>
    </Fragment>
  );
}

export default WordOrInflection;
