namespace Wacton.Japangolin.Domain.Commands
{
    using Wacton.Japangolin.Domain.Enums;
    using Wacton.Japangolin.Domain.Mains;
    using Wacton.Tovarisch.MVVM;

    public class ChangeWordFilterCommand : ModelChangeCommand<WordFilter>
    {
        private readonly Settings settings;

        public ChangeWordFilterCommand(ModelChangeNotifier modelChangeNotifier, Settings settings)
            : base(modelChangeNotifier, settings)
        {
            this.settings = settings;
        }

        protected override void Action(WordFilter wordFilter)
        {
            this.settings.SetWordFilter(wordFilter);
        }
    }
}
