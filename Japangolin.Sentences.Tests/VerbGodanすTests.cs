namespace Wacton.Japangolin.Sentences.Tests
{
    using NUnit.Framework;

    using Wacton.Japangolin.Sentences.Domain.Conjugations;
    using Wacton.Japangolin.Sentences.Domain.Golins;

    [TestFixture]
    public class VerbGodanすTests
    {
        private const string Kana = "けす";
        private const string Kanji = "消す";
        private const string Translation = "to erase";

        [Test]
        public void ConjugateNone()
        {
            var golin = CreateGolin(Conjugation.None);
            Assert.That(golin.KanaConjugated, Is.EqualTo("けす"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("消す"));
        }

        [Test]
        public void ConjugateLongPresentAffirmative()
        {
            // ~u becomes ~i, add masu
            var golin = CreateGolin(Conjugation.LongPresentAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("けします"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("消します"));
        }

        [Test]
        public void ConjugateLongPresentNegative()
        {
            // ~u becomes ~i, add masen
            var golin = CreateGolin(Conjugation.LongPresentNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("けしません"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("消しません"));
        }

        [Test]
        public void ConjugateLongPastAffirmative()
        {
            // ~u becomes ~i, add mashita
            var golin = CreateGolin(Conjugation.LongPastAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("けしました"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("消しました"));
        }

        [Test]
        public void ConjugateLongPastNegative()
        {
            // ~u becomes ~i, add masendeshita
            var golin = CreateGolin(Conjugation.LongPastNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("けしませんでした"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("消しませんでした"));
        }

        [Test]
        public void ConjugateShortPresentAffirmative()
        {
            // dictionary form
            var golin = CreateGolin(Conjugation.ShortPresentAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("けす"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("消す"));
        }

        [Test]
        public void ConjugateShortPresentNegative()
        {
            // ~u becomes ~a, add nai 
            var golin = CreateGolin(Conjugation.ShortPresentNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("けさない"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("消さない"));
        }

        [Test]
        public void ConjugateShortPastAffirmative()
        {
            // te-form, te becomes ta
            var golin = CreateGolin(Conjugation.ShortPastAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("けした"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("消した"));
        }

        [Test]
        public void ConjugateShortPastNegative()
        {
            // ~u becomes ~a, add nakatta [short-present: nai ~> short-past: nakatta]
            var golin = CreateGolin(Conjugation.ShortPastNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("けさなかった"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("消さなかった"));
        }

        private static IGolin CreateGolin(Conjugation conjugation)
        {
            var english = new English(Translation);
            var japanese = new Japanese(Kana, Kanji, conjugation, ConjugationFunctions.JapaneseVerbGodan, ConjugationInformations.JapaneseVerbGodan);
            return new Golin(english, japanese);
        }
    }
}
