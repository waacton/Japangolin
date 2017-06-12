namespace Wacton.Japangolin.Sentences.Tests
{
    using NUnit.Framework;

    using Wacton.Japangolin.Sentences.Domain.Mains;

    [TestFixture]
    public class VerbGodanぬTests
    {
        private const string Kana = "しぬ";
        private const string Kanji = "死ぬ";
        private const string Translation = "to die";

        [Test]
        public void ConjugateNone()
        {
            var golin = CreateGolin(Conjugation.None);
            Assert.That(golin.KanaConjugated, Is.EqualTo("しぬ"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("死ぬ"));
        }

        // TODO: add conjugation comments to the golin object, can show to user if confused about conjugation rules

        [Test]
        public void ConjugateLongPresentAffirmative()
        {
            // ~u becomes ~i, add masu
            var golin = CreateGolin(Conjugation.LongPresentAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("しにます"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("死にます"));
        }

        [Test]
        public void ConjugateLongPresentNegative()
        {
            // ~u becomes ~i, add masen
            var golin = CreateGolin(Conjugation.LongPresentNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("しにません"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("死にません"));
        }

        [Test]
        public void ConjugateLongPastAffirmative()
        {
            // ~u becomes ~i, add mashita
            var golin = CreateGolin(Conjugation.LongPastAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("しにました"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("死にました"));
        }

        [Test]
        public void ConjugateLongPastNegative()
        {
            // ~u becomes ~i, add masendeshita
            var golin = CreateGolin(Conjugation.LongPastNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("しにませんでした"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("死にませんでした"));
        }

        [Test]
        public void ConjugateShortPresentAffirmative()
        {
            // dictionary form
            var golin = CreateGolin(Conjugation.ShortPresentAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("しぬ"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("死ぬ"));
        }

        [Test]
        public void ConjugateShortPresentNegative()
        {
            // ~u becomes ~a, add nai 
            var golin = CreateGolin(Conjugation.ShortPresentNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("しなない"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("死なない"));
        }

        [Test]
        public void ConjugateShortPastAffirmative()
        {
            // te-form, te becomes ta
            var golin = CreateGolin(Conjugation.ShortPastAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("しんだ"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("死んだ"));
        }

        [Test]
        public void ConjugateShortPastNegative()
        {
            // ~u becomes ~a, add nakatta [short-present: nai ~> short-past: nakatta]
            var golin = CreateGolin(Conjugation.ShortPastNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("しななかった"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("死ななかった"));
        }

        private static IGolin CreateGolin(Conjugation conjugation)
        {
            var english = new English(Translation);
            var japanese = new Japanese(Kana, Kanji, conjugation, ConjugationFunctions.JapaneseVerbGodan);
            return new Golin(english, japanese, true);
        }
    }
}
