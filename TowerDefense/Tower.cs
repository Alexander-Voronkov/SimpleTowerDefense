using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace TowerDefense
{
    public interface ITower
    {
        double Damage { get; set; }
        Image CurrentSprite{ get; set; }
        double AttackDelay{ get; set; }
        short Level { get; set; }
    }


    public class InfernalTower:ITower
    {
        private double _Damage = 70;
        public double Damage { get { return _Damage; }  set { _Damage = value; } }
        private Image _CurrentSprite = new Image() { Source= new ImageSourceConverter().ConvertFromString($"Textures/Towers/InfernalTower/InfernalTower1.png") as ImageSource };
        public Image CurrentSprite { get { return _CurrentSprite; } set { _CurrentSprite = value; } }

        private double _AttackDelay = 2.5;
        public double AttackDelay { get { return _AttackDelay; } set { _AttackDelay = value;} }
        private short _Level = 1;
        public short Level 
        { 
            get { return _Level; }
            set 
            {
                if (value < 4 && value > 0)
                { 
                    CurrentSprite.Source = new ImageSourceConverter().ConvertFromString($"Textures/Towers/InfernalTower/InfernalTower{value}.png") as ImageSource; 
                    _Level = value;
                }
            } 
        }
    }

    public class ArcherTower : ITower
    {
        private double _Damage = 35;
        public double Damage { get { return _Damage; } set { _Damage = value; } }
        private Image _CurrentSprite = new Image() { Source = new ImageSourceConverter().ConvertFromString($"Textures/Towers/ArcherTower/ArcherTower1.png") as ImageSource };
        public Image CurrentSprite { get { return _CurrentSprite; } set { _CurrentSprite = value; } }

        private double _AttackDelay = 1.5;
        public double AttackDelay { get { return _AttackDelay; } set { _AttackDelay = value; } }
        private short _Level = 1;
        public short Level
        {
            get { return _Level; }
            set
            {
                if (value < 4 && value > 0)
                {
                    CurrentSprite.Source = new ImageSourceConverter().ConvertFromString($"Textures/Towers/ArcherTower/ArcherTower{value}.png") as ImageSource;
                    _Level = value;
                }
            }
        }
    }

    public class CannonTower : ITower
    {
        private double _Damage = 45;
        public double Damage { get { return _Damage; } set { _Damage = value; } }
        private Image _CurrentSprite = new Image() { Source = new ImageSourceConverter().ConvertFromString($"Textures/Towers/Cannon/Cannon1.png") as ImageSource };
        public Image CurrentSprite { get { return _CurrentSprite; } set { _CurrentSprite = value; } }

        private double _AttackDelay = 2;
        public double AttackDelay { get { return _AttackDelay; } set { _AttackDelay = value; } }
        private short _Level = 1;
        public short Level
        {
            get { return _Level; }
            set
            {
                if (value < 4 && value > 0)
                {
                    CurrentSprite.Source = new ImageSourceConverter().ConvertFromString($"Textures/Towers/Cannon/Cannon{value}.png") as ImageSource;
                    _Level = value;
                }
            }
        }
    }
}
