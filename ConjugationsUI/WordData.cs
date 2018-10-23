namespace ConjugationsUI
{
    using Wacton.Japangolin.Sentences.Domain.Conjugations;

    public class WordData
    {
        public string Text { get; set; }
        public WordClass Class { get; set; }

        public override string ToString()
        {
            return $"{this.Text} / {this.Class}";
        }
    }
}
