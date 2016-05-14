namespace Wacton.Desu
{
    using System.Collections.Generic;
    using System.Linq;

    public class Sense
    {
        public List<string> KanjiRestricted { get; set; } = new List<string>();
        public List<string> ReadingRestricted { get; set; } = new List<string>();
        public List<string> PartsOfSpeech { get; set; } = new List<string>();
        public List<string> CrossReferences { get; set; } = new List<string>();
        public List<string> Antonyms { get; set; } = new List<string>();
        public List<Field> Fields { get; set; } = new List<Field>();
        public List<string> Miscellanea { get; set; } = new List<string>();
        public List<string> Informations { get; set; } = new List<string>();
        public List<LoanwordGloss> LoanwordSources { get; set; } = new List<LoanwordGloss>();
        public List<Dialect> Dialects { get; set; } = new List<Dialect>();
        public List<Gloss> Glosses { get; set; } = new List<Gloss>();

        public override string ToString()
        {
            return this.Glosses.First(gloss => gloss.Language.Equals(Language.English)).ToString();
        }
    }
}
