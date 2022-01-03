import { Box, Typography } from "@mui/material";
import { Theme } from "@mui/material/styles";
import { Fragment } from "react";
import { SxProps } from "@mui/system";

interface Props {
  label: "word" | "inflection";
  text: string;
  selected?: boolean;
  onSelect?: () => void;
}

function WordOrInflection(props: Props) {
  /*
   * notes on gradient borders (with radius): https://codyhouse.co/nuggets/css-gradient-borders
   * essentially, background has 2 "background images" (gradients are images, not colours)
   * 1. a "fake" background-looking gradient, takes up padding box (everything but border)
   * 2. the desired border gradient, takes up border box (everything including border)
   * the fake gradient sits on top of the desired gradient, masking everything except the border
   * */

  const boxHoverStyle: SxProps<Theme> = props.selected
    ? {
        background: (theme) => theme.custom.gradient,
      }
    : {
        background: (theme: Theme) =>
          "linear-gradient(#FAFAFA, #FAFAFA) padding-box, " + theme.custom.gradient + " border-box",
      };

  const boxStyle: SxProps<Theme> = {
    width: "fit-content",
    lineHeight: "unset",
    padding: 0.5,
    border: "1px solid transparent",
    borderRadius: 1,
    background: (theme) => (props.selected ? theme.custom.gradient : "transparent"),
    color: props.selected ? "white" : "text.primary",
    cursor: "pointer",
    "&:hover": boxHoverStyle,
  };

  return (
    <Fragment>
      <Typography
        variant={"overline"}
        sx={{
          fontSize: (theme: Theme) => theme.custom.labelTextSize,
          fontWeight: "medium",
          opacity: 0.5,
          lineHeight: "unset",
          paddingLeft: 0.5,
        }}
      >
        {props.label}
      </Typography>

      <Box sx={boxStyle} onClick={props.onSelect}>
        <Typography variant={"body1"}>{props.text}</Typography>
      </Box>
    </Fragment>
  );
}

export default WordOrInflection;
