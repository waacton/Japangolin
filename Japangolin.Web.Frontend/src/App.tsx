import "./App.css";

import { createTheme, ThemeProvider } from "@mui/material/styles";
import { orange, pink } from "@mui/material/colors";
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
  // just experimenting with adding custom theme variables
  // (requires typescript module augmentation, see types/mui-styles.d.ts)
  status: {
    danger: orange[500],
    hot: pink[500],
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
