import { Stack, Typography } from "@mui/material";
import JapaneseTypography from "./japaneseTypography";

interface Props {
  firstDetail: string;
  secondDetail: string;
  thirdDetail: string;
}

export function Detail(props: Props) {
  return (
    <Stack
      sx={{
        justifyContent: "center",
        alignItems: "center",
        height: "100%",
        gap: 0.5,
        margin: 2,
      }}
    >
      <JapaneseTypography>{props.firstDetail}</JapaneseTypography>
      <JapaneseTypography>{props.secondDetail}</JapaneseTypography>
      <Typography sx={{ fontSize: (theme) => theme.custom.subTextSize, opacity: 0.6 }}>{props.thirdDetail}</Typography>
    </Stack>
  );
}

export function NoDetail() {
  return (
    <Typography sx={{ fontWeight: "light", fontSize: (theme) => theme.custom.subTextSize, opacity: 0.6, margin: 2 }}>
      Select a word or inflection to see a hint
    </Typography>
  );
}
