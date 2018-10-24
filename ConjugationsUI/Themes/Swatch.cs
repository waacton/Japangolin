namespace ConjugationsUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using MaterialDesignColors;

    using Wacton.Tovarisch.Enum;

    public class Swatch : Enumeration
    {
        private static readonly SwatchesProvider StandardSwatchesProvider = new SwatchesProvider();
        private static readonly SwatchesProvider CustomSwatchesProvider = new SwatchesProvider(Assembly.GetExecutingAssembly());
        private static readonly IEnumerable<MaterialDesignColors.Swatch> StandardSwatches = StandardSwatchesProvider.Swatches;
        private static readonly IEnumerable<MaterialDesignColors.Swatch> CustomSwatches = CustomSwatchesProvider.Swatches;

        // standard swatches (work nicely with PaletteHelper replace methods)
        public static readonly Swatch Red = new Swatch("Red");
        public static readonly Swatch Pink = new Swatch("Pink");
        public static readonly Swatch Purple = new Swatch("Purple");
        public static readonly Swatch DeepPurple = new Swatch("DeepPurple");
        public static readonly Swatch Indigo = new Swatch("Indigo");
        public static readonly Swatch Blue = new Swatch("Blue");
        public static readonly Swatch LightBlue = new Swatch("LightBlue");
        public static readonly Swatch Cyan = new Swatch("Cyan");
        public static readonly Swatch Teal = new Swatch("Teal");
        public static readonly Swatch Green = new Swatch("Green");
        public static readonly Swatch LightGreen = new Swatch("LightGreen");
        public static readonly Swatch Lime = new Swatch("Lime");
        public static readonly Swatch Yellow = new Swatch("Yellow");
        public static readonly Swatch Amber = new Swatch("Amber");
        public static readonly Swatch Orange = new Swatch("Orange");
        public static readonly Swatch DeepOrange = new Swatch("DeepOrange");
        public static readonly Swatch Brown = new Swatch("Brown");
        public static readonly Swatch Grey = new Swatch("Grey");
        public static readonly Swatch BlueGrey = new Swatch("BlueGrey");

        // custom swatches, as defined in Themes folder (do not work nicely with PaletteHelper replace methods, hence Stylist hardcoded indexes)
        public static readonly Swatch Firefinch = new Swatch("Firefinch");
        public static readonly Swatch UndoOrange = new Swatch("UndoOrange");
        public static readonly Swatch HotPink = new Swatch("HotPink");

        public MaterialDesignColors.Swatch MaterialSwatch { get; }
        public bool IsAccented => this.MaterialSwatch.IsAccented;
        public bool IsStandard => StandardSwatches.Contains(this.MaterialSwatch);

        private Swatch(string displayName)
            : base(displayName)
        {
            this.MaterialSwatch = StandardSwatches.SingleOrDefault(swatch => swatch.Name == displayName.ToLower())
                                  ?? CustomSwatches.SingleOrDefault(swatch => swatch.Name == displayName.ToLower());

            if (this.MaterialSwatch == null)
            {
                throw new InvalidOperationException(
                    $"Swatch {displayName.ToLower()} was not found in standard or custom swatches");
            }
        }
    }
}