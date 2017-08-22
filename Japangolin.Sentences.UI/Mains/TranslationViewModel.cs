﻿namespace Wacton.Japangolin.Sentences.UI.Mains
{
    using System.Collections.Generic;

    using Wacton.Japangolin.Sentences.Domain.Conjugations;
    using Wacton.Japangolin.Sentences.Domain.Golins;
    using Wacton.Tovarisch.MVVM;
    using Wacton.Tovarisch.UI.MVVM;

    public class TranslationViewModel : ViewModelBase
    {
        public IGolin Golin { get; private set; }

        public string EnglishBase => this.Golin?.EnglishBase;
        public string KanaBase => this.Golin?.KanaBase;
        public string KanjiBase => this.Golin?.KanjiBase;
        public IEnumerable<string> TranslationInformation => this.Golin?.TranslationInformation;

        public bool HasKanji => this.KanjiBase != this.KanaBase;

        private Conjugation Conjugation => this.Golin?.Conjugation;
        public string ConjugationDescription => $"<{this.Golin?.Conjugation?.Description.ToLower()}>";
        public string ConjugationInformation => this.Golin?.ConjugationInformation;

        private bool HasConjugation => this.Conjugation != null && !this.Conjugation.Equals(Conjugation.None);
        public bool IsShowingConjugationInformation => this.IsCheatModeEnabled && this.HasConjugation;

        private bool isCheatModeEnabled;
        public bool IsCheatModeEnabled
        {
            get
            {
                return this.isCheatModeEnabled;
            }
            set
            {
                this.isCheatModeEnabled = value;
                this.NotifyOfPropertyChange(nameof(this.IsCheatModeEnabled));
                this.NotifyOfPropertyChange(nameof(this.IsShowingConjugationInformation));
            }
        }

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