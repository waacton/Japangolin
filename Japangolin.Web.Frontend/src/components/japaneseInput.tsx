import { styled, TextField, TextFieldProps } from "@mui/material";
import { containsNonLatinCharacters } from "../utils/stringUtils";
import React from "react";

interface Props {
  value: string;
  onChange: (event: React.ChangeEvent<HTMLInputElement>) => void;
}

// combination of https://mui.com/components/text-fields/#customization and inspecting class names
const ColourfulHoverTextField = styled(TextField)<TextFieldProps>(({ theme }) => ({
  "& .MuiFilledInput-root": {
    fontFamily: "inherit",
    // same as "& .MuiFilledInput-root:hover:not(.Mui-disabled):before"
    "&:hover:not(.Mui-disabled):before": {
      borderBottom: "1px solid",
      borderBottomColor: `${theme.palette.secondary.main}A9`, // needs to not be fully opaque, otherwise looks odd
    },
  },
}));

function JapaneseInput(props: Props) {
  const useJapaneseFont = containsNonLatinCharacters(props.value);
  console.log(`Input value: ${props.value} --> use Japanese? ${useJapaneseFont}`);

  return (
    <ColourfulHoverTextField
      label={"Japanese Conjugation"}
      variant={"filled"}
      fullWidth
      value={props.value}
      onChange={props.onChange}
      sx={{ fontFamily: (theme) => (useJapaneseFont ? theme.custom.japaneseFont : {}) }}
    />
  );
}

export default JapaneseInput;
