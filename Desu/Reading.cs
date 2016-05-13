namespace Wacton.Desu
{
    using System.Collections.Generic;
    public class Reading
    {
        public string Text { get; set; }
        public string NoKanji { get; set; }
        public List<string> Restricted { get; set; } = new List<string>();
        public List<string> Informations { get; set; } = new List<string>();
        public List<string> Priorities { get; set; } = new List<string>();

        public override string ToString()
        {
            return this.Text;
        }
    }
}
