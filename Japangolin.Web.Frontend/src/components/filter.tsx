import { FormControlLabel, Switch } from "@mui/material";
import UppercaseLabel from "./uppercaseLabel";
import React from "react";

interface Props {
  children: string;
  checked: boolean;
  onChange: (event: React.SyntheticEvent) => void;
  disabled?: boolean;
}

function Filter(props: Props) {
  // TODO: need to control checked state
  return (
    <FormControlLabel
      labelPlacement={"start"}
      checked={props.checked}
      onChange={props.onChange}
      control={<Switch />}
      label={<UppercaseLabel>{props.children}</UppercaseLabel>}
      disabled={props.disabled}
    />
  );
}

export default Filter;
