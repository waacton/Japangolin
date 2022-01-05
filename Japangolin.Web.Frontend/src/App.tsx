import "./App.css";

import { createTheme, ThemeProvider } from "@mui/material/styles";
import Main from "./components/main";
import packageJson from "../package.json";

const gradientStart = "#E004DD";
const gradientEnd = "#F63D96";

const theme = createTheme({
  palette: {
    primary: {
      main: gradientStart,
    },
    secondary: {
      main: gradientEnd,
    },
  },
  // custom theme variables (requires typescript module augmentation, see types/mui-styles.d.ts)
  custom: {
    wactonDark: "#404046",
    wactonLight: "#E8E8FF",
    gradient: `linear-gradient(to right, ${gradientStart} 0%, ${gradientEnd} 100%)`,
    gradientSvgFill: "url(#svgGradient)",
    background: "#FAFAFA",
    labelTextSize: "0.7rem",
    subTextSize: "0.8rem",
    japaneseFont: "'Noto Sans JP', sans-serif;",
  },
});

function App() {
  console.log(`Welcome to Wacton.Japangolin · ${packageJson.version} · いらっしゃいませー`);
  return (
    <ThemeProvider theme={theme}>
      <Main />

      <svg width={0} height={0}>
        <linearGradient id="svgGradient" x1={0} y1={0} x2={1} y2={0}>
          <stop offset={0} stopColor={gradientStart} />
          <stop offset={1} stopColor={gradientEnd} />
        </linearGradient>
      </svg>
    </ThemeProvider>
  );
}

export default App;
