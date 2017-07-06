using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    public class Player
    {
        private string _name;
        private int _highestScore;

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        public int HighestScore
        {
            get
            {
                return _highestScore;
            }

            set
            {
                _highestScore = value;
            }
        }
    }
}
