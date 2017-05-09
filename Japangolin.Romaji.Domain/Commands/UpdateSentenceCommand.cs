namespace Wacton.Japangolin.Romaji.Domain.Commands
{
    using Wacton.Japangolin.Romaji.Domain.Mains;
    using Wacton.Tovarisch.MVVM;

    public class UpdateSentenceCommand : ModelChangeCommand
    {
        private readonly SentenceMain sentenceMain;

        public UpdateSentenceCommand(ModelChangeNotifier modelChangeNotifier, SentenceMain sentenceMain)
            : base(modelChangeNotifier, sentenceMain)
        {
            this.sentenceMain = sentenceMain;
        }

        protected override void Action()
        {
            this.sentenceMain.UpdateSentence();
        }
    }
}
