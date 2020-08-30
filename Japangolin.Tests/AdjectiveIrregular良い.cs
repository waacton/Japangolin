using NUnit.Framework;
using Wacton.Japangolin.Domain.Conjugation;
using Wacton.Japangolin.Domain.Enums;
using Wacton.Japangolin.Domain.Words;

namespace Wacton.Japangolin.Tests
{
    public class AdjectiveIrregular良い
    {
        // TODO: enable handling of irregular adjective 良い (new WordClass)
        private readonly Word word = new Word { Kanji = "良い", Kana = "よい", Class = WordClass.AdjectiveI };

        [Test]
        public void Dictionary()
        {
            (var kana, var kanji) = Inflection.Dictionary.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("良い"));
            Assert.That(kana, Is.EqualTo("いい"));
        }

        [Test]
        public void Stem()
        {
            (var kana, var kanji) = Inflection.Stem.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("良"));
            Assert.That(kana, Is.EqualTo("よ"));
        }

        [Test]
        public void Te()
        {
            (var kana, var kanji) = Inflection.Te.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("良くて"));
            Assert.That(kana, Is.EqualTo("よくて"));
        }

        [Test]
        public void PresentAffirmativeLong()
        {
            (var kana, var kanji) = Inflection.PresentAffirmativeLong.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("良いです"));
            Assert.That(kana, Is.EqualTo("いいです"));
        }

        [Test]
        public void PresentAffirmativeShort()
        {
            (var kana, var kanji) = Inflection.PresentAffirmativeShort.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("良い"));
            Assert.That(kana, Is.EqualTo("いい"));
        }

        [Test]
        public void PresentNegativeLong()
        {
            (var kana, var kanji) = Inflection.PresentNegativeLong.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("良くないです"));
            Assert.That(kana, Is.EqualTo("よくないです"));
        }

        [Test]
        public void PresentNegativeShort()
        {
            (var kana, var kanji) = Inflection.PresentNegativeShort.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("良くない"));
            Assert.That(kana, Is.EqualTo("よくない"));
        }

        [Test]
        public void PastAffirmativeLong()
        {
            (var kana, var kanji) = Inflection.PastAffirmativeLong.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("良かったです"));
            Assert.That(kana, Is.EqualTo("よかったです"));
        }

        [Test]
        public void PastAffirmativeShort()
        {
            (var kana, var kanji) = Inflection.PastAffirmativeShort.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("良かった"));
            Assert.That(kana, Is.EqualTo("よかった"));
        }

        [Test]
        public void PastNegativeLong()
        {
            (var kana, var kanji) = Inflection.PastNegativeLong.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("良くなかったです"));
            Assert.That(kana, Is.EqualTo("よくなかったです"));
        }

        [Test]
        public void PastNegativeShort()
        {
            (var kana, var kanji) = Inflection.PastNegativeShort.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("良くなかった"));
            Assert.That(kana, Is.EqualTo("よくなかった"));
        }
    }
}