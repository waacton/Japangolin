using NUnit.Framework;
using Wacton.Japangolin.Domain.Conjugation;
using Wacton.Japangolin.Domain.Enums;
using Wacton.Japangolin.Domain.Words;

namespace Japangolin.Tests
{
    public class Noun
    {
        private readonly Word word = new Word { Kanji = "人", Kana = "ひと", Class = WordClass.Noun };

        [Test]
        public void Dictionary()
        {
            (var kanji, var kana) = Inflection.Dictionary.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("ひと"));
            Assert.That(kana, Is.EqualTo("人"));
        }

        [Test]
        public void Stem()
        {
            (var kanji, var kana) = Inflection.Stem.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("ひと"));
            Assert.That(kana, Is.EqualTo("人"));
        }

        [Test]
        public void Te()
        {
            (var kanji, var kana) = Inflection.Te.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("ひとで"));
            Assert.That(kana, Is.EqualTo("人で"));
        }

        [Test]
        public void PresentAffirmativeLong()
        {
            (var kanji, var kana) = Inflection.PresentAffirmativeLong.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("ひとです"));
            Assert.That(kana, Is.EqualTo("人です"));
        }

        [Test]
        public void PresentAffirmativeShort()
        {
            (var kanji, var kana) = Inflection.PresentAffirmativeShort.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("ひとだ"));
            Assert.That(kana, Is.EqualTo("人だ"));
        }

        [Test]
        public void PresentNegativeLong()
        {
            (var kanji, var kana) = Inflection.PresentNegativeLong.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("ひとじゃないです"));
            Assert.That(kana, Is.EqualTo("人じゃないです"));
        }

        [Test]
        public void PresentNegativeShort()
        {
            (var kanji, var kana) = Inflection.PresentNegativeShort.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("ひとじゃない"));
            Assert.That(kana, Is.EqualTo("人じゃない"));
        }

        [Test]
        public void PastAffirmativeLong()
        {
            (var kanji, var kana) = Inflection.PastAffirmativeLong.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("ひとでした"));
            Assert.That(kana, Is.EqualTo("人でした"));
        }

        [Test]
        public void PastAffirmativeShort()
        {
            (var kanji, var kana) = Inflection.PastAffirmativeShort.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("ひとだった"));
            Assert.That(kana, Is.EqualTo("人だった"));
        }

        [Test]
        public void PastNegativeLong()
        {
            (var kanji, var kana) = Inflection.PastNegativeLong.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("ひとじゃなかったです"));
            Assert.That(kana, Is.EqualTo("人じゃなかったです"));
        }

        [Test]
        public void PastNegativeShort()
        {
            (var kanji, var kana) = Inflection.PastNegativeShort.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("ひとじゃなかった"));
            Assert.That(kana, Is.EqualTo("人じゃなかった"));
        }

    }
}