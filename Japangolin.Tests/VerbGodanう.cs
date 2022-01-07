namespace Wacton.Japangolin.Tests
{
    using NUnit.Framework;
    using Wacton.Japangolin.Domain.Conjugation;
    using Wacton.Japangolin.Domain.Enums;
    using Wacton.Japangolin.Domain.Words;

    public class VerbGodanう
    {
        private readonly Word word = new Word { Kanji = "会う", Kana = "あう", Class = WordClass.VerbU };

        [Test]
        public void Dictionary()
        {
            (var kana, var kanji) = Inflection.Dictionary.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("会う"));
            Assert.That(kana, Is.EqualTo("あう"));
        }

        [Test]
        public void Stem()
        {
            (var kana, var kanji) = Inflection.Stem.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("会い"));
            Assert.That(kana, Is.EqualTo("あい"));
        }

        [Test]
        public void Te()
        {
            (var kana, var kanji) = Inflection.Te.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("会って"));
            Assert.That(kana, Is.EqualTo("あって"));
        }

        [Test]
        public void PresentAffirmativeLong()
        {
            (var kana, var kanji) = Inflection.PresentAffirmativeLong.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("会います"));
            Assert.That(kana, Is.EqualTo("あいます"));
        }

        [Test]
        public void PresentAffirmativeShort()
        {
            (var kana, var kanji) = Inflection.PresentAffirmativeShort.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("会う"));
            Assert.That(kana, Is.EqualTo("あう"));
        }

        [Test]
        public void PresentNegativeLong()
        {
            (var kana, var kanji) = Inflection.PresentNegativeLong.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("会いません"));
            Assert.That(kana, Is.EqualTo("あいません"));
        }

        [Test]
        public void PresentNegativeShort()
        {
            (var kana, var kanji) = Inflection.PresentNegativeShort.Conjugate(word);
            Assert.That(kana, Is.EqualTo("あわない"));
            Assert.That(kanji, Is.EqualTo("会わない"));
        }

        [Test]
        public void PastAffirmativeLong()
        {
            (var kana, var kanji) = Inflection.PastAffirmativeLong.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("会いました"));
            Assert.That(kana, Is.EqualTo("あいました"));
        }

        [Test]
        public void PastAffirmativeShort()
        {
            (var kana, var kanji) = Inflection.PastAffirmativeShort.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("会った"));
            Assert.That(kana, Is.EqualTo("あった"));
        }

        [Test]
        public void PastNegativeLong()
        {
            (var kana, var kanji) = Inflection.PastNegativeLong.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("会いませんでした"));
            Assert.That(kana, Is.EqualTo("あいませんでした"));
        }
        
        [Test]
        public void PastNegativeShort()
        {
            (var kana, var kanji) = Inflection.PastNegativeShort.Conjugate(word);
            Assert.That(kanji, Is.EqualTo("会わなかった"));
            Assert.That(kana, Is.EqualTo("あわなかった"));
        }
    }
}