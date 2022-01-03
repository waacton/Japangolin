import { Typography, TypographyProps, useTheme } from "@mui/material";

function JapaneseTypography(props: TypographyProps) {
  // looks for any character from CJK Radicals Supplement to CJK Unified Ideographs Extension G (https://unicode-table.com/en/blocks/)
  let containsNonLatinCharacters = false;
  if (props.children != null) {
    const text = props.children as string;
    const regex = /[\u2E80-\u3134F]/;
    containsNonLatinCharacters = regex.test(text);
  }

  const theme = useTheme();
  return (
    <Typography {...props} fontFamily={containsNonLatinCharacters ? theme.custom.japaneseFont : {}}>
      {props.children}
    </Typography>
  );
}

export default JapaneseTypography;
