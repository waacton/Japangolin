import { Typography, TypographyProps } from "@mui/material";
import { Theme } from "@mui/material/styles";

function UppercaseLabel(props: TypographyProps) {
  return (
    <Typography
      {...props}
      variant={"overline"}
      sx={{
        ...props.sx,
        fontSize: (theme: Theme) => theme.custom.labelTextSize,
        fontWeight: "medium",
        opacity: 0.5,
        lineHeight: "unset",
      }}
    >
      {props.children}
    </Typography>
  );
}

export default UppercaseLabel;
