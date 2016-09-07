using Windows.ApplicationModel.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MathGame.View
{
    public sealed partial class Utama : Page
    {
        public Utama()
        {
            this.InitializeComponent();
        }

        private void btnMainPage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void btnGameDua_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MenujuGameDua));
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(About));
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            CoreApplication.Exit();
        }
    }
}
