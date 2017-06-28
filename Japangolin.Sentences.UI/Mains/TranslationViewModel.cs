namespace Wacton.Japangolin.Sentences.UI.Mains
{
    using Wacton.Japangolin.Sentences.Domain.Golins;
    using Wacton.Tovarisch.MVVM;
    using Wacton.Tovarisch.UI.MVVM;

    public class TranslationViewModel : ViewModelBase
    {
        public IGolin Golin { get; private set; }

        public bool HasGolin => this.Golin != null;

        public string DefaultMessage => "Select a golin to see its translation here";

        public string EnglishBase => this.Golin?.EnglishBase;
        public string KanaBase => this.Golin?.KanaBase;
        public string KanjiBase => this.Golin?.KanjiBase;
        public string TranslationInformation => this.Golin?.TranslationInformation;

        public TranslationViewModel(ModelChangeNotifier modelChangeNotifier) : base(modelChangeNotifier)
        {
        }

        public void Update(IGolin newGolin)
        {
            this.Golin = newGolin;
            this.NotifyAllPropertyBindings();
        }
    }

    public class DesignTimeTranslationViewModel : TranslationViewModel
    {
        public DesignTimeTranslationViewModel() : base(null)
        {
        }
    }
}
