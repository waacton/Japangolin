namespace Wacton.Japangolin.Domain.Actions
{
    using Wacton.Japangolin.Domain.Enums;
    using Wacton.Japangolin.Domain.Mains;
    using Wacton.Japangolin.Domain.MVVM;
    
    public class ChangeWordFilterAction : ModelChangeAction<WordFilter>
    {
        private readonly Settings settings;

        public ChangeWordFilterAction(ModelChangeNotifier modelChangeNotifier, Settings settings)
            : base(modelChangeNotifier, settings)
        {
            this.settings = settings;
        }

        protected override void Action(WordFilter wordFilter)
        {
            settings.SetWordFilter(wordFilter);
        }
    }
}
