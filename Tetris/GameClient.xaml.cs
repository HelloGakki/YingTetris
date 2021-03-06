﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tetris.ViewModel;

namespace Tetris
{
    /// <summary>
    /// GameClient.xaml 的交互逻辑
    /// </summary>
    public partial class GameClient : Window
    {
        public GameClient()
        {
            InitializeComponent();
        }

        private void CommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Close)
                this.Close();

            if (e.Command == GameViewModel.ShowAboutCommand)
            {
                var aboutWin = new About();
                aboutWin.ShowDialog();
            }

            if (e.Command == GameViewModel.StartNewGameCommand)
            {
                var model = new GameViewModel();
                DataContext = model;
                model.StartNewGame();
                ShowMaster.DelayClear();
                ShowMaster.AutoDownInit();
                ShowMaster.AutoDownStart();
            }

            if (ShowMaster.Game.StopAndStart)
            {
                if (e.Command == GameViewModel.BlocksTransformationCommand)
                    ShowMaster.BlocksTransformation();
                if (e.Command == GameViewModel.BlocksLeftCommand)
                    ShowMaster.DelayBlocksLeft();
                if (e.Command == GameViewModel.BlocksRightCommand)
                    ShowMaster.DelayBlocksRight();
                if (e.Command == GameViewModel.BlocksDownCommand)
                    ShowMaster.DelayBlocksDown();
            }
                if (e.Command == GameViewModel.BlocksStopCommand)
                ShowMaster.AutoDownStop();
            e.Handled = true;
        }

        private void CommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Close)
                e.CanExecute = true;
            if (e.Command == GameViewModel.StartNewGameCommand)
                e.CanExecute = true;
            if (e.Command == GameViewModel.ShowAboutCommand)
                e.CanExecute = true;
            if (e.Command == GameViewModel.BlocksTransformationCommand)
                e.CanExecute = true;
            if (e.Command == GameViewModel.BlocksLeftCommand)
                e.CanExecute = true;
            if (e.Command == GameViewModel.BlocksRightCommand)
                e.CanExecute = true;
            if (e.Command == GameViewModel.BlocksStopCommand)
                e.CanExecute = true;
            if (e.Command == GameViewModel.BlocksDownCommand)
                e.CanExecute = true;
            e.Handled = true;
        }
    }
}
