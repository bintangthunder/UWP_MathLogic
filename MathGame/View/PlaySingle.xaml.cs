using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MathGame.View
{
    public sealed partial class PlaySingle : Page
    {
        private int staticNumA, staticNumB, staticResult, staticRandomResult, Score = 0, State = 1, BestScore = 0, mode;
        private DispatcherTimer dispatcherTimer;
        //private EventHandler<object> DispatcherTimer_Tick;

        private void setupProgresbar()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            dispatcherTimer.Start();

            progressBar.Value = 9999;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested -= PlaySingle_BackRequested;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            Frame.Navigate(typeof(MainPage));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += PlaySingle_BackRequested;
            dispatcherTimer = null;

            Playing();
        }

        private async void PlaySingle_BackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
        {
            e.Handled = true;
            dispatcherTimer.Stop();
            dispatcherTimer = null;

            var msg = new MessageDialog("Do you want to stop ?");
            var okBtn = new UICommand("Yes");
            var cancelBtn = new UICommand("No");
            msg.Commands.Add(okBtn);
            msg.Commands.Add(cancelBtn);
            IUICommand result = await msg.ShowAsync();

            if (result != null && result.Label.Equals("Yes"))
                Frame.Navigate(typeof(GameOver), Score.ToString()); // navigate GameOver page and Send
        }

        private void DispatcherTimer_Tick(object sender, object e)
        {
            progressBar.Value -= Common.Common.Speed;
            if(progressBar.Value <= 0)
            {
                dispatcherTimer.Stop();
                dispatcherTimer = null;

                Frame.Navigate(typeof(GameOver), Score.ToString());
            }
        }

        public PlaySingle()
        {
            this.InitializeComponent();
        }

        private void btnTrue_Click(object sender, RoutedEventArgs e)
        {
            if(mode == 1) // mode - 1 so correct answer is TRUE
            {
                txtScore.Text = String.Format("Score:{0}".ToUpper(), ++Score);
                txtState.Text = String.Format("(0)", ++State);
                dispatcherTimer.Stop();
                dispatcherTimer = null;
                Playing();
            }
            else
            {
                dispatcherTimer.Stop();
                dispatcherTimer = null;

                Frame.Navigate(typeof(GameOver), Score.ToString());
            }
        }

        private void btnFalse_Click(object sender, RoutedEventArgs e)
        {
            if (mode == 0) // mode = 0 so correct answer is TRUE
            {
                txtScore.Text = String.Format("Score:{0}".ToUpper(), ++Score);
                txtState.Text = String.Format("(0)", ++State);
                dispatcherTimer.Stop();
                dispatcherTimer = null;
                Playing();
            }
            else
            {
                dispatcherTimer.Stop();
                dispatcherTimer = null;

                Frame.Navigate(typeof(GameOver), Score.ToString());
            }
        }

        private void Playing()
        {
            Random rd = new Random();
            int value = rd.Next(1, 4);
            if (value == 1) // +
            {
                // membuat bilangan random -> create random number
                staticNumA = rd.Next(1, 9);
                staticNumB = rd.Next(0, staticNumA - 1); // nilai si b tidak lebih dari 8
                staticResult = staticNumA + staticNumB;
                staticRandomResult = rd.Next(0, 17); // random hasil maksimal 17

                mode = rd.Next(0, 1); // Random mode show answer. If mode = 0  show incorrect result

                if(mode == 0)
                    txtMath.Text = String.Format("{0} + {1} = {2}", staticNumA, staticNumB, staticRandomResult);
                else
                    txtMath.Text = String.Format("{0} + {1} = {2}", staticNumA, staticNumB, staticResult);
            }

            if (value == 2) // -
            {
                staticNumA = rd.Next(1, 9);
                staticNumB = rd.Next(0, staticNumA - 1);
                staticResult = staticNumA - staticNumB;
                staticRandomResult = rd.Next(0, 9);

                mode = rd.Next(0, 1); // Random mode show answer. If mode = 0  show incorrect result

                if (mode == 0)
                    txtMath.Text = String.Format("{0} - {1} = {2}", staticNumA, staticNumB, staticRandomResult);
                else
                    txtMath.Text = String.Format("{0} - {1} = {2}", staticNumA, staticNumB, staticResult);
            }

            if (value == 3) // * 
            {
                staticNumA = rd.Next(1, 9);
                staticNumB = rd.Next(0, staticNumA - 1);
                staticResult = staticNumA * staticNumB;
                staticRandomResult = rd.Next(0, 72);

                mode = rd.Next(0, 1); // Random mode show answer. If mode = 0  show incorrect result
                if (mode == 0)
                    txtMath.Text = String.Format("{0} * {1} = {2}", staticNumA, staticNumB, staticRandomResult);
                else
                    txtMath.Text = String.Format("{0} * {1} = {2}", staticNumA, staticNumB, staticResult);
            }

            if (value == 4) // / 
            {
                staticNumA = rd.Next(1, 9);
                staticNumB = rd.Next(1, staticNumA); // Because we use divided, we change something
                staticResult = staticNumA / staticNumB;
                staticRandomResult = rd.Next(0, 10);

                mode = rd.Next(0, 1); // Random mode show answer. If mode = 0  show incorrect result

                if (mode == 0)
                    txtMath.Text = String.Format("{0} / {1} = {2}", staticNumA, staticNumB, staticRandomResult);
                else
                    txtMath.Text = String.Format("{0} / {1} = {2}", staticNumA, staticNumB, staticResult);
            }

            setupProgresbar();
        }
    }
}
