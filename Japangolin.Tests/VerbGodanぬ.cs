using NUnit.Framework;
using Wacton.Japangolin.Domain.Conjugation;
using Wacton.Japangolin.Domain.Enums;
using Wacton.Japangolin.Domain.Words;

namespace Wacton.Japangolin.Tests
{
    public class VerbGodanぬ
    {
        private readonly Word word = new Word { Kanji = "死ぬ", Kana = "しぬ", Class = WordClass.VerbU };

        [Test]
        public void Dictionary()
        {
            (var kana, var kanji) = Inflection.Dictionary.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("死ぬ"));
            Assert.That(kana, Is.EqualTo("しぬ"));
        }

        [Test]
        public void Stem()
        {
            (var kana, var kanji) = Inflection.Stem.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("死に"));
            Assert.That(kana, Is.EqualTo("しに"));
        }

        [Test]
        public void Te()
        {
            (var kana, var kanji) = Inflection.Te.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("死んで"));
            Assert.That(kana, Is.EqualTo("しんで"));
        }

        [Test]
        public void PresentAffirmativeLong()
        {
            (var kana, var kanji) = Inflection.PresentAffirmativeLong.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("死にます"));
            Assert.That(kana, Is.EqualTo("しにます"));
        }

        [Test]
        public void PresentAffirmativeShort()
        {
            (var kana, var kanji) = Inflection.PresentAffirmativeShort.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("死ぬ"));
            Assert.That(kana, Is.EqualTo("しぬ"));
        }

        [Test]
        public void PresentNegativeLong()
        {
            (var kana, var kanji) = Inflection.PresentNegativeLong.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("死にません"));
            Assert.That(kana, Is.EqualTo("しにません"));
        }

        [Test]
        public void PresentNegativeShort()
        {
            (var kana, var kanji) = Inflection.PresentNegativeShort.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("死なない"));
            Assert.That(kana, Is.EqualTo("しなない"));
        }

        [Test]
        public void PastAffirmativeLong()
        {
            (var kana, var kanji) = Inflection.PastAffirmativeLong.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("死にました"));
            Assert.That(kana, Is.EqualTo("しにました"));
        }

        [Test]
        public void PastAffirmativeShort()
        {
            (var kana, var kanji) = Inflection.PastAffirmativeShort.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("死んだ"));
            Assert.That(kana, Is.EqualTo("しんだ"));
        }

        [Test]
        public void PastNegativeLong()
        {
            (var kana, var kanji) = Inflection.PastNegativeLong.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("死にませんでした"));
            Assert.That(kana, Is.EqualTo("しにませんでした"));
        }

        [Test]
        public void PastNegativeShort()
        {
            (var kana, var kanji) = Inflection.PastNegativeShort.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("死ななかった"));
            Assert.That(kana, Is.EqualTo("しななかった"));
        }
    }
}