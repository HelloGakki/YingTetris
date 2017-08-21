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
using System.IO;

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
        /// <summary>
        /// 左移函数
        /// </summary>
        private void BlocksLeft(object Game)
        {
            var data = Game as GameViewModel;
            if (data == null)
                return;
            if (!isCheckLeft())
                return;
            if (gameSurface.Children.Contains(data.CurrentBlocks))
                gameSurface.Children.Remove(data.CurrentBlocks);
            data.currentX--;
            data.CurrentBlocks.Margin = new Thickness(data.currentX * 16, data.currentY * 16, 0, 0);
            data.CurrentBlocks.VerticalAlignment = VerticalAlignment.Top;
            data.CurrentBlocks.HorizontalAlignment = HorizontalAlignment.Left;
            gameSurface.Children.Add(data.CurrentBlocks);
        }
        /// <summary>
        /// 右移函数
        /// </summary>
        /// <param name="Game"></param>
        private void BlocksRight(object Game)
        {
            var data = Game as GameViewModel;
            if (data == null)
                return;
            if (!isCheckRight())
                return;
            if (gameSurface.Children.Contains(data.CurrentBlocks))
                gameSurface.Children.Remove(data.CurrentBlocks);
            data.currentX++;
            data.CurrentBlocks.Margin = new Thickness(data.currentX * 16, data.currentY * 16, 0, 0);
            data.CurrentBlocks.VerticalAlignment = VerticalAlignment.Top;
            data.CurrentBlocks.HorizontalAlignment = HorizontalAlignment.Left;
            gameSurface.Children.Add(data.CurrentBlocks);
        }
        /// <summary>
        /// 下降函数
        /// </summary>
        /// <param name="Game"></param>
        private void BlocksDown(object Game)
        {
            var data = Game as GameViewModel;
            if (data == null)
                return;
            if (!IsCheckDown())
            {
                NextBlockStart();
                IsGameLose();
                Eliminate();
            }
            if (gameSurface.Children.Contains(data.CurrentBlocks))
                gameSurface.Children.Remove(data.CurrentBlocks);
            data.currentY++;
            data.CurrentBlocks.Margin = new Thickness(data.currentX * 16, data.currentY * 16, 0, 0);
            data.CurrentBlocks.VerticalAlignment = VerticalAlignment.Top;
            data.CurrentBlocks.HorizontalAlignment = HorizontalAlignment.Left;
            gameSurface.Children.Add(data.CurrentBlocks);
        }
        /// <summary>
        /// 初始化自动下降函数
        /// </summary>
        public void AutoDownInit()
        {
            if (delayDownWorker != null)
                delayDownWorker.Dispose();
            delayDownWorker = new Timer(DelayAutoBlocksDown, Game, Timeout.Infinite, 50 * (10 - 9));
        }
        private void AutoDownLevelChange()
        {
            if (delayDownWorker == null || Game.StopAndStart == false)
                return;
            delayDownWorker.Change(0, 50 * (10 - Level));
        }
        /// <summary>
        /// 自动下降
        /// </summary>
        public void AutoDownStart()
        {
            delayDownWorker.Change(0, 50 * (10 - Level));
        }
        /// <summary>
        /// 停止自动下降
        /// </summary>
        public void AutoDownStop()
        {
            if (!Game.StopAndStart)
            {
                AutoDownStart();
                Game.StopAndStart = true;
            }
            else
            {
                delayDownWorker.Change(Timeout.Infinite, Timeout.Infinite);
                Game.StopAndStart = false;
            }
        }
        /// <summary>
        /// 结束自动下降
        /// </summary>
        private void AutoDownOver()
        {
            delayDownWorker.Dispose();
        }
        /// <summary>
        /// 开线程调用下降函数
        /// </summary>
        /// <param name="state"></param>
        private void DelayAutoBlocksDown(object state)
        {
            var data = state as GameViewModel;
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action<GameViewModel>(BlocksDown), data);
        }
        /// <summary>
        /// 开线程调用下移函数
        /// </summary>
        public void DelayBlocksDown()
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action<GameViewModel>(BlocksDown), Game);
        }
        /// <summary>
        /// 开线程调用左移函数
        /// </summary>
        public void DelayBlocksLeft()
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action<GameViewModel>(BlocksLeft), Game);
        }
        /// <summary>
        /// 开线程调用右移函数
        /// </summary>
        public void DelayBlocksRight()
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action<GameViewModel>(BlocksRight), Game);
        }
        /// <summary>
        /// 开线程调用Clear()
        /// </summary>
        public void DelayClear()
        {
            Dispatcher.Invoke(DispatcherPriority.DataBind, new Action<PlayGroundControl>(Clear), this);
        }
        /// <summary>
        /// 清除所有方块
        /// </summary>
        /// <param name="pay"></param>
        private void Clear(object pay)
        {
            var data = pay as PlayGroundControl;
            gameSurface.Children.Clear();
        }
        /// <summary>
        /// 判断游戏是否继续
        /// </summary>
        private void IsGameLose()
        {
            if (Game.currentY <= 0)
            {
                for (var x = 0; x < 4; x++)
                {
                    if (Game.backGround[0, x + Game.currentX] != 0)
                    {
                        AutoDownOver();
                        Game.StopAndStart = false;
                        MessageBox.Show("胜败乃兵家常事\r\n少侠请重新来过...", "GameOver");
                        return;
                    }
                }

            }
        }
        /// <summary>
        /// 查看是否能下降
        /// </summary>
        /// <returns></returns>
        private bool IsCheckDown()
        {
            for (var y = 0; y < 4; y++)
            {
                for (var x = 0; x < 4; x++)
                {
                    if (Game.CurrentBlocks.blocksArray[y, x] == 1)
                    {
                        if (y + Game.currentY < 0)
                            continue;
                        // 到底
                        if (y + Game.currentY + 1 >= Game.backGroundY)
                            return false;
                        // 下方有砖块
                        if (Game.backGround[y + Game.currentY + 1, x + Game.currentX] != 0)
                            return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 查看是否能左移
        /// </summary>
        /// <returns></returns>
        private bool isCheckLeft()
        {
            if (Game.currentY < 0)
                return false;
            for (var y = 0; y < 4; y++)
            {
                for (var x = 0; x < 4; x++)
                {
                    if (Game.CurrentBlocks.blocksArray[y, x] == 1)
                    {
                        if (x + Game.currentX - 1 < 0)
                            return false;
                        if (Game.backGround[y + Game.currentY, x + Game.currentX - 1] != 0)
                            return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 查看是否能右移
        /// </summary>
        /// <returns></returns>
        private bool isCheckRight()
        {
            if (Game.currentY < 0)
                return false;
            for (var y = 0; y < 4; y++)
            {
                for (var x = 0; x < 4; x++)
                {
                    if (Game.CurrentBlocks.blocksArray[y, x] == 1)
                    {
                        if (x + Game.currentX + 1 >= Game.backGroundX)
                            return false;
                        if (Game.backGround[y + Game.currentY, x + Game.currentX + 1] != 0)
                            return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 判断是否可以变形
        /// </summary>
        /// <returns></returns>
        private bool isCheckTransformation()
        {
            BlocksShape blocksShapeTemp = Game.CurrentBlocks.BlocksShape;
            BlocksStatus blocksStatusTemp = Game.CurrentBlocks.BlocksStatus;

            if (blocksStatusTemp + 1 > BlocksStatus.Rotation270)
                blocksStatusTemp = BlocksStatus.RotationZero;
            else
                blocksStatusTemp = blocksStatusTemp + 1;
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (BlocksControl.blockArray[(int)blocksShapeTemp - 1, (int)blocksStatusTemp, y, x] == 1)
                    {

                        if (Game.currentY < 0)
                            return false;
                        // 判断右边
                        if (x + Game.currentX >= Game.backGroundX)
                            return false;
                        // 判断左边
                        if (x + Game.currentX < 0)
                            return false;
                        // 判断下边
                        if (y + Game.currentY >= Game.backGroundY - 1)
                            return false;
                        // 判断四周是否有砖，需要放在最后一个，不然可能出险数组超界的情况
                        if (Game.backGround[y + Game.currentY, x + Game.currentX] != 0)
                            return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 砖块变形
        /// </summary>
        public void BlocksTransformation()
        {
            if (isCheckTransformation())
            {
                if (Game.CurrentBlocks.BlocksStatus + 1 > BlocksStatus.Rotation270)
                    Game.CurrentBlocks.BlocksStatus = BlocksStatus.RotationZero;
                else
                    Game.CurrentBlocks.BlocksStatus = Game.CurrentBlocks.BlocksStatus + 1;
            }
        }
        /// <summary>
        /// 生成新的砖块，并重新设定坐标
        /// </summary>
        private void NextBlockStart()
        {
            // 清除当前砖块
            if (gameSurface.Children.Contains(Game.CurrentBlocks))
                gameSurface.Children.Remove(Game.CurrentBlocks);
            // 将当前砖块散装装入背景
            foreach (BlockControl blockControl in Game.CurrentBlocks.GetBlocksList())
            {
                Point blockPoint = blockControl.TranslatePoint(new Point(0, 0), Game.CurrentBlocks.blockShowGrid);
                if (Game.currentY < 0)
                    continue;
                Game.backGround[(int)(blockPoint.Y / 16) + Game.currentY, (int)(blockPoint.X / 16) + Game.currentX] = (int)(blockControl.BlocksShape);
                BlockControl newBlockControl = new BlockControl(blockControl.BlocksShape);
                newBlockControl.Margin = new Thickness(blockPoint.X + Game.currentX * 16, blockPoint.Y + Game.currentY * 16, 0, 0);
                newBlockControl.HorizontalAlignment = HorizontalAlignment.Left;
                newBlockControl.VerticalAlignment = VerticalAlignment.Top;
                gameSurface.Children.Add(newBlockControl);
            }
            Game.currentX = 5;
            Game.currentY = -4;
            Game.CurrentBlocks = Game.GetCurrentBlocks();
            Game.NextBlocks = Game.GetNextBlocks();
        }
        /// <summary>
        /// 消除得分
        /// </summary>
        public void Eliminate()
        {

            bool isEliminate = false;
            int[,] backGroundTemp;
            for (int y = 0; y < Game.backGroundY; y++)
            {
                for (int x = 0; x < Game.backGroundX; x++)
                {
                    if (Game.backGround[y, x] != 0)
                        isEliminate = true;
                    else
                    {
                        isEliminate = false;
                        break;
                    }
                }
                if (isEliminate)
                {
                    backGroundTemp = new int[Game.backGroundY, Game.backGroundX];
                    Game.Score += 10;
                    if (Game.Level < 9 && Game.Score % 100 == 0)
                        Game.Level += 1;
                    gameSurface.Children.Clear();
                    for (int staticY = Game.backGroundY - 1; staticY > y; staticY--)
                    {
                        for (int staticX = 0; staticX < Game.backGroundX; staticX++)
                        {
                            if (Game.backGround[staticY, staticX] != 0)
                            {
                                backGroundTemp[staticY, staticX] = Game.backGround[staticY, staticX];
                                BlockControl newBlockControl = new BlockControl((BlocksShape)Game.backGround[staticY, staticX]);
                                newBlockControl.Margin = new Thickness(staticX * 16, staticY * 16, 0, 0);
                                newBlockControl.VerticalAlignment = VerticalAlignment.Top;
                                newBlockControl.HorizontalAlignment = HorizontalAlignment.Left;
                                gameSurface.Children.Add(newBlockControl);
                            }
                        }
                    }
                    for (int moveY = 1; moveY <= y; moveY++)
                    {
                        for (int moveX = 0; moveX < Game.backGroundX; moveX++)
                        {
                            if (Game.backGround[moveY - 1, moveX] != 0)
                            {
                                backGroundTemp[moveY, moveX] = Game.backGround[moveY - 1, moveX];
                                BlockControl newBlockControl = new BlockControl((BlocksShape)Game.backGround[moveY - 1, moveX]);
                                newBlockControl.Margin = new Thickness(moveX * 16, moveY * 16, 0, 0);
                                newBlockControl.VerticalAlignment = VerticalAlignment.Top;
                                newBlockControl.HorizontalAlignment = HorizontalAlignment.Left;
                                gameSurface.Children.Add(newBlockControl);
                            }
                        }
                    }
                    Game.backGround = backGroundTemp;
                }
            }

        }
    }
}
