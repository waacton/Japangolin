import {
  Box,
  Button,
  Card,
  FormControlLabel,
  Stack,
  SvgIcon,
  SvgIconProps,
  Switch,
  TextField,
  Tooltip,
  Typography,
} from "@mui/material";
import Header from "./header";
import VisibilityIcon from "@mui/icons-material/Visibility";

function SkipIcon(props: SvgIconProps) {
  return (
    <SvgIcon {...props}>
      <path d="M12,14A2,2 0 0,1 14,16A2,2 0 0,1 12,18A2,2 0 0,1 10,16A2,2 0 0,1 12,14M23.46,8.86L21.87,15.75L15,14.16L18.8,11.78C17.39,9.5 14.87,8 12,8C8.05,8 4.77,10.86 4.12,14.63L2.15,14.28C2.96,9.58 7.06,6 12,6C15.58,6 18.73,7.89 20.5,10.72L23.46,8.86Z" />
    </SvgIcon>
  );
}

const showHighlight = false;
const bgcolor = showHighlight ? "yellow" : "transparent";

// TODO: make colours such as #404046 and #FAFAFA accessible around the app - custom theme variables?
function Main() {
  return (
    <Box
      sx={{
        display: "grid",
        gridTemplateColumns: "2fr 2fr",
        gridTemplateRows: "repeat(6, auto)",
        bgcolor: "#FAFAFA",
        borderBottom: 1,
        borderColor: "#40404622",
      }}
    >
      <Box
        sx={{
          gridRow: 1,
          gridColumn: "1 / span 2",
          borderBottom: 1,
          borderColor: "#40404622",
          bgcolor: "background.paper",
        }}
      >
        <Header />
      </Box>

      {/* `checked` needs to be controlled by state */}
      <Stack
        sx={{
          gridRow: 2,
          gridColumn: "1 / span 2",
          justifyContent: "center",
          alignItems: "flex-end",
          marginLeft: 2,
          marginRight: 2,
          marginTop: 1,
          bgcolor: bgcolor,
        }}
      >
        <FormControlLabel
          control={<Switch defaultChecked />}
          label={
            <Typography variant={"overline"} sx={{ fontSize: "0.7rem", fontWeight: "medium", opacity: 0.6 }}>
              JLPT N5
            </Typography>
          }
          labelPlacement={"start"}
          checked={true}
        />
      </Stack>

      <Stack
        direction={"column"}
        sx={{
          gridRow: 3,
          gridColumn: 1,
          marginLeft: 2,
          marginTop: 1,
          marginBottom: 1,
          marginRight: 1,
          bgcolor: bgcolor,
        }}
      >
        <Typography variant={"overline"} sx={{ fontSize: "0.7rem", fontWeight: "medium", opacity: 0.6 }}>
          WORD
        </Typography>
        <Typography variant={"body1"}>word</Typography>
      </Stack>

      <Stack
        direction={"column"}
        sx={{
          gridRow: 4,
          gridColumn: 1,
          marginLeft: 2,
          marginTop: 1,
          marginBottom: 1,
          marginRight: 1,
          bgcolor: bgcolor,
        }}
      >
        <Typography variant={"overline"} sx={{ fontSize: "0.7rem", fontWeight: "medium", opacity: 0.6 }}>
          Inflection
        </Typography>
        <Typography variant={"body1"}>inflection</Typography>
      </Stack>

      <Box
        sx={{
          gridRow: "3 / span 2",
          gridColumn: 2,
          marginLeft: 1,
          marginTop: 1,
          marginBottom: 1,
          marginRight: 2,
          bgcolor: bgcolor,
        }}
      >
        <Card variant={"elevation"} sx={{ height: "100%" }}>
          <Stack sx={{ justifyContent: "center", alignItems: "center", height: "100%" }}>
            <Typography sx={{ fontWeight: "light", fontSize: "0.75rem", opacity: 0.6 }}>
              Select a word or inflection to see a hint
            </Typography>
          </Stack>
        </Card>
      </Box>

      <Stack
        direction={"row"}
        sx={{
          gridRow: 5,
          gridColumn: "1 / span 2",
          gap: 2,
          justifyContent: "flex-start",
          alignItems: "center",
          marginLeft: 2,
          marginTop: 1,
          marginRight: 2,
          bgcolor: bgcolor,
        }}
      >
        <TextField label={"Japanese Conjugation"} variant={"filled"} fullWidth />

        <Tooltip title={"Skip"}>
          <Button variant={"outlined"} sx={{ width: 64, height: 64 }}>
            <SkipIcon />
          </Button>
        </Tooltip>
      </Stack>

      <Stack
        direction={"row"}
        sx={{
          gridRow: 6,
          gridColumn: "1 / span 2",
          gap: 1,
          justifyContent: "flex-start",
          alignItems: "center",
          marginLeft: 2,
          marginRight: 2,
          marginTop: 0.5,
          marginBottom: 2,
          bgcolor: bgcolor,
        }}
      >
        <Tooltip title={"View Answer"}>
          <Button variant={"outlined"} sx={{ width: 32, height: 32, minWidth: 32 }}>
            <VisibilityIcon sx={{ width: 16, height: 16 }} />
          </Button>
        </Tooltip>

        <Typography sx={{ fontSize: "0.75rem", opacity: 0.6 }}>Click to reveal the answer</Typography>
      </Stack>
    </Box>
  );
}

export default Main;
