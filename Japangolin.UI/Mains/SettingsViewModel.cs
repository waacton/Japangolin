namespace Wacton.Japangolin.UI.Mains
{
    using Wacton.Japangolin.Domain.Commands;
    using Wacton.Japangolin.Domain.Enums;
    using Wacton.Japangolin.Domain.Mains;
    using Wacton.Tovarisch.MVVM;
    using Wacton.Tovarisch.UI.MVVM;

    public class SettingsViewModel : ViewModelBase
    {
        private readonly Settings settings;
        private readonly ChangeWordFilterCommand changeWordFilterCommand;

        public bool IsJLPTN5 => settings.WordFilter == WordFilter.JLPTN5;

        public SettingsViewModel(Settings settings, ChangeWordFilterCommand changeWordFilterCommand, ModelChangeNotifier modelChangeNotifier)
            : base(modelChangeNotifier, settings)
        {
            this.settings = settings;
            this.changeWordFilterCommand = changeWordFilterCommand;
        }

        public void ToggleWordFilter()
        {
            var wordFilter = settings.WordFilter == WordFilter.None ? WordFilter.JLPTN5 : WordFilter.None; 
            this.changeWordFilterCommand.ExecuteAndNotify(wordFilter);
        }
    }

    // --- design time ---

    public class DesignTimeSettingsViewModel : SettingsViewModel
    {
        public new bool IsJLPTN5 => true;

        public DesignTimeSettingsViewModel() : base(null, null, null)
        {
        }
    }
}