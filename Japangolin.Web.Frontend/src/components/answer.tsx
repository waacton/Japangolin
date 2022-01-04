import VisibilityIcon from "@mui/icons-material/Visibility";
import React, { Fragment } from "react";
import JapaneseTypography from "./japaneseTypography";
import GradientIconButton from "./gradientIconButton";
import { Box, Tooltip } from "@mui/material";

interface Props {
  children: string;
  showAnswer: boolean;
  onShowAnswer: () => void;
  disabled?: boolean;
}

function Answer(props: Props) {
  return (
    <Fragment>
      <Tooltip title={"View Answer"}>
        <Box>
          {/* see discussion in tooltipWithForwardRef.tsx for why <Box> is needed */}
          <GradientIconButton
            icon={VisibilityIcon}
            width={32}
            height={32}
            disabled={props.showAnswer || props.disabled}
            onClick={props.onShowAnswer}
          />
        </Box>
      </Tooltip>

      <JapaneseTypography
        sx={{
          fontSize: (theme) => theme.custom.subTextSize,
          opacity: 0.6,
          cursor: "help",
          color: props.disabled ? "rgba(0, 0, 0, 0.38)" : {},
        }}
      >
        {props.showAnswer ? props.children : "Click to reveal the answer"}
      </JapaneseTypography>
    </Fragment>
  );
}

export default Answer;
