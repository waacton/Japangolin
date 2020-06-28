namespace Wacton.Japangolin.Sentences.Tests
{
    using NUnit.Framework;

    using Wacton.Japangolin.Sentences.Domain.Conjugations;
    using Wacton.Japangolin.Sentences.Domain.Golins;

    [TestFixture]
    public class VerbGodanぶTests
    {
        private const string Kana = "あそぶ";
        private const string Kanji = "遊ぶ";
        private const string Translation = "to play";

        [Test]
        public void ConjugateNone()
        {
            var golin = CreateGolin(Conjugation.None);
            Assert.That(golin.KanaConjugated, Is.EqualTo("あそぶ"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("遊ぶ"));
        }

        [Test]
        public void ConjugateLongPresentAffirmative()
        {
            // ~u becomes ~i, add masu
            var golin = CreateGolin(Conjugation.LongPresentAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("あそびます"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("遊びます"));
        }

        [Test]
        public void ConjugateLongPresentNegative()
        {
            // ~u becomes ~i, add masen
            var golin = CreateGolin(Conjugation.LongPresentNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("あそびません"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("遊びません"));
        }

        [Test]
        public void ConjugateLongPastAffirmative()
        {
            // ~u becomes ~i, add mashita
            var golin = CreateGolin(Conjugation.LongPastAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("あそびました"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("遊びました"));
        }

        [Test]
        public void ConjugateLongPastNegative()
        {
            // ~u becomes ~i, add masendeshita
            var golin = CreateGolin(Conjugation.LongPastNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("あそびませんでした"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("遊びませんでした"));
        }

        [Test]
        public void ConjugateShortPresentAffirmative()
        {
            // dictionary form
            var golin = CreateGolin(Conjugation.ShortPresentAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("あそぶ"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("遊ぶ"));
        }

        [Test]
        public void ConjugateShortPresentNegative()
        {
            // ~u becomes ~a, add nai 
            var golin = CreateGolin(Conjugation.ShortPresentNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("あそばない"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("遊ばない"));
        }

        [Test]
        public void ConjugateShortPastAffirmative()
        {
            // te-form, te becomes ta
            var golin = CreateGolin(Conjugation.ShortPastAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("あそんだ"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("遊んだ"));
        }

        [Test]
        public void ConjugateShortPastNegative()
        {
            // ~u becomes ~a, add nakatta [short-present: nai ~> short-past: nakatta]
            var golin = CreateGolin(Conjugation.ShortPastNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("あそばなかった"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("遊ばなかった"));
        }

        private static IGolin CreateGolin(Conjugation conjugation)
        {
            var english = new English(Translation);
            var japanese = new Japanese(Kana, Kanji, WordClass.JapaneseVerbGodan, conjugation);
            return new Golin(english, japanese);
        }
    }
}
