namespace Wacton.Japangolin.Romaji.Domain.Commands
{
    using Wacton.Japangolin.Romaji.Domain.Mains;
    using Wacton.Tovarisch.MVVM;

    public class UpdateJapanesePhraseCommand : ModelChangeCommand
    {
        private readonly RomajiMain romajiMain;

        public UpdateJapanesePhraseCommand(ModelChangeNotifier modelChangeNotifier, RomajiMain romajiMain)
            : base(modelChangeNotifier, romajiMain)
        {
            this.romajiMain = romajiMain;
        }

        protected override void Action()
        {
            this.romajiMain.UpdatePhrase();
        }
    }
}
