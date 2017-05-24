namespace Wacton.Japangolin.Sentences.UI.Mains
{
    using System.Windows.Controls;

    using Wacton.Japangolin.Sentences.Domain.Mains;

    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView
    {
        public MainView()
        {
            this.InitializeComponent();
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (e.AddedItems.Count <= 0)
            //{
            //    return;
            //}

            //var wordDefinition = (Golin)e.AddedItems[0];
            //if (!wordDefinition.HasJapanese)
            //{
            //    this.ListView.SelectedIndex = -1;
            //}
        }
    }
}
