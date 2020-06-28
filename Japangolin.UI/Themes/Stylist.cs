namespace Wacton.Japangolin
{
    using System;

    using MaterialDesignThemes.Wpf;

    public static class Stylist
    {
        public static Swatch CurrentPrimarySwatch;
        public static Swatch CurrentAccentSwatch;
        public static bool IsDarkBase;

        private static readonly PaletteHelper PaletteHelper = new PaletteHelper();

        // NOTE: never seems to change (based on MaterialDesignColor XAML files)
        // NOTE: see if this can be removed in future, if able to QueryPalette when custom swatches are used
        private static readonly int PrimaryLightHueIndex = 1;
        private static readonly int PrimaryMidHueIndex = 5;
        private static readonly int PrimaryDarkHueIndex = 7;
        private static readonly int AccentHueIndex = 3;

        public static void SetPrimarySwatch(Swatch primarySwatch)
        {
            SetStyle(primarySwatch, CurrentAccentSwatch, IsDarkBase);
        }

        public static void SetAccentSwatch(Swatch accentSwatch)
        {
            SetStyle(CurrentPrimarySwatch, accentSwatch, IsDarkBase);
        }

        public static void SetSwatches(Swatch primarySwatch, Swatch accentSwatch)
        {
            SetStyle(primarySwatch, accentSwatch, IsDarkBase);
        }

        public static void SetDarkBase(bool isDarkBase)
        {
            SetStyle(CurrentPrimarySwatch, CurrentAccentSwatch, isDarkBase);
        }

        public static void SetStyle(Swatch primarySwatch, Swatch accentSwatch, bool isDarkBase)
        {
            if (!accentSwatch.IsAccented)
            {
                throw new InvalidOperationException($"Swatch {accentSwatch} has no accent definition");
            }

            CurrentPrimarySwatch = primarySwatch;
            CurrentAccentSwatch = accentSwatch;
            IsDarkBase = isDarkBase;

            var newPalette = new Palette(primarySwatch.MaterialSwatch, accentSwatch.MaterialSwatch, 
                                         PrimaryLightHueIndex, PrimaryMidHueIndex, PrimaryDarkHueIndex, AccentHueIndex);

            PaletteHelper.ReplacePalette(newPalette);
            PaletteHelper.SetLightDark(IsDarkBase);
        }
    }
}
