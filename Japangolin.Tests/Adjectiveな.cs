namespace Wacton.Japangolin.Tests
{
    using NUnit.Framework;
    using Wacton.Japangolin.Domain.Conjugation;
    using Wacton.Japangolin.Domain.Enums;
    using Wacton.Japangolin.Domain.Words;

    public class Adjectiveな
    {
        // dictionary used in Wacton.Desu does not present な-adjectives dictionary form with trailing "な"
        private readonly Word word = new Word { Kanji = "綺麗", Kana = "きれい", Class = WordClass.AdjectiveNa };

        [Test]
        public void Dictionary()
        {
            (var kana, var kanji) = Inflection.Dictionary.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("綺麗"));
            Assert.That(kana, Is.EqualTo("きれい"));
        }

        [Test]
        public void Stem()
        {
            (var kana, var kanji) = Inflection.Stem.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("綺麗"));
            Assert.That(kana, Is.EqualTo("きれい"));
        }

        [Test]
        public void Te()
        {
            (var kana, var kanji) = Inflection.Te.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("綺麗で"));
            Assert.That(kana, Is.EqualTo("きれいで"));
        }

        [Test]
        public void PresentAffirmativeLong()
        {
            (var kana, var kanji) = Inflection.PresentAffirmativeLong.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("綺麗です"));
            Assert.That(kana, Is.EqualTo("きれいです"));
        }

        [Test]
        public void PresentAffirmativeShort()
        {
            (var kana, var kanji) = Inflection.PresentAffirmativeShort.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("綺麗だ"));
            Assert.That(kana, Is.EqualTo("きれいだ"));
        }

        [Test]
        public void PresentNegativeLong()
        {
            (var kana, var kanji) = Inflection.PresentNegativeLong.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("綺麗じゃないです"));
            Assert.That(kana, Is.EqualTo("きれいじゃないです"));
        }

        [Test]
        public void PresentNegativeShort()
        {
            (var kana, var kanji) = Inflection.PresentNegativeShort.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("綺麗じゃない"));
            Assert.That(kana, Is.EqualTo("きれいじゃない"));
        }

        [Test]
        public void PastAffirmativeLong()
        {
            (var kana, var kanji) = Inflection.PastAffirmativeLong.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("綺麗でした"));
            Assert.That(kana, Is.EqualTo("きれいでした"));
        }

        [Test]
        public void PastAffirmativeShort()
        {
            (var kana, var kanji) = Inflection.PastAffirmativeShort.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("綺麗だった"));
            Assert.That(kana, Is.EqualTo("きれいだった"));
        }

        [Test]
        public void PastNegativeLong()
        {
            (var kana, var kanji) = Inflection.PastNegativeLong.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("綺麗じゃなかったです"));
            Assert.That(kana, Is.EqualTo("きれいじゃなかったです"));
        }

        [Test]
        public void PastNegativeShort()
        {
            (var kana, var kanji) = Inflection.PastNegativeShort.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("綺麗じゃなかった"));
            Assert.That(kana, Is.EqualTo("きれいじゃなかった"));
        }
    }
}