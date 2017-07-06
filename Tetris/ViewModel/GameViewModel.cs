using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace Tetris.ViewModel
{
    public class GameViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// 背景大小
        /// </summary>
        public readonly int backGroundX = 12;
        public readonly int backGroundY = 20;
        public static int[,] backGround;
        /// <summary>
        /// 当前砖块坐标,状态和形状
        /// </summary>
        public int currentX = 0;
        public int currentY = 0;
        public BlocksShape currentShape;
        public BlocksStatus currentStatus;
        /// <summary>
        /// 下一个砖块状态和形状
        /// </summary>
        public BlocksShape nextShape;
        public BlocksStatus nextStatus;


        private int _score, _level;
        private Player _currentPlayer;
        private BlocksControl currentBlocks, nextBlocks;
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public GameViewModel()
        {
            CurrentPlayer = new Player();
        }
        // 开始新游戏路由命令
        public static RoutedCommand StartNewGameCommand =
            new RoutedCommand("Start New Game", typeof(GameViewModel),
                new InputGestureCollection(new List<InputGesture> { new KeyGesture(Key.N, ModifierKeys.Control) }));
        // 打开About
        public static RoutedCommand ShowAboutCommand =
            new RoutedCommand("Show About", typeof(GameViewModel),
                new InputGestureCollection(new List<InputGesture> { new KeyGesture(Key.A, ModifierKeys.Control) }));

        public int Score
        {
            get
            {
                return _score;
            }

            set
            {
                _score = value;
                OnPropertyChange("Score");
            }
        }

        public int Level
        {
            get
            {
                return _level;
            }

            set
            {
                _level = value;
                OnPropertyChange("Level");
            }
        }

        public Player CurrentPlayer
        {
            get
            {
                return _currentPlayer;
            }

            set
            {
                _currentPlayer = value;
                OnPropertyChange("CurrentPlayer");
            }
        }

        public BlocksControl CurrentBlocks
        {
            get
            {
                return currentBlocks;
            }

            set
            {
                currentBlocks = value;
                OnPropertyChange("CurrentBlocks");
            }
        }

        public BlocksControl NextBlocks
        {
            get
            {
                return nextBlocks;
            }

            set
            {
                nextBlocks = value;
                OnPropertyChange("NextCurrentBlocks");
            }
        }

        public BlocksControl GetCurrentBlocks()
        {
            currentShape = NextBlocks.BlocksShape;
            currentStatus = NextBlocks.BlocksStatus;
            return NextBlocks;
        }

        public BlocksControl GetNextBlocks()
        {
            Random random = new Random();
            nextShape = (BlocksShape)random.Next(0, Enum.GetNames(typeof(BlocksShape)).Length);
            nextStatus = (BlocksStatus)random.Next(0, Enum.GetNames(typeof(BlocksStatus)).Length);
            return new BlocksControl(nextShape, nextStatus);
        }

        public void CreateGame()
        {
            CurrentBlocks = GetNextBlocks();
            NextBlocks = GetNextBlocks();
            backGround = new int[backGroundY, backGroundX];
        }
        public void CreatePlayer()
        {
            CurrentPlayer.Name = "Admin";
            CurrentPlayer.HighestScore = 0;
        }
        public void InitializationGame()
        {
            Level = 5;
            Score = 0;
            currentX = 6;
            currentY = -3;
            currentShape = CurrentBlocks.BlocksShape;
            currentStatus = CurrentBlocks.BlocksStatus;
        }
        public void StartNewGame()
        {
            CreateGame();
            CreatePlayer();
            InitializationGame();
        }
    }
}
