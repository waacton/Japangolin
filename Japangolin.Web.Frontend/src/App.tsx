import "./App.css";

import { createTheme, ThemeProvider } from "@mui/material/styles";
import Main from "./components/main";

const theme = createTheme({
  palette: {
    primary: {
      main: "#E004DD",
    },
    secondary: {
      main: "#F63D96",
    },
  },
  // custom theme variables (requires typescript module augmentation, see types/mui-styles.d.ts)
  custom: {
    wactonDark: "#404046",
    wactonLight: "#E8E8FF",
    gradient: "linear-gradient(to right, #E004DD 0%, #F63D96 100%)",
    labelTextSize: "0.7rem",
    subTextSize: "0.75rem",
  },
});

function App() {
  return (
    <ThemeProvider theme={theme}>
      <Main />
    </ThemeProvider>
  );
}

export default App;
