namespace Wacton.Japangolin.Sentences.Tests
{
    using NUnit.Framework;

    using Wacton.Japangolin.Sentences.Domain.Conjugations;
    using Wacton.Japangolin.Sentences.Domain.Golins;

    [TestFixture]
    public class AdjectiveいTests
    {
        private const string Kana = "おいしい";
        private const string Kanji = "美味しい";
        private const string Translation = "delicious";

        [Test]
        public void ConjugateNone()
        {
            var golin = CreateGolin(Conjugation.None);
            Assert.That(golin.KanaConjugated, Is.EqualTo("おいしい"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("美味しい"));
        }

        [Test]
        public void ConjugateLongPresentAffirmative()
        {
            // add desu
            var golin = CreateGolin(Conjugation.LongPresentAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("おいしいです"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("美味しいです"));
        }

        [Test]
        public void ConjugateLongPresentNegative()
        {
            // drop i, add kunaidesu
            var golin = CreateGolin(Conjugation.LongPresentNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("おいしくないです"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("美味しくないです"));
        }

        [Test]
        public void ConjugateLongPastAffirmative()
        {
            // drop i, add kattadesu
            var golin = CreateGolin(Conjugation.LongPastAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("おいしかったです"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("美味しかったです"));
        }

        [Test]
        public void ConjugateLongPastNegative()
        {
            // drop i, add kunakattadesu
            var golin = CreateGolin(Conjugation.LongPastNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("おいしくなかったです"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("美味しくなかったです"));
        }

        [Test]
        public void ConjugateShortPresentAffirmative()
        {
            // dictionary form
            var golin = CreateGolin(Conjugation.ShortPresentAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("おいしい"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("美味しい"));
        }

        [Test]
        public void ConjugateShortPresentNegative()
        {
            // add kunai [long: kunaidesu ~> short: kunai (drop desu)]
            var golin = CreateGolin(Conjugation.ShortPresentNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("おいしくない"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("美味しくない"));
        }

        [Test]
        public void ConjugateShortPastAffirmative()
        {
            // add katta [long: kattadesu ~> short: katta (drop desu)]
            var golin = CreateGolin(Conjugation.ShortPastAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("おいしかった"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("美味しかった"));
        }

        [Test]
        public void ConjugateShortPastNegative()
        {
            // add kunakatta [long: kunakattadesu ~> short: kunakatta (drop desu)]
            var golin = CreateGolin(Conjugation.ShortPastNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("おいしくなかった"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("美味しくなかった"));
        }

        private static IGolin CreateGolin(Conjugation conjugation)
        {
            var english = new English(Translation);
            var japanese = new Japanese(Kana, Kanji, conjugation, ConjugationFunctions.JapaneseAdjectiveI, ConjugationInformations.JapaneseAdjectiveI);
            return new Golin(english, japanese);
        }
    }
}
