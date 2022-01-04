import { Button, SvgIconProps } from "@mui/material";
import { gradientBorderStyle, gradientBorderWithTranslucentFillStyle } from "../utils/gradientHelper";
import { SxProps } from "@mui/system";
import { Theme } from "@mui/material/styles";

interface Props {
  icon: (props: SvgIconProps) => JSX.Element; // type deciphered from custom SkipIcon
  width?: number;
  height?: number;
  disabled?: boolean;
  onClick: () => void;
}

function GradientIconButton(props: Props) {
  const width = props.width ?? 32;
  const height = props.height ?? 32;
  const disabled = props.disabled;
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
    <Button variant={"outlined"} onClick={props.onClick} sx={{ width, height, minWidth: 32, ...buttonStyle }}>
      <props.icon sx={{ width: width / 2, height: height / 2, ...svgIconStyle }} />
    </Button>
  );
}

export default GradientIconButton;
