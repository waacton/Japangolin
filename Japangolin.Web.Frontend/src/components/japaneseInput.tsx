import { styled, TextField, TextFieldProps } from "@mui/material";
import { containsNonLatinCharacters } from "../utils/stringUtils";
import React, { useEffect, useRef } from "react";

interface Props {
  value: string;
  onChange: (event: React.ChangeEvent<HTMLInputElement>) => void;
  onKeyUp: (event: React.KeyboardEvent<HTMLInputElement>) => void;
  disabled?: boolean;
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

  // using a reference to access the DOM (https://reactjs.org/docs/refs-and-the-dom.html)
  // in order to drill down to the actual input element that's been wrapped by MUI
  // so that it can be triggered to focus
  const textFieldDivRef = useRef<HTMLDivElement>(null);
  useEffect(() => {
    if (textFieldDivRef.current) {
      const inputElement = textFieldDivRef.current.getElementsByTagName("input")[0];
      inputElement.focus();
    }
  }, [props.disabled]); // re-focus the input whenever disabled prop changes, i.e. whenever data fetch completes

  return (
    <ColourfulHoverTextField
      ref={textFieldDivRef}
      label={"Japanese Conjugation"}
      variant={"filled"}
      fullWidth
      value={props.value}
      onChange={props.onChange}
      onKeyUp={props.onKeyUp}
      disabled={props.disabled}
      sx={{ fontFamily: (theme) => (useJapaneseFont ? theme.custom.japaneseFont : {}) }}
    />
  );
}

export default JapaneseInput;
