namespace Wacton.Japangolin.Sentences.Tests
{
    using NUnit.Framework;

    using Wacton.Japangolin.Sentences.Domain.Conjugations;
    using Wacton.Japangolin.Sentences.Domain.Golins;

    [TestFixture]
    public class VerbGodanぐTests
    {
        private const string Kana = "いそぐ";
        private const string Kanji = "急ぐ";
        private const string Translation = "to hurry";

        [Test]
        public void ConjugateNone()
        {
            var golin = CreateGolin(Conjugation.None);
            Assert.That(golin.KanaConjugated, Is.EqualTo("いそぐ"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("急ぐ"));
        }

        [Test]
        public void ConjugateLongPresentAffirmative()
        {
            // ~u becomes ~i, add masu
            var golin = CreateGolin(Conjugation.LongPresentAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("いそぎます"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("急ぎます"));
        }

        [Test]
        public void ConjugateLongPresentNegative()
        {
            // ~u becomes ~i, add masen
            var golin = CreateGolin(Conjugation.LongPresentNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("いそぎません"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("急ぎません"));
        }

        [Test]
        public void ConjugateLongPastAffirmative()
        {
            // ~u becomes ~i, add mashita
            var golin = CreateGolin(Conjugation.LongPastAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("いそぎました"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("急ぎました"));
        }

        [Test]
        public void ConjugateLongPastNegative()
        {
            // ~u becomes ~i, add masendeshita
            var golin = CreateGolin(Conjugation.LongPastNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("いそぎませんでした"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("急ぎませんでした"));
        }

        [Test]
        public void ConjugateShortPresentAffirmative()
        {
            // dictionary form
            var golin = CreateGolin(Conjugation.ShortPresentAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("いそぐ"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("急ぐ"));
        }

        [Test]
        public void ConjugateShortPresentNegative()
        {
            // ~u becomes ~a, add nai 
            var golin = CreateGolin(Conjugation.ShortPresentNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("いそがない"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("急がない"));
        }

        [Test]
        public void ConjugateShortPastAffirmative()
        {
            // te-form, te becomes ta
            var golin = CreateGolin(Conjugation.ShortPastAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("いそいだ"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("急いだ"));
        }

        [Test]
        public void ConjugateShortPastNegative()
        {
            // ~u becomes ~a, add nakatta [short-present: nai ~> short-past: nakatta]
            var golin = CreateGolin(Conjugation.ShortPastNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("いそがなかった"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("急がなかった"));
        }

        private static IGolin CreateGolin(Conjugation conjugation)
        {
            var english = new English(Translation);
            var japanese = new Japanese(Kana, Kanji, WordClass.JapaneseVerbGodan, conjugation);
            return new Golin(english, japanese);
        }
    }
}
