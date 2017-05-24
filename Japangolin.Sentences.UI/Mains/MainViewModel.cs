namespace Wacton.Japangolin.Sentences.UI.Mains
{
    using System.Collections.Generic;

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

        public List<IGolin> GolinEnglish => this.CurrentSentence.GolinEnglish();
        public List<IGolin> GolinJapanese => this.CurrentSentence.GolinJapanese();

        public string EnglishSentence => this.CurrentSentence.GetEnglish();
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
        private readonly ConjugatedEnglish conjugatedEnglish = new ConjugatedEnglish("Japangolin", Conjugation.LongPresentAffirmative);
        private readonly ConjugatedJapanese conjugatedJapanese = new ConjugatedJapanese("ジャッパンゴリン", "日本蜥蜴", Conjugation.LongPresentAffirmative);

        public new List<IGolin> GolinEnglish => new List<IGolin> { new DesignTimeGolin(this.conjugatedEnglish, this.conjugatedJapanese) };
        public new string KanaSentence => "ジャッパンゴリン";
        public new string KanjiSentence => "日本蜥蜴";

        public DesignTimeMainViewModel() : base(null, null, null)
        {
        }
    }

    public class DesignTimeGolin : Golin
    {
        public DesignTimeGolin(ConjugatedEnglish conjugatedEnglish, ConjugatedJapanese conjugatedJapanese) : base(conjugatedEnglish, conjugatedJapanese)
        {
        }
    }
}