namespace Wacton.Japangolin.Sentences.Tests
{
    using NUnit.Framework;

    using Wacton.Japangolin.Sentences.Domain.Conjugations;
    using Wacton.Japangolin.Sentences.Domain.Golins;

    [TestFixture]
    public class VerbGodanくTests
    {
        private const string Kana = "かく";
        private const string Kanji = "書く";
        private const string Translation = "to write";

        [Test]
        public void ConjugateNone()
        {
            var golin = CreateGolin(Conjugation.None);
            Assert.That(golin.KanaConjugated, Is.EqualTo("かく"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("書く"));
        }

        [Test]
        public void ConjugateLongPresentAffirmative()
        {
            // ~u becomes ~i, add masu
            var golin = CreateGolin(Conjugation.LongPresentAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("かきます"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("書きます"));
        }

        [Test]
        public void ConjugateLongPresentNegative()
        {
            // ~u becomes ~i, add masen
            var golin = CreateGolin(Conjugation.LongPresentNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("かきません"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("書きません"));
        }

        [Test]
        public void ConjugateLongPastAffirmative()
        {
            // ~u becomes ~i, add mashita
            var golin = CreateGolin(Conjugation.LongPastAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("かきました"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("書きました"));
        }

        [Test]
        public void ConjugateLongPastNegative()
        {
            // ~u becomes ~i, add masendeshita
            var golin = CreateGolin(Conjugation.LongPastNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("かきませんでした"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("書きませんでした"));
        }

        [Test]
        public void ConjugateShortPresentAffirmative()
        {
            // dictionary form
            var golin = CreateGolin(Conjugation.ShortPresentAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("かく"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("書く"));
        }

        [Test]
        public void ConjugateShortPresentNegative()
        {
            // ~u becomes ~a, add nai 
            var golin = CreateGolin(Conjugation.ShortPresentNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("かかない"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("書かない"));
        }

        [Test]
        public void ConjugateShortPastAffirmative()
        {
            // te-form, te becomes ta
            var golin = CreateGolin(Conjugation.ShortPastAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("かいた"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("書いた"));
        }

        [Test]
        public void ConjugateShortPastNegative()
        {
            // ~u becomes ~a, add nakatta [short-present: nai ~> short-past: nakatta]
            var golin = CreateGolin(Conjugation.ShortPastNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("かかなかった"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("書かなかった"));
        }

        private static IGolin CreateGolin(Conjugation conjugation)
        {
            var english = new English(Translation);
            var japanese = new Japanese(Kana, Kanji, WordClass.JapaneseVerbGodan, conjugation);
            return new Golin(english, japanese);
        }
    }
}
