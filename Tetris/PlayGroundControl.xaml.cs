using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tetris.ViewModel;
using System.Threading;
using System.Windows.Threading;

namespace Tetris
{
    /// <summary>
    /// PlayGroundControl.xaml 的交互逻辑
    /// </summary>
    public partial class PlayGroundControl : UserControl
    {
        Timer delayDownWorker;

        public PlayGroundControl()
        {
            InitializeComponent();
        }

        public GameViewModel Game
        {
            get { return (GameViewModel)GetValue(GameProperty); }
            set { SetValue(GameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Game.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GameProperty =
            DependencyProperty.Register("Game", typeof(GameViewModel), typeof(PlayGroundControl), new PropertyMetadata(null));


        public int Level
        {
            get { return (int)GetValue(LevelProperty); }
            set { SetValue(LevelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Level.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LevelProperty =
            DependencyProperty.Register("Level", typeof(int), typeof(PlayGroundControl), new PropertyMetadata(1, new PropertyChangedCallback(OnLevelChange)));

        private static void OnLevelChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as PlayGroundControl;
            control.AutoDownLevelChange();
        }

        public void BlocksLeft(object Game)
        {
            var data = Game as GameViewModel;

            if (gameSurface.Children.Contains(data.CurrentBlocks))
                gameSurface.Children.Remove(data.CurrentBlocks);
            data.currentX--;
            data.CurrentBlocks.Margin = new Thickness(data.currentX * 16, data.currentY * 16, 0, 0);
            gameSurface.Children.Add(data.CurrentBlocks);
        }

        public void BlocksRight(object Game)
        {
            var data = Game as GameViewModel;

            if (gameSurface.Children.Contains(data.CurrentBlocks))
                gameSurface.Children.Remove(data.CurrentBlocks);
            data.currentX++;
            data.CurrentBlocks.Margin = new Thickness(data.currentX * 16, data.currentY * 16, 0, 0);
            gameSurface.Children.Add(data.CurrentBlocks);
        }

        public void BlockDown(object Game)
        {
            var data = Game as GameViewModel;
            if (data == null)
                return;
            if (!IsCheckDown())
                return;
            if (gameSurface.Children.Contains(data.CurrentBlocks))
                gameSurface.Children.Remove(data.CurrentBlocks);
            data.currentY++;
            data.CurrentBlocks.Margin = new Thickness(data.currentX * 16, data.currentY * 16, 0, 0);
            gameSurface.Children.Add(data.CurrentBlocks);
        }

        public void AutoDownInit()
        {
            if (delayDownWorker != null)
                delayDownWorker.Dispose();
            delayDownWorker = new Timer(DelayBlockDown, Game, Timeout.Infinite, 150 * (10 - 9));
        }

        private void DelayBlockDown(object state)
        {
            var data = state as GameViewModel;
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action<GameViewModel>(BlockDown), data);
        }

        private void AutoDownLevelChange()
        {
            if (delayDownWorker == null)
                return;
            delayDownWorker.Change(0, 150 * (10 - Level));
        }

        public void AutoDownStart()
        {
            delayDownWorker.Change(0, 150 * (10 - Level));
        }

        public void DelayClear()
        {
            Dispatcher.Invoke(DispatcherPriority.DataBind, new Action<PlayGroundControl>(Clear), this);
        }

        private void Clear(object pay)
        {
            var data = pay as PlayGroundControl;
            gameSurface.Children.Clear();
        }
        private bool IsCheckDown()
        {
            foreach (BlockControl block in Game.CurrentBlocks.blockShowCanvas.Children)
            {
                Point blockPoint = block.TranslatePoint(new Point(0, 0), Game.CurrentBlocks.blockShowCanvas);
                if (blockPoint.Y / 16 + Game.currentY + 1 >= Game.backGroundY)
                    return false;
            }
            return true;
        }
    }
}
