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
        public int[,] backGround;
        /// <summary>
        /// 当前砖块坐标,状态和形状
        /// </summary>
        public int currentX;
        public int currentY;
        //private BlocksShape currentShape;
        //private BlocksStatus currentStatus;
        /// <summary>
        /// 下一个砖块状态和形状
        /// </summary>
        private BlocksShape nextShape;
        private BlocksStatus nextStatus;

        private bool _stopAndStart;
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
        // 砖块变形态命令
        public static RoutedCommand BlocksTransformationCommand =
            new RoutedCommand("Blocks Transformation", typeof(GameViewModel));
        // 砖块左移
        public static RoutedCommand BlocksLeftCommand =
            new RoutedCommand("Blocks Left", typeof(GameViewModel));
        // 砖块右移
        public static RoutedCommand BlocksRightCommand =
            new RoutedCommand("Blocks Right", typeof(GameViewModel));
        // 暂停
        public static RoutedCommand BlocksStopCommand =
            new RoutedCommand("Blocks Stop", typeof(GameViewModel));

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
                OnPropertyChange("NextBlocks");
            }
        }

        public BlocksShape NextShape
        {
            get
            {
                return nextShape;
            }

            set
            {
                nextShape = value;
                OnPropertyChange("NextShape");
            }
        }

        public BlocksStatus NextStatus
        {
            get
            {
                return nextStatus;
            }

            set
            {
                nextStatus = value;
                OnPropertyChange("NextStatus");
            }
        }

        public bool StopAndStart
        {
            get
            {
                return _stopAndStart;
            }

            set
            {
                _stopAndStart = value;
                OnPropertyChange("StopAndStart");
            }
        }

        public BlocksControl GetCurrentBlocks()
        {
            return NextBlocks;
        }

        public BlocksControl GetNextBlocks()
        {
            Random random = new Random();
            NextShape = (BlocksShape)random.Next(1, 7);
            NextStatus = (BlocksStatus)random.Next(0, 4);
            return new BlocksControl(NextShape, NextStatus);
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
            Level = 1;
            Score = 0;
            currentX = 5;
            currentY = -4;
        }
        public void StartNewGame()
        {
            CreateGame();
            CreatePlayer();
            InitializationGame();
            StopAndStart = true;
        }
    }
}
