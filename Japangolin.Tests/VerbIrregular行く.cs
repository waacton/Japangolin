namespace Wacton.Japangolin.Tests;

using NUnit.Framework;
using Wacton.Japangolin.Core.Conjugation;
using Wacton.Japangolin.Core.Enums;
using Wacton.Japangolin.Core.Words;

public class VerbIrregular行く
{
    // TODO: enable handling of irregular verb 行く (new WordClass)
    private readonly Word word = new() { Kanji = "行く", Kana = "いく", Class = WordClass.VerbU };

    [Test]
    public void Dictionary()
    {
        var (kana, kanji) = Inflection.Dictionary.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("行く"));
        Assert.That(kana, Is.EqualTo("いく"));
    }

    [Test]
    public void Stem()
    {
        var (kana, kanji) = Inflection.Stem.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("行き"));
        Assert.That(kana, Is.EqualTo("いき"));
    }

    [Test]
    public void Te()
    {
        var (kana, kanji) = Inflection.Te.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("行って"));
        Assert.That(kana, Is.EqualTo("いって"));
    }

    [Test]
    public void PresentAffirmativeLong()
    {
        var (kana, kanji) = Inflection.PresentAffirmativeLong.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("行きます"));
        Assert.That(kana, Is.EqualTo("いきます"));
    }

    [Test]
    public void PresentAffirmativeShort()
    {
        var (kana, kanji) = Inflection.PresentAffirmativeShort.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("行く"));
        Assert.That(kana, Is.EqualTo("いく"));
    }

    [Test]
    public void PresentNegativeLong()
    {
        var (kana, kanji) = Inflection.PresentNegativeLong.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("行きません"));
        Assert.That(kana, Is.EqualTo("いきません"));
    }

    [Test]
    public void PresentNegativeShort()
    {
        var (kana, kanji) = Inflection.PresentNegativeShort.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("行かない"));
        Assert.That(kana, Is.EqualTo("いかない"));
    }

    [Test]
    public void PastAffirmativeLong()
    {
        var (kana, kanji) = Inflection.PastAffirmativeLong.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("行きました"));
        Assert.That(kana, Is.EqualTo("いきました"));
    }

    [Test]
    public void PastAffirmativeShort()
    {
        var (kana, kanji) = Inflection.PastAffirmativeShort.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("行った"));
        Assert.That(kana, Is.EqualTo("いった"));
    }

    [Test]
    public void PastNegativeLong()
    {
        var (kana, kanji) = Inflection.PastNegativeLong.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("行きませんでした"));
        Assert.That(kana, Is.EqualTo("いきませんでした"));
    }

    [Test]
    public void PastNegativeShort()
    {
        var (kana, kanji) = Inflection.PastNegativeShort.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("行かなかった"));
        Assert.That(kana, Is.EqualTo("いかなかった"));
    }
}