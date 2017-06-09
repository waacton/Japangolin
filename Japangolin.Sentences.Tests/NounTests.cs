namespace Wacton.Japangolin.Sentences.Tests
{
    using NUnit.Framework;

    using Wacton.Japangolin.Sentences.Domain.Mains;

    [TestFixture]
    public class NounTests
    {
        private string kana = "ひと";
        private string kanji = "人";
        private string translation = "person";

        [SetUp]
        public void SetupTest()
        {
            //
        }

        [Test]
        public void ConjugateNone()
        {
            var golin = this.CreateGolin(Conjugation.None);
            Assert.That(golin.KanaConjugated, Is.EqualTo("ひと"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("人"));
        }

        // TODO: add conjugation comments to the golin object, can show to user if confused about conjugation rules

        [Test]
        public void ConjugateLongPresentAffirmative()
        {
            // add desu
            var golin = this.CreateGolin(Conjugation.LongPresentAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("ひとです"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("人です"));
        }

        [Test]
        public void ConjugateLongPresentNegative()
        {
            // add janaidesu (colloquial of ja arimasen)
            var golin = this.CreateGolin(Conjugation.LongPresentNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("ひとじゃないです"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("人じゃないです"));
        }

        [Test]
        public void ConjugateLongPastAffirmative()
        {
            // add deshita
            var golin = this.CreateGolin(Conjugation.LongPastAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("ひとでした"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("人でした"));
        }

        [Test]
        public void ConjugateLongPastNegative()
        {
            // add janakattadesu
            var golin = this.CreateGolin(Conjugation.LongPastNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("ひとじゃなかったです"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("人じゃなかったです"));
        }

        [Test]
        public void ConjugateShortPresentAffirmative()
        {
            // add da [long: desu ~> short: da]
            var golin = this.CreateGolin(Conjugation.ShortPresentAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("ひとだ"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("人だ"));
        }

        [Test]
        public void ConjugateShortPresentNegative()
        {
            // add janai [long: janaidesu ~> short: janai (drop desu)]
            var golin = this.CreateGolin(Conjugation.ShortPresentNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("ひとじゃない"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("人じゃない"));
        }

        [Test]
        public void ConjugateShortPastAffirmative()
        {
            // add datta [long: deshita ~> short: datta]
            var golin = this.CreateGolin(Conjugation.ShortPastAffirmative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("ひとだった"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("人だった"));
        }

        [Test]
        public void ConjugateShortPastNegative()
        {
            // add janakatta [long: janakattadesu ~> short: janakatta (drop desu)]
            var golin = this.CreateGolin(Conjugation.ShortPastNegative);
            Assert.That(golin.KanaConjugated, Is.EqualTo("ひとじゃなかった"));
            Assert.That(golin.KanjiConjugated, Is.EqualTo("人じゃなかった"));
        }

        private IGolin CreateGolin(Conjugation conjugation)
        {
            var english = new English(this.translation);
            var japanese = new Japanese(this.kana, this.kanji, conjugation, ConjugationFunctions.JapaneseNoun);
            return new Golin(english, japanese, true);
        }
    }
}
