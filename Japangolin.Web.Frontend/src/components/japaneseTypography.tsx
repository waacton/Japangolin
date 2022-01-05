import { Typography, TypographyProps, useTheme } from "@mui/material";
import { containsNonLatinCharacters } from "../utils/stringUtils";

function JapaneseTypography(props: TypographyProps) {
  let useJapaneseFont = false;
  if (props.children != null) {
    const text = props.children as string;
    useJapaneseFont = containsNonLatinCharacters(text);
  }

  // small example of `useTheme()` instead of accessing directly via sx prop
  const theme = useTheme();
  return (
    <Typography {...props} fontFamily={useJapaneseFont ? theme.custom.japaneseFont : {}}>
      {props.children}
    </Typography>
  );
}

export default JapaneseTypography;
