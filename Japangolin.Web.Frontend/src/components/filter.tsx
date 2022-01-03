import { FormControlLabel, Switch, Typography } from "@mui/material";
import { Theme } from "@mui/material/styles";

interface Props {
  text: string;
}

function Filter(props: Props) {
  const label = (
    <Typography
      variant={"overline"}
      sx={{ fontSize: (theme: Theme) => theme.custom.labelTextSize, fontWeight: "medium", opacity: 0.5 }}
    >
      {props.text}
    </Typography>
  )

  // TODO: need to control checked state
  return (
    <FormControlLabel
      labelPlacement={"start"}
      checked={true}
      control={<Switch defaultChecked />}
      label={label}
    />
  );
}

export default Filter;
