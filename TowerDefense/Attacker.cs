using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace TowerDefense
{
    public interface IAttacker
    {
        string Name { get; set; }
        double Health { get; set; }
        double Speed { get; set; }
        List<Image> Sprites { get; }
    }

    public class Dragon : IAttacker
    {
        private string _Name;
        public string Name { get => _Name; set => _Name = value; }
        private double _Health;
        public double Health { get => _Health; set => _Health=value; }
        private double _Speed=5;
        public double Speed { get => _Speed; set => _Speed = value; }
        private List<Image> _Sprites = new List<Image>();
        public List<Image> Sprites { get => _Sprites; }
        public Dragon()
        {
            for (int i = 1; i < 4; i++)
            {
                Sprites.Add(new Image() { Source = new ImageSourceConverter().ConvertFromString($"Textures/Attackers/DragonFlying/Front{i}.png") as ImageSource }); ;
            }
            for (int i = 1; i < 4; i++)
            {
                Sprites.Add(new Image() { Source = new ImageSourceConverter().ConvertFromString($"Textures/Attackers/DragonFlying/Back{i}.png") as ImageSource }); ;
            }
            for (int i = 1; i < 4; i++)
            {
                Sprites.Add(new Image() { Source = new ImageSourceConverter().ConvertFromString($"Textures/Attackers/DragonFlying/Left{i}.png") as ImageSource }); ;
            }
            for (int i = 1; i < 4; i++)
            {
                Sprites.Add(new Image() { Source = new ImageSourceConverter().ConvertFromString($"Textures/Attackers/DragonFlying/Right{i}.png") as ImageSource }); ;
            }
        }
    }
    public class Zombie : IAttacker
    {
        private string _Name;
        public string Name { get => _Name; set => _Name = value; }
        private double _Health;
        public double Health { get => _Health; set => _Health = value; }
        private double _Speed=2.5;
        public double Speed { get => _Speed; set => _Speed = value; }
        private List<Image> _Sprites = new List<Image>();
        public List<Image> Sprites { get => _Sprites; } 
        public Zombie()
        {
            for (int i = 1; i < 4; i++)
            {
                Sprites.Add(new Image() { Source = new ImageSourceConverter().ConvertFromString($"Textures/Attackers/ZombieWalking/Front{i}.png") as ImageSource }); ;
            }
            for (int i = 1; i < 4; i++)
            {
                Sprites.Add(new Image() { Source = new ImageSourceConverter().ConvertFromString($"Textures/Attackers/ZombieWalking/Back{i}.png") as ImageSource }); ;
            }
            for (int i = 1; i < 4; i++)
            {
                Sprites.Add(new Image() { Source = new ImageSourceConverter().ConvertFromString($"Textures/Attackers/ZombieWalking/Left{i}.png") as ImageSource }); ;
            }
            for (int i = 1; i < 4; i++)
            {
                Sprites.Add(new Image() { Source = new ImageSourceConverter().ConvertFromString($"Textures/Attackers/ZombieWalking/Right{i}.png") as ImageSource }); ;
            }
        }
    }
    public class Skeleton : IAttacker
    {
        private string _Name;
        public string Name { get => _Name; set => _Name = value; }
        private double _Health;
        public double Health { get => _Health; set => _Health = value; }
        private double _Speed=3;
        public double Speed { get => _Speed; set => _Speed = value; }
        private List<Image> _Sprites=new List<Image>();
        public List<Image> Sprites { get => _Sprites; } 
        public Skeleton()
        {
            for (int i = 1; i < 4; i++)
            {
                Sprites.Add(new Image() { Source = new ImageSourceConverter().ConvertFromString($"Textures/Attackers/SkeletonWalking/Front{i}.png") as ImageSource }); ;
            }
            for (int i = 1; i < 4; i++)
            {
                Sprites.Add(new Image() { Source = new ImageSourceConverter().ConvertFromString($"Textures/Attackers/SkeletonWalking/Back{i}.png") as ImageSource }); ;
            }
            for (int i = 1; i < 4; i++)
            {
                Sprites.Add(new Image() { Source = new ImageSourceConverter().ConvertFromString($"Textures/Attackers/SkeletonWalking/Left{i}.png") as ImageSource }); ;
            }
            for (int i = 1; i < 4; i++)
            {
                Sprites.Add(new Image() { Source = new ImageSourceConverter().ConvertFromString($"Textures/Attackers/SkeletonWalking/Right{i}.png") as ImageSource }); ;
            }
        }
    }
}
