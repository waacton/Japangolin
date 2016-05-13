namespace Wacton.Desu
{
    using Wacton.Tovarisch.Enum;

    public class PartOfSpeech : Enumeration
    {
        public static readonly PartOfSpeech Adjective_I = new PartOfSpeech("AdjectiveI", "adj-i");
        public static readonly PartOfSpeech Adjective_IX = new PartOfSpeech("AdjectiveIX", "adj-ix");


        public string Code { get; }

        private static int counter;
        public PartOfSpeech(string displayName, string code)
            : base(counter++, displayName)
        {
            this.Code = code;
        }
    }
}