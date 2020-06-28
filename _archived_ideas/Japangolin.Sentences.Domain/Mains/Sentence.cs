namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;
    using System.Linq;

    using Wacton.Japangolin.Sentences.Domain.Golins;
    using Wacton.Japangolin.Sentences.Domain.SentenceBlocks;
    using Wacton.Tovarisch.Collections;

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
            return golins.Select(golin => golin.EnglishConjugated).ToDelimitedString(EnglishSpace);
        }

        private static string ConvertToKana(List<IGolin> golins)
        {
            return golins.Select(golin => golin.KanaConjugated).ToDelimitedString(string.Empty);
        }

        private static string ConvertToKanji(List<IGolin> golins)
        {
            return golins.Select(golin => golin.KanjiConjugated).ToDelimitedString(string.Empty);
        }

        public override string ToString()
        {
            return $"{this.GetEnglish()} | {this.GetKana()}";
        }
    }
}
