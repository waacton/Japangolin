import { Button, SvgIcon, SvgIconProps, Tooltip } from "@mui/material";
import { gradientBorderStyle, gradientBorderWithTranslucentFillStyle } from "../utils/gradientHelper";
import { SxProps } from "@mui/system";
import { Theme } from "@mui/material/styles";

interface Props {
  // children: string;
  disabled?: boolean;
  onClick: () => void;
}

function SkipIcon(props: SvgIconProps) {
  return (
    <SvgIcon {...props}>
      <path d="M12,14A2,2 0 0,1 14,16A2,2 0 0,1 12,18A2,2 0 0,1 10,16A2,2 0 0,1 12,14M23.46,8.86L21.87,15.75L15,14.16L18.8,11.78C17.39,9.5 14.87,8 12,8C8.05,8 4.77,10.86 4.12,14.63L2.15,14.28C2.96,9.58 7.06,6 12,6C15.58,6 18.73,7.89 20.5,10.72L23.46,8.86Z" />
    </SvgIcon>
  );
}

function SkipButton(props: Props) {
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
    <Tooltip title={"Skip"}>
      <Button variant={"outlined"} onClick={props.onClick} sx={{ width: 64, height: 64, ...buttonStyle }}>
        <SkipIcon sx={{ width: 32, height: 32, ...svgIconStyle }} />
      </Button>
    </Tooltip>
  );
}

export default SkipButton;
