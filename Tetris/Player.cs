using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Tetris
{
    public class Player:INotifyPropertyChanged
    {
        private string _name;
        private int _highestScore;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public Player():this("Admin")
        {

        }

        public Player(string _name)
        {
            this._name = _name;
        }

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
                OnPropertyChange("Name");
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
                OnPropertyChange("HighestScore");
            }
        }
    }
}
