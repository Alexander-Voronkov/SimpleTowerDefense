using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public class Settings
    {
        private int _Difficulty = 1;
        public int Difficulty { get { return _Difficulty; } set { _Difficulty = value; } }
        private double _Volume = 1;
        public double Volume { get { return _Volume; } set { _Volume = value; } }
    }
}
