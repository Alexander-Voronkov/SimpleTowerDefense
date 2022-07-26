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
    /// <summary>
    /// Логика взаимодействия для TowerControl.xaml
    /// </summary>
    public partial class TowerControl : UserControl
    {
        ITower tower;
        Canvas buttons;
        public void TurnModifiersOff()
        {
            this.MouseEnter -= UserControl_MouseEnter;
            this.MouseLeave -= UserControl_MouseLeave;
        }
        public void MakeUndroppable()
        {
            this.MouseMove-= UserControl_MouseMove;
        }
        public TowerControl(ITower temp, Canvas b)
        {
            InitializeComponent();
            tower = temp;
            buttons = b;
            TowerImage.Source = tower.CurrentSprite.Source;
            TowerName.Content = $"{tower.GetType().Name} Level {tower.Level}";
            earthbrush.ImageSource = new ImageSourceConverter().ConvertFromString("Textures/earth.png") as ImageSource;
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Parent is Grid)
            {
                (this.Parent as Grid).Children.Remove(this);
            }
            else if (this.Parent is Canvas)
            {
                Point p = new Point(Canvas.GetLeft(this),Canvas.GetTop(this));
                (this.Parent as Canvas).Children.Remove(this);
                foreach (var item in buttons.Children)
                {
                    if(item is Button&&p==new Point(Canvas.GetLeft(item as Button),Canvas.GetTop(item as Button)))
                    {
                        (item as Button).Background = Brushes.Transparent;
                        (item as Button).AllowDrop = true;
                    }
                }
            }            
        }

        private void UpgradeButton_Click(object sender, RoutedEventArgs e)
        {
            if (tower.Level < 3)
            {
                this.tower.Level = (short)(tower.Level + 1);
                TowerImage.Source = tower.CurrentSprite.Source;
                TowerName.Content = $"{tower.GetType().Name} Level {tower.Level}";
            }
            else
            {
                UpgradeButton.IsEnabled = false;
            }
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                foreach (var item in buttons.Children)
                {
                    if (item is Button)
                        ((Button)item).Visibility = Visibility.Visible;
                }                
                if(tower is InfernalTower)
                    DragDrop.DoDragDrop(this, new TowerControl(new InfernalTower(), buttons) { Width = 150, Height = 150 }, DragDropEffects.Copy);
                else if(tower is ArcherTower)
                    DragDrop.DoDragDrop(this, new TowerControl(new ArcherTower(), buttons) { Width = 150, Height = 150 }, DragDropEffects.Copy);
                else if (tower is CannonTower)
                    DragDrop.DoDragDrop(this, new TowerControl(new CannonTower(), buttons) { Width=150,Height=150}, DragDropEffects.Copy);
            }
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            UpgradeButton.Visibility = Visibility.Visible;
            RemoveButton.Visibility = Visibility.Visible;
            TowerName.Visibility = Visibility.Visible;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            UpgradeButton.Visibility = Visibility.Hidden;
            RemoveButton.Visibility = Visibility.Hidden;
            TowerName.Visibility = Visibility.Hidden;
        }
    }
}
