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

        private SentenceNounIsNoun currentSentence => this.main.CurrentSentence;

        public List<Translation> EnglishWords => this.currentSentence.GetTranslations(); 
        public string KanaSentence => this.currentSentence.GetKana();
        public string KanjiSentence => this.currentSentence.GetKanji();

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
        public new List<Translation> EnglishWords => new List<Translation> { new Translation("Japangolin", "日本蜥蜴", "ジャッパンゴリン") };
        public new string KanaSentence => "ジャッパンゴリン";
        public new string KanjiSentence => "日本蜥蜴";

        public DesignTimeMainViewModel() : base(null, null, null)
        {
        }
    }
}