namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class SentenceBlock
    {
        public INounPhrase NounPhrase { get; }
        public Conjugation Conjugation => this.NounPhrase.Conjugation;

        public SentenceBlock(INounPhrase nounPhrase)
        {
            this.NounPhrase = nounPhrase;
        }

        public abstract List<IGolin> GolinEnglish();

        public abstract List<IGolin> GolinJapanese();

    }

    public class TopicBlock : SentenceBlock
    {
        private Topicgolin Topicgolin => new Topicgolin(this.Conjugation);

        public TopicBlock(INounPhrase nounPhrase) : base(nounPhrase)
        {
        }

        public override List<IGolin> GolinEnglish()
        {
            var golins = this.NounPhrase.GolinEnglish();
            golins.Add(this.Topicgolin);
            return golins;
        }

        public override List<IGolin> GolinJapanese()
        {
            var golins = this.NounPhrase.GolinJapanese();
            golins.Add(this.Topicgolin);
            return golins;
        }
    }

    public class ObjectBlock : SentenceBlock
    {
        private IGolin FinalNoun => this.NounPhrase.GolinJapanese().Last();

        private Objectgolin Objectgolin => new Objectgolin(this.Conjugation);
        private ObjectNoungolin ObjectNoungolin => new ObjectNoungolin(new NounEnglish(this.FinalNoun.EnglishBase, this.Conjugation), new ObjectNounJapanese(this.FinalNoun.KanaBase, this.FinalNoun.KanjiBase, this.Conjugation));

        public ObjectBlock(INounPhrase nounPhrase) : base(nounPhrase)
        {
        }

        public override List<IGolin> GolinEnglish()
        {
            var golins = this.NounPhrase.GolinEnglish();
            golins.Insert(0, this.Objectgolin);
            return golins;
        }

        public override List<IGolin> GolinJapanese()
        {
            // replace the final noun of the noun phrase with one that will conjugate as an object-noun
            var golins = this.NounPhrase.GolinJapanese();
            var finalGolin = golins.Last();
            golins.Remove(finalGolin);
            golins.Add(this.ObjectNoungolin);
            return golins;
        }

    }
}
