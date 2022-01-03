import { FormControlLabel, Switch } from "@mui/material";
import UppercaseLabel from "./uppercaseLabel";

interface Props {
  children: string;
}

function Filter(props: Props) {
  // TODO: need to control checked state
  return (
    <FormControlLabel
      labelPlacement={"start"}
      checked={true}
      control={<Switch defaultChecked />}
      label={<UppercaseLabel>{props.children}</UppercaseLabel>}
    />
  );
}

export default Filter;
