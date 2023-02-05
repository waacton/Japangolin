namespace Wacton.Japangolin.Tests;

using NUnit.Framework;
using Wacton.Japangolin.Core.Conjugation;
using Wacton.Japangolin.Core.Enums;
using Wacton.Japangolin.Core.Words;

public class VerbIrregular来る
{
    // TODO: enable handling of irregular verb 来る (new WordClass)
    private readonly Word word = new() { Kanji = "来る", Kana = "くる", Class = WordClass.Unknown };

    [Test]
    public void Dictionary()
    {
        var (kana, kanji) = Inflection.Dictionary.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("来る"));
        Assert.That(kana, Is.EqualTo("くる"));
    }

    [Test]
    public void Stem()
    {
        var (kana, kanji) = Inflection.Stem.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("来"));
        Assert.That(kana, Is.EqualTo("き"));
    }

    [Test]
    public void Te()
    {
        var (kana, kanji) = Inflection.Te.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("来て"));
        Assert.That(kana, Is.EqualTo("きて"));
    }

    [Test]
    public void PresentAffirmativeLong()
    {
        var (kana, kanji) = Inflection.PresentAffirmativeLong.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("来ます"));
        Assert.That(kana, Is.EqualTo("きます"));
    }

    [Test]
    public void PresentAffirmativeShort()
    {
        var (kana, kanji) = Inflection.PresentAffirmativeShort.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("来る"));
        Assert.That(kana, Is.EqualTo("くる"));
    }

    [Test]
    public void PresentNegativeLong()
    {
        var (kana, kanji) = Inflection.PresentNegativeLong.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("来ません"));
        Assert.That(kana, Is.EqualTo("きません"));
    }

    [Test]
    public void PresentNegativeShort()
    {
        var (kana, kanji) = Inflection.PresentNegativeShort.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("来ない"));
        Assert.That(kana, Is.EqualTo("こない"));
    }

    [Test]
    public void PastAffirmativeLong()
    {
        var (kana, kanji) = Inflection.PastAffirmativeLong.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("来ました"));
        Assert.That(kana, Is.EqualTo("きました"));
    }

    [Test]
    public void PastAffirmativeShort()
    {
        var (kana, kanji) = Inflection.PastAffirmativeShort.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("来た"));
        Assert.That(kana, Is.EqualTo("きた"));
    }

    [Test]
    public void PastNegativeLong()
    {
        var (kana, kanji) = Inflection.PastNegativeLong.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("来ませんでした"));
        Assert.That(kana, Is.EqualTo("きませんでした"));
    }

    [Test]
    public void PastNegativeShort()
    {
        var (kana, kanji) = Inflection.PastNegativeShort.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("来なかった"));
        Assert.That(kana, Is.EqualTo("こなかった"));
    }
}