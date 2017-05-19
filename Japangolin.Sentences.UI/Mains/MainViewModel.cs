namespace Wacton.Japangolin.Sentences.UI.Mains
{
    using System.Collections.Generic;

    using Wacton.Desu.Japanese;
    using Wacton.Japangolin.Sentences.Domain.Commands;
    using Wacton.Japangolin.Sentences.Domain.Mains;
    using Wacton.Tovarisch.MVVM;
    using Wacton.Tovarisch.UI.MVVM;

    public class MainViewModel : ViewModelBase
    {
        public static string Title { get; set; }

        private readonly Main main;
        private readonly UpdateSentenceCommand updateSentenceCommand;

        private Sentence CurrentSentence => this.main.CurrentSentence;

        public List<ITranslation> EnglishWords => this.CurrentSentence.GetEnglishOrderTranslations(); 
        public string KanaSentence => this.CurrentSentence.GetKana();
        public string KanjiSentence => this.CurrentSentence.GetKanji();

        public MainViewModel(Main main, UpdateSentenceCommand updateSentenceCommand, ModelChangeNotifier modelChangeNotifier)
            : base(modelChangeNotifier, main)
        {
            this.main = main;
            this.updateSentenceCommand = updateSentenceCommand;
        }

        public void NextSentence()
        {
            this.updateSentenceCommand.ExecuteAndNotify();
        }
    }

    public class DesignTimeMainViewModel : MainViewModel
    {
        public new List<ITranslation> EnglishWords => new List<ITranslation> { new DesignTimeTranslation("Japangolin", "日本蜥蜴", "ジャッパンゴリン", Conjugation.None) };
        public new string KanaSentence => "ジャッパンゴリン";
        public new string KanjiSentence => "日本蜥蜴";

        public DesignTimeMainViewModel() : base(null, null, null)
        {
        }
    }

    public class DesignTimeTranslation : Translation
    {
        public override string EnglishConjugated => this.English;
        public override string KanaConjugated => this.Kana;
        public override string KanjiConjugated => this.Kanji;

        public DesignTimeTranslation(string english, string kanji, string kana, Conjugation conjugation) : base(english, kanji, kana, conjugation)
        {
        }
    }
}