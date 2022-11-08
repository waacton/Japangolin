namespace Wacton.Japangolin.Domain.Conjugation
{
    public class Hint
    {
        public string BaseForm { get; }
        public string Modification { get; }

        public Hint(string baseForm, string modification)
        {
            BaseForm = baseForm;
            Modification = modification;
        }

        public Hint(string baseForm) : this(baseForm, null)
        {
        }
    }
}