namespace Wacton.Japangolin.Sentences.UI.Mains
{
    using System.Collections.Generic;
    using System.Diagnostics;

    using Wacton.Japangolin.Sentences.Domain.Commands;
    using Wacton.Japangolin.Sentences.Domain.Conjugations;
    using Wacton.Japangolin.Sentences.Domain.Golins;
    using Wacton.Japangolin.Sentences.Domain.Mains;
    using Wacton.Tovarisch.MVVM;
    using Wacton.Tovarisch.UI.MVVM;

    public class MainViewModel : ViewModelBase
    {
        public static string Title { get; set; }

        private readonly Main main;
        private readonly UpdateSentenceCommand updateSentenceCommand;

        private Sentence CurrentSentence => this.main.Sentence;

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

        public void GoogleTranslate()
        {
            var url = $"https://translate.google.com/#ja/en/{this.KanjiSentence}";
            Process.Start(url);
        }
    }

    public class DesignTimeMainViewModel : MainViewModel
    {
        private readonly English english = new English("Japangolin");
        private readonly Japanese japanese = new Japanese("ジャパンゴリン", "日本蜥蜴", Conjugation.LongPresentAffirmative, ConjugationFunctions.JapaneseNoun);

        public new List<IGolin> GolinEnglish => new List<IGolin> { new DesignTimeGolin(this.english, this.japanese) };
        public new string KanaSentence => "ジャパンゴリン";
        public new string KanjiSentence => "日本蜥蜴";

        public DesignTimeMainViewModel() : base(null, null, null)
        {
        }
    }

    public class DesignTimeGolin : Golin
    {
        public DesignTimeGolin(English english, Japanese nounJapanese) : base(english, nounJapanese, "DesignTimeInformation")
        {
        }
    }
}