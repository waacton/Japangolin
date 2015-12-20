namespace Wacton.Japangolin.Domain.Commands
{
    using Wacton.Japangolin.Domain.Mains;
    using Wacton.Tovarisch.MVVM;

    public class UpdateJapanesePhraseCommand : ModelCommand
    {
        private readonly Main main;

        public UpdateJapanesePhraseCommand(ModelChanger modelChanger, Main main)
            : base(modelChanger, main)
        {
            this.main = main;
        }

        protected override void Execute()
        {
            this.main.UpdatePhrase();
        }
    }
}
