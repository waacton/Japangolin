// without this import, typescript can't export from mui/material/styles
// eslint-disable-next-line @typescript-eslint/no-unused-vars
import { Theme, ThemeOptions } from "@mui/material/styles";

declare module "@mui/material/styles" {
  interface Theme {
    custom: {
      wactonDark: string;
      wactonLight: string;
      gradient: string;
      gradientSvgFill: string;
      background: string;
      labelTextSize: string;
      subTextSize: string;
      japaneseFont: string;
    };
  }
  // allow configuration using `createTheme`
  interface ThemeOptions {
    custom?: {
      wactonDark?: string;
      wactonLight?: string;
      gradient?: string;
      gradientSvgFill?: string;
      background?: string;
      labelTextSize?: string;
      subTextSize?: string;
      japaneseFont?: string;
    };
  }
}
