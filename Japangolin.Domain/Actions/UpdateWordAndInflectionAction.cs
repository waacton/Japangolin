namespace Wacton.Japangolin.Domain.Actions
{
    using Wacton.Japangolin.Domain.Mains;
    using Wacton.Japangolin.Domain.MVVM;
    
    public class UpdateWordAndInflectionAction : ModelChangeAction
    {
        private readonly Main main;

        public UpdateWordAndInflectionAction(ModelChangeNotifier modelChangeNotifier, Main main)
            : base(modelChangeNotifier, main)
        {
            this.main = main;
        }

        protected override void Action()
        {
            main.UpdateWordAndInflection();
        }
    }
}
