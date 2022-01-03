import { Button, Tooltip } from "@mui/material";
import VisibilityIcon from "@mui/icons-material/Visibility";
import { Fragment } from "react";
import JapaneseTypography from "./japaneseTypography";
import { gradientBorderStyle, gradientBorderWithTranslucentFillStyle } from "../utils/gradientHelper";
import { SxProps } from "@mui/system";
import { Theme } from "@mui/material/styles";

interface Props {
  children: string;
  showAnswer: boolean;
  onShowAnswer: () => void;
}

function Answer(props: Props) {
  const disabled = props.showAnswer;
  const buttonStyle: SxProps<Theme> = disabled
    ? {} // no style override when disabled
    : {
        // fancy style override when enabled
        ...gradientBorderStyle(),
        "&:hover": {
          ...gradientBorderWithTranslucentFillStyle(),
        },
      };

  const svgIconStyle: SxProps<Theme> = disabled ? {} : { fill: (theme) => theme.custom.gradientSvgFill };

  return (
    <Fragment>
      <Tooltip title={"View Answer"}>
        <Button
          variant={"outlined"}
          onClick={props.onShowAnswer}
          disabled={props.showAnswer}
          sx={{
            width: 32,
            height: 32,
            minWidth: 32,
            ...buttonStyle,
          }}
        >
          <VisibilityIcon sx={{ width: 16, height: 16, ...svgIconStyle }} />
        </Button>
      </Tooltip>

      <JapaneseTypography sx={{ fontSize: (theme) => theme.custom.subTextSize, opacity: 0.6, cursor: "help" }}>
        {props.showAnswer ? props.children : "Click to reveal the answer"}
      </JapaneseTypography>
    </Fragment>
  );
}

export default Answer;
