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

        public List<Translation> EnglishWords => this.main.Translations; 
        public string EnglishSentence => this.main.EnglishSentence;
        public string Help => this.main.Help;
        public string KanaSentence => this.main.KanaSentence;
        public string KanjiSentence => this.main.KanjiSentence;

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
        public new string EnglishSentence => "Japangolin";
        public new string Help { get; set; } = "jappangorin";
        public new string KanaSentence => "ジャッパンゴリン";
        public new string KanjiSentence => "日本蜥蜴";

        public DesignTimeMainViewModel() : base(null, null, null)
        {
        }
    }
}