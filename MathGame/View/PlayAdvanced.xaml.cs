using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MathGame.View
{
    
    public sealed partial class PlayAdvanced : Page
    {
        private Random randomMath = new Random();
        private int Score = 0, state = 1, bestScore, staticNumA, staticNumB, staticResult;
        private DispatcherTimer dispatcherTimer;
        //private Boolean RTime = true;             // sebagai variabel tampungan nilai untuk Running Time 

        void setupProgressBar()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            dispatcherTimer.Start();

            progressBar.Value = 9999;
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

        public PlayAdvanced()
        {
            this.InitializeComponent();
        }

        private int randomNumber()
        {
            return randomMath.Next(1, 9);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested -= PlayAdvanced_BackRequested;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += PlayAdvanced_BackRequested;
            bestScore = int.Parse(Common.Common.LoadSetting("BestScore"));
            txtBestScore.Text = String.Format("BEST:{0}", bestScore);
            dispatcherTimer = null;
            Playing();
        }

        private async void PlayAdvanced_BackRequested(object sender, BackRequestedEventArgs e)
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

        private int randomMathValue()
        {
            return randomMath.Next(0, 3); // 0 = +, 1 = -, 2 = *, 3 = /
        }

        // back Button :)
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            Frame.Navigate(typeof(MainPage));
        }

        // Fungsi untuk main
        private void Playing()
        {
            int numberA = randomNumber();                   // untuk random bilangan
            int numberB = randomMath.Next(0, numberA - 1);  // memberikan hasil agar bilangan random B tidak melebihi nilai dari random B
            int mathValue = randomMathValue();              // Nilai random hasil
            int result = -1;

            // Mengecek operator apa yang akan digunakan
            if(mathValue == 0)
                result = numberA + numberB;

            else if(mathValue == 1)
                result = numberA - numberB;
            
            else if(mathValue == 2)
                result = numberA * numberB;            

            else
                result = numberA / numberB;

            staticNumA = numberA;
            staticNumB = numberB;
            staticResult = result;
            txtMath.Text = String.Format("{0} ... {1} = {2}", staticNumA, staticNumB, staticResult);

            setupProgressBar();
        }

        // Plus (+)
        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            if(staticNumA + staticNumB == staticResult)
            {
                txtScore.Text = String.Format("Score:{0}".ToUpper(), ++Score);
                txtState.Text = String.Format("{0}", ++state);
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

        // Decrement (-)
        private void btnDec_Click(object sender, RoutedEventArgs e)
        {
            if (staticNumA - staticNumB == staticResult)
            {
                txtScore.Text = String.Format("Score:{0}".ToUpper(), ++Score);
                txtState.Text = String.Format("{0}", ++state);
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

        // Kali (*)
        private void btnMulti_Click(object sender, RoutedEventArgs e)
        {
            if (staticNumA * staticNumB == staticResult)
            {
                txtScore.Text = String.Format("Score:{0}".ToUpper(), ++Score);
                txtState.Text = String.Format("{0}", ++state);
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

        // Bagi (/) 
        private void btnDiv_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (staticNumA / staticNumB == staticResult)
                {
                    txtScore.Text = String.Format("Score:{0}".ToUpper(), ++Score);
                    txtState.Text = String.Format("{0}", ++state);
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
            catch(DivideByZeroException)
            {
                Frame.Navigate(typeof(GameOver), Score.ToString());
            }
        }


        // Button Pause
        //private void btnPause_Click(object sender, RoutedEventArgs e)
        //{
        //    if(RTime == true)
        //    {
        //        dispatcherTimer.Stop();
        //        RTime = false;
        //        btnPause.Content = "START";
        //    }
        //    else
        //    {
        //        dispatcherTimer.Start();
        //        RTime = true;
        //        btnPause.Content = "PAUSE";
        //    }

        //}
    }
}
