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

/*
 * notes on gradient borders (with radius): https://codyhouse.co/nuggets/css-gradient-borders
 * essentially, background has 2 "background images" (gradients are images, not colours)
 * 1. a "fake" background-looking gradient, takes up padding box (everything but border)
 * 2. the desired border gradient, takes up border box (everything including border)
 * the fake gradient sits on top of the desired gradient, masking everything except the border
 * */

const getBorderGradientPaddingBox = (colour: string) => `linear-gradient(${colour}, ${colour}) padding-box`;
const getBorderGradientBorderBox = (gradient: string) => `${gradient} border-box`;

const gradientBorderStyle: SxProps<Theme> = {
  background: (theme: Theme) =>
    `${getBorderGradientPaddingBox(theme.custom.background)}, ${getBorderGradientBorderBox(theme.custom.gradient)}`,
};

function WordOrInflection(props: Props) {
  const boxStyle: SxProps<Theme> = {
    width: "fit-content",
    lineHeight: "unset",
    padding: 0.5,
    border: "1px solid transparent",
    borderRadius: 1,
    background: (theme) => (props.selected ? theme.custom.gradient : "transparent"),
    color: props.selected ? "white" : "text.primary",
    cursor: "pointer",
    "&:hover": props.selected ? {} : gradientBorderStyle,
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
