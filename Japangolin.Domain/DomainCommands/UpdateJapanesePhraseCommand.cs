namespace Wacton.Japangolin.Domain.DomainCommands
{
    using Wacton.Japangolin.Domain.Mains;
    using Wacton.Tovarisch.MVVM;

    public class UpdateJapanesePhraseCommand : DomainCommand
    {
        private readonly Main main;

        public UpdateJapanesePhraseCommand(CommandInvoker commandInvoker, Main main)
            : base(commandInvoker, main)
        {
            this.main = main;
        }

        protected override void Execute()
        {
            this.main.UpdatePhrase();
        }
    }
}
