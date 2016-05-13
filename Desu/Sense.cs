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
        public List<string> Fields { get; set; } = new List<string>();
        public List<string> Miscellanea { get; set; } = new List<string>();
        public List<string> Informations { get; set; } = new List<string>();
        public List<Gloss> LoanwordSource { get; set; } = new List<Gloss>();
        public List<string> Dialects { get; set; } = new List<string>();
        public List<Gloss> Glosses { get; set; } = new List<Gloss>();

        // TODO: missing some small attributes: ls_type, ls_wasei, g_gend
        // TODO: loanword gloss to be subtype of gloss, to include partial/full type and wasei bool?

        public override string ToString()
        {
            return this.Glosses.First(gloss => gloss.Language.Equals(Language.English)).ToString();
        }
    }
}
