namespace Wacton.Japangolin.Sentences.UI.Mains
{
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView
    {
        public MainView()
        {
            this.InitializeComponent();
        }

        //private void SnackBar3_OnClick(object sender, RoutedEventArgs e)
        //{
        //    //use the message queue to send a message.
        //    var messageQueue = SnackbarThree.MessageQueue;
        //    var message = MessageTextBox.Text;

        //    //the message queue can be called from any thread
        //    Task.Factory.StartNew(() => messageQueue.Enqueue(message));
        //}
    }
}
