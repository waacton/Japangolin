namespace Wacton.Japangolin.Tests;

using NUnit.Framework;
using Wacton.Japangolin.Core.Conjugation;
using Wacton.Japangolin.Core.Enums;
using Wacton.Japangolin.Core.Words;

public class VerbIrregularある
{
    // TODO: enable handling of irregular verb ある (new WordClass)
    private readonly Word word = new() { Kanji = "有る", Kana = "ある", Class = WordClass.VerbU };

    [Test]
    public void Dictionary()
    {
        var (kana, kanji) = Inflection.Dictionary.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("有る"));
        Assert.That(kana, Is.EqualTo("ある"));
    }

    [Test]
    public void Stem()
    {
        var (kana, kanji) = Inflection.Stem.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("有り"));
        Assert.That(kana, Is.EqualTo("あり"));
    }

    [Test]
    public void Te()
    {
        var (kana, kanji) = Inflection.Te.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("有って"));
        Assert.That(kana, Is.EqualTo("あって"));
    }

    [Test]
    public void PresentAffirmativeLong()
    {
        var (kana, kanji) = Inflection.PresentAffirmativeLong.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("有ります"));
        Assert.That(kana, Is.EqualTo("あります"));
    }

    [Test]
    public void PresentAffirmativeShort()
    {
        var (kana, kanji) = Inflection.PresentAffirmativeShort.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("有る"));
        Assert.That(kana, Is.EqualTo("ある"));
    }

    [Test]
    public void PresentNegativeLong()
    {
        var (kana, kanji) = Inflection.PresentNegativeLong.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("有りません"));
        Assert.That(kana, Is.EqualTo("ありません"));
    }

    [Test]
    public void PresentNegativeShort()
    {
        var (kana, kanji) = Inflection.PresentNegativeShort.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("ない"));　// TODO: confirm kanji usage (有/あ not present in negative short forms?)
        Assert.That(kana, Is.EqualTo("ない"));
    }

    [Test]
    public void PastAffirmativeLong()
    {
        var (kana, kanji) = Inflection.PastAffirmativeLong.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("有りました"));
        Assert.That(kana, Is.EqualTo("ありました"));
    }

    [Test]
    public void PastAffirmativeShort()
    {
        var (kana, kanji) = Inflection.PastAffirmativeShort.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("有った"));
        Assert.That(kana, Is.EqualTo("あった"));
    }

    [Test]
    public void PastNegativeLong()
    {
        var (kana, kanji) = Inflection.PastNegativeLong.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("有りませんでした"));
        Assert.That(kana, Is.EqualTo("ありませんでした"));
    }

    [Test]
    public void PastNegativeShort()
    {
        var (kana, kanji) = Inflection.PastNegativeShort.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("なかった")); // TODO: confirm kanji usage (有/あ not present in negative short forms?)
        Assert.That(kana, Is.EqualTo("なかった"));
    }
}