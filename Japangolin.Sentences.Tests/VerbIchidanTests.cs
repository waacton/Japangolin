namespace Wacton.Japangolin.Sentences.Tests
{
    using NUnit.Framework;

    using Wacton.Japangolin.Sentences.Domain.Conjugations;
    using Wacton.Japangolin.Sentences.Domain.Golins;

    [TestFixture]
    public class VerbIchidanTests
    {
        private const string Kana = "たべる";
        private const string Kanji = "食べる";
        private const string Translation = "to eat";

        [Test]
        public void ConjugateNone()
        {
            var golin = CreateGolin(Conjugation.None);
            Assert.That(golin.KanaConjugated, Is.EqualTo("たべる"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("食べる"));
        }

        // TODO: add conjugation comments to the golin object, can show to user if confused about conjugation rules

        [Test]
        public void ConjugateLongPresentAffirmative()
        {
            // drop ru, add masu
            var golin = CreateGolin(Conjugation.LongPresentAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("たべます"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("食べます"));
        }

        [Test]
        public void ConjugateLongPresentNegative()
        {
            // drop ru, add masen
            var golin = CreateGolin(Conjugation.LongPresentNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("たべません"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("食べません"));
        }

        [Test]
        public void ConjugateLongPastAffirmative()
        {
            // drop ru, add mashita
            var golin = CreateGolin(Conjugation.LongPastAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("たべました"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("食べました"));
        }

        [Test]
        public void ConjugateLongPastNegative()
        {
            // drop ru, add masendeshita
            var golin = CreateGolin(Conjugation.LongPastNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("たべませんでした"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("食べませんでした"));
        }

        [Test]
        public void ConjugateShortPresentAffirmative()
        {
            // dictionary form
            var golin = CreateGolin(Conjugation.ShortPresentAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("たべる"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("食べる"));
        }

        [Test]
        public void ConjugateShortPresentNegative()
        {
            // drop ru, add nai
            var golin = CreateGolin(Conjugation.ShortPresentNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("たべない"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("食べない"));
        }

        [Test]
        public void ConjugateShortPastAffirmative()
        {
            // te-form, te becomes ta
            var golin = CreateGolin(Conjugation.ShortPastAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("たべた"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("食べた"));
        }

        [Test]
        public void ConjugateShortPastNegative()
        {
            // drop ru, add nakatta [short-present: nai ~> short-past: nakatta]
            var golin = CreateGolin(Conjugation.ShortPastNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("たべなかった"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("食べなかった"));
        }

        private static IGolin CreateGolin(Conjugation conjugation)
        {
            var english = new English(Translation);
            var japanese = new Japanese(Kana, Kanji, conjugation, ConjugationFunctions.JapaneseVerbIchidan);
            return new Golin(english, japanese);
        }
    }
}
