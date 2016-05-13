namespace Wacton.Desu
{
    using System.Collections.Generic;

    public class Kanji
    {
        public string Text { get; set; }
        public List<string> Informations { get; set; } = new List<string>();
        public List<string> Priorities { get; set; } = new List<string>();

        public override string ToString()
        {
            return this.Text;
        }
    }
}
