namespace Wacton.Japangolin.Sentences.Tests
{
    using NUnit.Framework;

    using Wacton.Japangolin.Sentences.Domain.Conjugations;
    using Wacton.Japangolin.Sentences.Domain.Golins;

    [TestFixture]
    public class AdjectiveなTests
    {
        private const string Kana = "きれいな";
        private const string Kanji = "綺麗な";
        private const string Translation = "beautiful";

        [Test]
        public void ConjugateNone()
        {
            var golin = CreateGolin(Conjugation.None);
            Assert.That(golin.KanaConjugated, Is.EqualTo("きれいな"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("綺麗な"));
        }

        // TODO: add conjugation comments to the golin object, can show to user if confused about conjugation rules

        [Test]
        public void ConjugateLongPresentAffirmative()
        {
            // drop na, conjugate as noun - add desu
            var golin = CreateGolin(Conjugation.LongPresentAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("きれいです"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("綺麗です"));
        }

        [Test]
        public void ConjugateLongPresentNegative()
        {
            // drop na, conjugate as noun - add janaidesu (colloquial of ja arimasen)
            var golin = CreateGolin(Conjugation.LongPresentNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("きれいじゃないです"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("綺麗じゃないです"));
        }

        [Test]
        public void ConjugateLongPastAffirmative()
        {
            // drop na, conjugate as noun - add deshita
            var golin = CreateGolin(Conjugation.LongPastAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("きれいでした"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("綺麗でした"));
        }

        [Test]
        public void ConjugateLongPastNegative()
        {
            // drop na, conjugate as noun - add janakattadesu
            var golin = CreateGolin(Conjugation.LongPastNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("きれいじゃなかったです"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("綺麗じゃなかったです"));
        }

        [Test]
        public void ConjugateShortPresentAffirmative()
        {
            // drop na, conjugate as noun - add da [long: desu ~> short: da]
            var golin = CreateGolin(Conjugation.ShortPresentAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("きれいだ"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("綺麗だ"));
        }

        [Test]
        public void ConjugateShortPresentNegative()
        {
            // drop na, conjugate as noun - add janai [long: janaidesu ~> short: janai (drop desu)]
            var golin = CreateGolin(Conjugation.ShortPresentNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("きれいじゃない"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("綺麗じゃない"));
        }

        [Test]
        public void ConjugateShortPastAffirmative()
        {
            // drop na, conjugate as noun - add datta [long: deshita ~> short: datta]
            var golin = CreateGolin(Conjugation.ShortPastAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("きれいだった"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("綺麗だった"));
        }

        [Test]
        public void ConjugateShortPastNegative()
        {
            // drop na, conjugate as noun - add janakatta [long: janakattadesu ~> short: janakatta (drop desu)]
            var golin = CreateGolin(Conjugation.ShortPastNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("きれいじゃなかった"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("綺麗じゃなかった"));
        }

        private static IGolin CreateGolin(Conjugation conjugation)
        {
            var english = new English(Translation);
            var japanese = new Japanese(Kana, Kanji, conjugation, ConjugationFunctions.JapaneseAdjectiveNa);
            return new Golin(english, japanese, true);
        }
    }
}
