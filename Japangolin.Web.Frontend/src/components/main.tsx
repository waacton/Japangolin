import { Box } from "@mui/material";
import Header from "./header";
import Placeholder from "./placeholder";

// TODO: make colours such as #404046 and #FAFAFA accessible around the app - custom theme variables?
function Main() {
  return (
    <Box sx={{ display: "grid", gridTemplateColumns: "3fr 2fr 1fr", gridTemplateRows: "auto repeat(5, 2fr)" }}>
      <Box gridRow={1} gridColumn={"1 / span 3"} sx={{ borderBottom: 1, borderColor: "#404046" }}>
        <Header />
      </Box>

      <Box gridRow={"2 / span 6"} gridColumn={"1 / span 3"} sx={{ bgcolor: "pink" }} />
      <Placeholder gridRow={2} gridColumn={3}>
        Toggle
      </Placeholder>
      <Placeholder gridRow={3} gridColumn={1}>
        Word
      </Placeholder>
      <Placeholder gridRow={4} gridColumn={1}>
        Inflection
      </Placeholder>
      <Placeholder gridRow={"3 / span 2"} gridColumn={"2 / span 2"}>
        Hint
      </Placeholder>
      <Placeholder gridRow={5} gridColumn={"1 / span 2"}>
        Input
      </Placeholder>
      <Placeholder gridRow={5} gridColumn={3}>
        Skip Button
      </Placeholder>
      <Placeholder gridRow={6} gridColumn={"1 / span 3"}>
        Answer
      </Placeholder>
    </Box>
  );
}

export default Main;
