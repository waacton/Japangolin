namespace Wacton.Desu
{
    public class Gloss
    {
        public Language Language { get; }
        public string Term { get; }

        public Gloss(Language language, string term)
        {
            this.Language = language;
            this.Term = term;
        }

        public override string ToString()
        {
            return $"{this.Language}: {this.Term}";
        }
    }
}
