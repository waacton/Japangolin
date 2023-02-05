namespace Wacton.Japangolin.Tests;

using NUnit.Framework;
using Wacton.Japangolin.Core.Conjugation;
using Wacton.Japangolin.Core.Enums;
using Wacton.Japangolin.Core.Words;

public class VerbGodanす
{
    private readonly Word word = new() { Kanji = "消す", Kana = "けす", Class = WordClass.VerbU };

    [Test]
    public void Dictionary()
    {
        var (kana, kanji) = Inflection.Dictionary.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("消す"));
        Assert.That(kana, Is.EqualTo("けす"));
    }

    [Test]
    public void Stem()
    {
        var (kana, kanji) = Inflection.Stem.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("消し"));
        Assert.That(kana, Is.EqualTo("けし"));
    }

    [Test]
    public void Te()
    {
        var (kana, kanji) = Inflection.Te.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("消して"));
        Assert.That(kana, Is.EqualTo("けして"));
    }

    [Test]
    public void PresentAffirmativeLong()
    {
        var (kana, kanji) = Inflection.PresentAffirmativeLong.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("消します"));
        Assert.That(kana, Is.EqualTo("けします"));
    }

    [Test]
    public void PresentAffirmativeShort()
    {
        var (kana, kanji) = Inflection.PresentAffirmativeShort.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("消す"));
        Assert.That(kana, Is.EqualTo("けす"));
    }

    [Test]
    public void PresentNegativeLong()
    {
        var (kana, kanji) = Inflection.PresentNegativeLong.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("消しません"));
        Assert.That(kana, Is.EqualTo("けしません"));
    }

    [Test]
    public void PresentNegativeShort()
    {
        var (kana, kanji) = Inflection.PresentNegativeShort.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("消さない"));
        Assert.That(kana, Is.EqualTo("けさない"));
    }

    [Test]
    public void PastAffirmativeLong()
    {
        var (kana, kanji) = Inflection.PastAffirmativeLong.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("消しました"));
        Assert.That(kana, Is.EqualTo("けしました"));
    }

    [Test]
    public void PastAffirmativeShort()
    {
        var (kana, kanji) = Inflection.PastAffirmativeShort.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("消した"));
        Assert.That(kana, Is.EqualTo("けした"));
    }

    [Test]
    public void PastNegativeLong()
    {
        var (kana, kanji) = Inflection.PastNegativeLong.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("消しませんでした"));
        Assert.That(kana, Is.EqualTo("けしませんでした"));
    }

    [Test]
    public void PastNegativeShort()
    {
        var (kana, kanji) = Inflection.PastNegativeShort.Conjugate(word);
        Assert.That(kanji, Is.EqualTo("消さなかった"));
        Assert.That(kana, Is.EqualTo("けさなかった"));
    }
}