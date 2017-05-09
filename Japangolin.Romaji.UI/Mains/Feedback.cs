namespace Wacton.Japangolin.Romaji.UI.Mains
{
    using System.Windows.Media;

    using Wacton.Tovarisch.Enum;

    public class Feedback : Enumeration
    {
        public static readonly Feedback None = new Feedback(nameof(None), string.Empty, Brushes.White);
        public static readonly Feedback Bad = new Feedback(nameof(Bad), "馬鹿 [○・｀Д´・○]", Brushes.Tomato);
        public static readonly Feedback Good = new Feedback(nameof(Good), "万歳！！！ ヽ(=^･ω･^=)丿", Brushes.MediumSeaGreen);

        public string Text { get; }
        public SolidColorBrush Brush { get; }

        private Feedback(string displayName, string text, SolidColorBrush brush)
            : base(displayName)
        {
            this.Text = text;
            this.Brush = brush;
        }
    }


}
