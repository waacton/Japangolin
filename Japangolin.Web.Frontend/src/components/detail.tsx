import { Card, Stack, Typography } from "@mui/material";
import { ReactNode } from "react";

interface Props {
  firstDetail?: string;
  secondDetail?: string;
  thirdDetail?: string;
}

// @ts-ignore
const DetailBase = (props: { children: ReactNode }) => (
  <Card variant={"elevation"} sx={{ height: "100%", textAlign: "center" }}>
    <Stack sx={{ justifyContent: "center", alignItems: "center", height: "100%" }}>{props.children}</Stack>
  </Card>
);

export function Detail(props: Props) {
  return (
    <DetailBase>
      <Stack sx={{ justifyContent: "center", alignItems: "center", height: "100%", gap: 0.5, margin: 2 }}>
        <Typography>{props.firstDetail}</Typography>
        <Typography>{props.secondDetail}</Typography>
        <Typography sx={{ fontSize: (theme) => theme.custom.subTextSize, opacity: 0.6 }}>
          {props.thirdDetail}
        </Typography>
      </Stack>
    </DetailBase>
  );
}

export function NoDetail() {
  return (
    <DetailBase>
      <Typography sx={{ fontWeight: "light", fontSize: "0.75rem", opacity: 0.6, margin: 2 }}>
        Select a word or inflection to see a hint
      </Typography>
    </DetailBase>
  );
}
