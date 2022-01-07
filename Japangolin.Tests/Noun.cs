namespace Wacton.Japangolin.Tests
{
    using NUnit.Framework;
    using Wacton.Japangolin.Domain.Conjugation;
    using Wacton.Japangolin.Domain.Enums;
    using Wacton.Japangolin.Domain.Words;

    public class Noun
    {
        private readonly Word word = new Word { Kanji = "人", Kana = "ひと", Class = WordClass.Noun };

        [Test]
        public void Dictionary()
        {
            (var kana, var kanji) = Inflection.Dictionary.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("人"));
            Assert.That(kana, Is.EqualTo("ひと"));
        }

        [Test]
        public void Stem()
        {
            (var kana, var kanji) = Inflection.Stem.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("人"));
            Assert.That(kana, Is.EqualTo("ひと"));
        }

        [Test]
        public void Te()
        {
            (var kana, var kanji) = Inflection.Te.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("人で"));
            Assert.That(kana, Is.EqualTo("ひとで"));
        }

        [Test]
        public void PresentAffirmativeLong()
        {
            (var kana, var kanji) = Inflection.PresentAffirmativeLong.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("人です"));
            Assert.That(kana, Is.EqualTo("ひとです"));
        }

        [Test]
        public void PresentAffirmativeShort()
        {
            (var kana, var kanji) = Inflection.PresentAffirmativeShort.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("人だ"));
            Assert.That(kana, Is.EqualTo("ひとだ"));
        }

        [Test]
        public void PresentNegativeLong()
        {
            (var kana, var kanji) = Inflection.PresentNegativeLong.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("人じゃないです"));
            Assert.That(kana, Is.EqualTo("ひとじゃないです"));
        }

        [Test]
        public void PresentNegativeShort()
        {
            (var kana, var kanji) = Inflection.PresentNegativeShort.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("人じゃない"));
            Assert.That(kana, Is.EqualTo("ひとじゃない"));
        }

        [Test]
        public void PastAffirmativeLong()
        {
            (var kana, var kanji) = Inflection.PastAffirmativeLong.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("人でした"));
            Assert.That(kana, Is.EqualTo("ひとでした"));
        }

        [Test]
        public void PastAffirmativeShort()
        {
            (var kana, var kanji) = Inflection.PastAffirmativeShort.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("人だった"));
            Assert.That(kana, Is.EqualTo("ひとだった"));
        }

        [Test]
        public void PastNegativeLong()
        {
            (var kana, var kanji) = Inflection.PastNegativeLong.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("人じゃなかったです"));
            Assert.That(kana, Is.EqualTo("ひとじゃなかったです"));
        }

        [Test]
        public void PastNegativeShort()
        {
            (var kana, var kanji) = Inflection.PastNegativeShort.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("人じゃなかった"));
            Assert.That(kana, Is.EqualTo("ひとじゃなかった"));
        }

    }
}