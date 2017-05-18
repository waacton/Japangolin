﻿namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;
    using System.Linq;

    using Wacton.Desu.Japanese;

    public class AdjectiveNounPhrase : INounPhrase
    {
        public ITranslation Noun { get; }
        public ITranslation Adjective { get; }

        public AdjectiveNounPhrase(IJapaneseEntry noun, IJapaneseEntry adjective)
        {
            this.Noun = new NounTranslation(noun);
            this.Adjective = new AdjectiveTranslation(adjective);
        }

        public List<ITranslation> GetEnglishOrder() => new List<ITranslation> { this.Adjective, this.Noun };
        public List<ITranslation> GetJapaneseOrder() => new List<ITranslation> { this.Adjective, this.Noun };

        public override string ToString()
        {
            var english = string.Join(" ", this.GetEnglishOrder().Select(translation => translation.English));
            var kana = string.Join(" ", this.GetEnglishOrder().Select(translation => translation.Kana));
            return $"{english} | {kana}";
        }
    }
}