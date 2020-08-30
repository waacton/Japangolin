using NUnit.Framework;
using Wacton.Japangolin.Domain.Conjugation;
using Wacton.Japangolin.Domain.Enums;
using Wacton.Japangolin.Domain.Words;

namespace Wacton.Japangolin.Tests
{
    public class VerbIchidan
    {
        private readonly Word word = new Word { Kanji = "食べる", Kana = "たべる", Class = WordClass.VerbRu };

        [Test]
        public void Dictionary()
        {
            (var kana, var kanji) = Inflection.Dictionary.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("食べる"));
            Assert.That(kana, Is.EqualTo("たべる"));
        }

        [Test]
        public void Stem()
        {
            (var kana, var kanji) = Inflection.Stem.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("食べ"));
            Assert.That(kana, Is.EqualTo("たべ"));
        }

        [Test]
        public void Te()
        {
            (var kana, var kanji) = Inflection.Te.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("食べて"));
            Assert.That(kana, Is.EqualTo("たべて"));
        }

        [Test]
        public void PresentAffirmativeLong()
        {
            (var kana, var kanji) = Inflection.PresentAffirmativeLong.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("食べます"));
            Assert.That(kana, Is.EqualTo("たべます"));
        }

        [Test]
        public void PresentAffirmativeShort()
        {
            (var kana, var kanji) = Inflection.PresentAffirmativeShort.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("食べる"));
            Assert.That(kana, Is.EqualTo("たべる"));
        }

        [Test]
        public void PresentNegativeLong()
        {
            (var kana, var kanji) = Inflection.PresentNegativeLong.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("食べません"));
            Assert.That(kana, Is.EqualTo("たべません"));
        }

        [Test]
        public void PresentNegativeShort()
        {
            (var kana, var kanji) = Inflection.PresentNegativeShort.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("食べない"));
            Assert.That(kana, Is.EqualTo("たべない"));
        }

        [Test]
        public void PastAffirmativeLong()
        {
            (var kana, var kanji) = Inflection.PastAffirmativeLong.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("食べました"));
            Assert.That(kana, Is.EqualTo("たべました"));
        }

        [Test]
        public void PastAffirmativeShort()
        {
            (var kana, var kanji) = Inflection.PastAffirmativeShort.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("食べた"));
            Assert.That(kana, Is.EqualTo("たべた"));
        }

        [Test]
        public void PastNegativeLong()
        {
            (var kana, var kanji) = Inflection.PastNegativeLong.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("食べませんでした"));
            Assert.That(kana, Is.EqualTo("たべませんでした"));
        }

        [Test]
        public void PastNegativeShort()
        {
            (var kana, var kanji) = Inflection.PastNegativeShort.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("食べなかった"));
            Assert.That(kana, Is.EqualTo("たべなかった"));
        }
    }
}
