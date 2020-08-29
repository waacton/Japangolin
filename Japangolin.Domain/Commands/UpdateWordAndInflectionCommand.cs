namespace Wacton.Japangolin.Domain.Commands
{
    using Wacton.Japangolin.Domain.Mains;
    using Wacton.Tovarisch.MVVM;

    public class UpdateWordAndInflectionCommand : ModelChangeCommand
    {
        private readonly Main main;

        public UpdateWordAndInflectionCommand(ModelChangeNotifier modelChangeNotifier, Main main)
            : base(modelChangeNotifier, main)
        {
            this.main = main;
        }

        protected override void Action()
        {
            this.main.UpdateWordAndInflection();
        }
    }
}
