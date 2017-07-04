namespace Wacton.Japangolin.Sentences.Tests
{
    using NUnit.Framework;

    using Wacton.Japangolin.Sentences.Domain.Conjugations;
    using Wacton.Japangolin.Sentences.Domain.Golins;

    [TestFixture]
    public class NounTests
    {
        private const string Kana = "ひと";
        private const string Kanji = "人";
        private const string Translation = "person";

        [Test]
        public void ConjugateNone()
        {
            var golin = CreateGolin(Conjugation.None);
            Assert.That(golin.KanaConjugated, Is.EqualTo("ひと"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("人"));
        }

        [Test]
        public void ConjugateLongPresentAffirmative()
        {
            // add desu
            var golin = CreateGolin(Conjugation.LongPresentAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("ひとです"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("人です"));
        }

        [Test]
        public void ConjugateLongPresentNegative()
        {
            // add janaidesu (colloquial of ja arimasen)
            var golin = CreateGolin(Conjugation.LongPresentNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("ひとじゃないです"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("人じゃないです"));
        }

        [Test]
        public void ConjugateLongPastAffirmative()
        {
            // add deshita
            var golin = CreateGolin(Conjugation.LongPastAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("ひとでした"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("人でした"));
        }

        [Test]
        public void ConjugateLongPastNegative()
        {
            // add janakattadesu
            var golin = CreateGolin(Conjugation.LongPastNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("ひとじゃなかったです"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("人じゃなかったです"));
        }

        [Test]
        public void ConjugateShortPresentAffirmative()
        {
            // add da [long: desu ~> short: da]
            var golin = CreateGolin(Conjugation.ShortPresentAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("ひとだ"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("人だ"));
        }

        [Test]
        public void ConjugateShortPresentNegative()
        {
            // add janai [long: janaidesu ~> short: janai (drop desu)]
            var golin = CreateGolin(Conjugation.ShortPresentNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("ひとじゃない"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("人じゃない"));
        }

        [Test]
        public void ConjugateShortPastAffirmative()
        {
            // add datta [long: deshita ~> short: datta]
            var golin = CreateGolin(Conjugation.ShortPastAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("ひとだった"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("人だった"));
        }

        [Test]
        public void ConjugateShortPastNegative()
        {
            // add janakatta [long: janakattadesu ~> short: janakatta (drop desu)]
            var golin = CreateGolin(Conjugation.ShortPastNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("ひとじゃなかった"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("人じゃなかった"));
        }

        private static IGolin CreateGolin(Conjugation conjugation)
        {
            var english = new English(Translation);
            var japanese = new Japanese(Kana, Kanji, conjugation, ConjugationFunctions.JapaneseNoun, ConjugationInformations.JapaneseNoun);
            return new Golin(english, japanese);
        }
    }
}
