namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;

    using Wacton.Desu.Japanese;

    public class VerbNounPhrase : INounPhrase
    {
        public ITranslation Noun { get; }
        public ITranslation Verb { get; }

        public VerbNounPhrase(IJapaneseEntry noun, IJapaneseEntry verb)
        {
            this.Noun = new Translation(noun);
            this.Verb = new Translation(verb);
        }

        public List<ITranslation> GetEnglishOrder() => new List<ITranslation> { this.Verb, this.Noun };
        public List<ITranslation> GetJapaneseOrder() => new List<ITranslation> { this.Verb, this.Noun };
    }
}
