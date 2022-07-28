using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public class Settings
    {
        private double _Volume = 1;
        public double Volume { get { return _Volume; } set { _Volume = value; } }
    }
}
