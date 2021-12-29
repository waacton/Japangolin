// without this import, typescript can't export from mui/material/styles
// eslint-disable-next-line @typescript-eslint/no-unused-vars
import { Theme, ThemeOptions } from "@mui/material/styles";

declare module "@mui/material/styles" {
  interface Theme {
    status: {
      danger: string;
      hot: string;
    };
  }
  // allow configuration using `createTheme`
  interface ThemeOptions {
    status?: {
      danger?: string;
      hot?: string;
    };
  }
}
