namespace Wacton.Japangolin.Domain.Conjugation
{
    public class Hint
    {
        public string BaseForm { get; }
        public string Modification { get; }

        public Hint(string baseForm, string modification)
        {
            this.BaseForm = baseForm;
            this.Modification = modification;
        }

        public Hint(string baseForm) : this(baseForm, null)
        {
        }
    }
}