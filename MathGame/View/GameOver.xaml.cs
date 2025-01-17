﻿using System;
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
    public sealed partial class GameOver : Page
    {
        private string bestScore, localBestScore;

        public GameOver()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested -= GameOver_BackRequested;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += GameOver_BackRequested;
            bestScore = e.Parameter as String;
            localBestScore = Common.Common.LoadSetting("BestScore");
            if (int.Parse(bestScore) > int.Parse(localBestScore))
                Common.Common.SaveSettings("BestScore", bestScore);

            Score.Text = bestScore;
        }

        private async void GameOver_BackRequested(object sender, BackRequestedEventArgs e)
        {
            e.Handled = true;
            var msg = new MessageDialog("Do you want to close apps ?");
            var okBtn = new UICommand("Yes");
            var cancelBtn = new UICommand("No");
            msg.Commands.Add(okBtn);
            msg.Commands.Add(cancelBtn);
            IUICommand result = await msg.ShowAsync();

            if (result != null && result.Label.Equals("Yes"))        
                Application.Current.Exit();
        }

        private void btnTryAgain_Click(object sender, RoutedEventArgs e)
        {
            if (Common.Common.PlayMode == 1)
                Frame.Navigate(typeof(PlayAdvanced));
            else
                Frame.Navigate(typeof(PlaySingle));
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
