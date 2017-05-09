namespace Wacton.Japangolin.Sentences.Domain.Commands
{
    using Wacton.Japangolin.Sentences.Domain.Mains;
    using Wacton.Tovarisch.MVVM;

    public class UpdateSentenceCommand : ModelChangeCommand
    {
        private readonly Main main;

        public UpdateSentenceCommand(ModelChangeNotifier modelChangeNotifier, Main main)
            : base(modelChangeNotifier, main)
        {
            this.main = main;
        }

        protected override void Action()
        {
            this.main.UpdateSentence();
        }
    }
}
