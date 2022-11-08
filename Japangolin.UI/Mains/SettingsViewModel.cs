using System;
using System.Windows.Input;
using Wacton.Japangolin.Domain.Actions;

namespace Wacton.Japangolin.UI.Mains
{
    using Wacton.Japangolin.Domain.Enums;
    using Wacton.Japangolin.Domain.Mains;
    using Wacton.Japangolin.Domain.MVVM;
    using Wacton.Japangolin.UI.MVVM;

    public class SettingsViewModel : ViewModelBase
    {
        private readonly Settings settings;
        private readonly ChangeWordFilterAction changeWordFilterAction;

        public bool IsJLPTN5 => settings.WordFilter == WordFilter.JLPTN5;
        
        public ICommand ToggleFilterCommand { get; }

        public SettingsViewModel(Settings settings, ChangeWordFilterAction changeWordFilterAction, ModelChangeNotifier modelChangeNotifier)
            : base(modelChangeNotifier, settings)
        {
            this.settings = settings;
            this.changeWordFilterAction = changeWordFilterAction;
            ToggleFilterCommand = new RelayCommand(_ => ToggleWordFilter());
        }

        private async void ToggleWordFilter()
        {
            var wordFilter = settings.WordFilter == WordFilter.None ? WordFilter.JLPTN5 : WordFilter.None;
            await this.changeWordFilterAction.ExecuteAndNotifyAsync(wordFilter);
        }
    }
}