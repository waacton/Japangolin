import { Box, BoxProps } from "@mui/material";

function Placeholder(props: BoxProps) {
  const { sx, ...other } = props;
  return (
    <Box
      sx={{
        bgcolor: "primary.main",
        color: "white",
        p: 1,
        m: 1,
        borderRadius: 1,
        textAlign: "center",
        fontSize: 19,
        fontWeight: "700",
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        ...sx,
      }}
      {...other}
    />
  );
}

export default Placeholder;
