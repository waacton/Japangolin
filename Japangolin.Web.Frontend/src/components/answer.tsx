import { Tooltip } from "@mui/material";
import VisibilityIcon from "@mui/icons-material/Visibility";
import { Fragment } from "react";
import JapaneseTypography from "./japaneseTypography";
import GradientIconButton from "./gradientIconButton";

interface Props {
  children: string;
  showAnswer: boolean;
  onShowAnswer: () => void;
}

function Answer(props: Props) {
  return (
    <Fragment>
      <Tooltip title={"View Answer"}>
        <GradientIconButton
          icon={VisibilityIcon}
          width={32}
          height={32}
          disabled={props.showAnswer}
          onClick={props.onShowAnswer}
        />
      </Tooltip>

      <JapaneseTypography sx={{ fontSize: (theme) => theme.custom.subTextSize, opacity: 0.6, cursor: "help" }}>
        {props.showAnswer ? props.children : "Click to reveal the answer"}
      </JapaneseTypography>
    </Fragment>
  );
}

export default Answer;
