import { SxProps } from "@mui/system";
import { Theme } from "@mui/material/styles";

/*
 * notes on gradient borders (with radius): https://codyhouse.co/nuggets/css-gradient-borders
 * essentially, background has 2 "background images" (gradients are images, not colours)
 * 1. a "fake" background-looking gradient, takes up padding box (everything but border)
 * 2. the desired border gradient, takes up border box (everything including border)
 * the fake gradient sits on top of the desired gradient, masking everything except the border
 * ----------
 * I've added a 3rd box which shows a translucent gradient effect on hover, using the same mechanics
 */

const gradientBorderBox = (theme: Theme) => `${theme.custom.gradient} border-box`;
const fakeBackgroundPaddingBox = (theme: Theme) =>
  `linear-gradient(${theme.custom.background}, ${theme.custom.background}) padding-box`;
const translucentGradientPaddingBox = (theme: Theme) =>
  `linear-gradient(to right, ${theme.palette.primary.main}11 0%, ${theme.palette.secondary.main}11 100%) padding-box`;

export function gradientBorderStyle(): SxProps<Theme> {
  return {
    background: (theme: Theme) => `${fakeBackgroundPaddingBox(theme)}, ${gradientBorderBox(theme)}`,
    border: "1px solid transparent",
  };
}

export function gradientBorderWithTranslucentFillStyle(): SxProps<Theme> {
  return {
    background: (theme: Theme) =>
      `${translucentGradientPaddingBox(theme)}, ${fakeBackgroundPaddingBox(theme)}, ${gradientBorderBox(theme)}`,
    border: "1px solid transparent",
  };
}

export function gradientFillStyle(): SxProps<Theme> {
  return {
    background: (theme: Theme) => theme.custom.gradient,
    border: "1px solid",
  };
}
