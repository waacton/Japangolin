namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;
    using System.Linq;

    public class Sentence
    {
        private static readonly string EnglishSpace = " ";
        private static readonly string JapaneseSpace = "　";

        public TopicBlock TopicBlock { get; }
        public ObjectBlock ObjectBlock { get; }

        public Sentence(TopicBlock topicBlock, ObjectBlock objectBlock)
        {
            this.TopicBlock = topicBlock;
            this.ObjectBlock = objectBlock;
        }

        public List<IGolin> GolinEnglish()
        {
            var golins = new List<IGolin>();
            golins.AddRange(this.TopicBlock.GolinEnglish());
            golins.AddRange(this.ObjectBlock.GolinEnglish());
            return golins;
        }

        public List<IGolin> GolinJapanese()
        {
            var golins = new List<IGolin>();
            golins.AddRange(this.TopicBlock.GolinJapanese());
            golins.AddRange(this.ObjectBlock.GolinJapanese());
            return golins;
        }

        public string GetEnglish() => ConvertToEnglish(this.GolinEnglish());

        public string GetKana() => ConvertToKana(this.GolinJapanese());

        public string GetKanji() => ConvertToKanji(this.GolinJapanese());

        private static string ConvertToEnglish(List<IGolin> golins)
        {
            return string.Join(EnglishSpace, golins.Select(golin => golin.EnglishConjugated));
        }

        private static string ConvertToKana(List<IGolin> golins)
        {
            return string.Join(JapaneseSpace, golins.Select(golin => golin.KanaConjugated));
        }

        private static string ConvertToKanji(List<IGolin> golins)
        {
            return string.Join(JapaneseSpace, golins.Select(golin => golin.KanjiConjugated));
        }

        public override string ToString()
        {
            return $"{this.GetEnglish()} | {this.GetKana()}";
        }
    }
}
