namespace Wacton.Japangolin.UI.Themes
{
    using System.Windows.Media;
    using MaterialDesignColors;
    using MaterialDesignThemes.Wpf;

    public static class Stylist
    {
        private static readonly PaletteHelper paletteHelper = new PaletteHelper();

        // general design uses custom gradients
        // this sets the overall theme to use one of the gradients colours
        // so non-customised controls still match colours from the gradient
        public static void SetVibrantTheme(Color vibrantColor)
        {
            var baseTheme = Theme.Light;
            var primary = vibrantColor;
            var secondary = vibrantColor;

            var theme = Theme.Create(baseTheme, primary, secondary);

            // modify secondary colour to make foreground color white
            theme.SecondaryMid = new ColorPair(theme.SecondaryMid.Color, Colors.White);

            paletteHelper.SetTheme(theme);
        }


        // generic styling methods, for reference

        public static void SetStyle(bool isDarkBase, PrimaryColor primary, SecondaryColor secondary)
        {
            Color primaryColor = SwatchHelper.Lookup[(MaterialDesignColor)primary];
            Color secondaryColor = SwatchHelper.Lookup[(MaterialDesignColor)secondary];
            SetStyle(isDarkBase, primaryColor, secondaryColor);
        }

        public static void SetStyle(bool isDarkBase, PrimaryColor primary, Color secondary)
        {
            Color primaryColor = SwatchHelper.Lookup[(MaterialDesignColor)primary];
            SetStyle(isDarkBase, primaryColor, secondary);
        }

        public static void SetStyle(bool isDarkBase, Color primary, SecondaryColor secondary)
        {
            Color secondaryColor = SwatchHelper.Lookup[(MaterialDesignColor)secondary];
            SetStyle(isDarkBase, primary, secondaryColor);
        }

        public static void SetStyle(bool isDarkBase, Color primary, Color secondary)
        {
            var baseTheme = isDarkBase ? Theme.Dark : Theme.Light;
            var theme = Theme.Create(baseTheme, primary, secondary);
            paletteHelper.SetTheme(theme);
        }
    }
}
