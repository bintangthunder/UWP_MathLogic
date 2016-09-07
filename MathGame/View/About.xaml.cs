using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MathGame.View
{
    public sealed partial class About : Page
    {
        public About()
        {
            this.InitializeComponent();
        }

        private void btnPlayBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Utama));
        }
    }
}
