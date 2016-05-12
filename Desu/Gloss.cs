namespace Wacton.Desu
{
    using Wacton.Tovarisch.Enum;

    public class Gloss : Enumeration
    {
        public static readonly Gloss Dutch = new Gloss("Dutch", "dut");
        public static readonly Gloss English = new Gloss("English", "eng");
        public static readonly Gloss French = new Gloss("French", "fre");
        public static readonly Gloss German = new Gloss("German", "ger");
        public static readonly Gloss Hungarian = new Gloss("Hungarian", "hun");
        public static readonly Gloss Italian = new Gloss("Italian", "ita");
        public static readonly Gloss Russian = new Gloss("Russian", "rus");
        public static readonly Gloss Slovenian = new Gloss("Slovenian", "slv");
        public static readonly Gloss Spanish = new Gloss("Spanish", "spa");
        public static readonly Gloss Swedish = new Gloss("Swedish", "swe");

        public string Code { get; }

        private static int counter;
        public Gloss(string displayName, string code)
            : base(counter++, displayName)
        {
            this.Code = code;
        }
    }
}
