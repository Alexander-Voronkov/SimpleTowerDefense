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
using System.Windows.Media.Animation;

namespace TowerDefense
{
    /// <summary>
    /// Логика взаимодействия для TowerControl.xaml
    /// </summary>
    public partial class TowerControl : UserControl
    {
        public System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        ITower tower;
        Canvas buttons;
        Label moneyCount;
        public double Damage { get { return tower.Damage; } }
        public void TurnModifiersOff()
        {
            this.MouseEnter -= UserControl_MouseEnter;
            this.MouseLeave -= UserControl_MouseLeave;
        }
        private async void Tick(object sender, EventArgs e)
        {
            AttackerControl t;
            if ((t = CheckRadius()) != null)
            {
                Image bullet = new Image() { Source = tower.ShootImage.Source, Width = 30, Height = 30 };
                buttons.Children.Add(bullet);
                Canvas.SetTop(bullet, Canvas.GetTop(this)+Height/2);
                Canvas.SetLeft(bullet, Canvas.GetLeft(this)+Width/2);
                while (true)
                {
                    if (Canvas.GetTop(bullet) == Canvas.GetTop(t)+ Math.Round(t.Height / 3) && Canvas.GetLeft(bullet) == Canvas.GetLeft(t)+ Math.Round(t.Width / 3))
                    {
                        t.GetDamaged(this);
                        buttons.Children.Remove(bullet);
                        return;
                    }
                    else if (Canvas.GetTop(t) + Math.Round(t.Height/3) < Canvas.GetTop(bullet) && Canvas.GetLeft(t) + Math.Round(t.Width / 3) < Canvas.GetLeft(bullet) )
                    {
                        Canvas.SetTop(bullet, Canvas.GetTop(bullet) - 1);
                        Canvas.SetLeft(bullet, Canvas.GetLeft(bullet) - 1);
                    }
                    else if (Canvas.GetTop(t) + Math.Round(t.Height / 3) > Canvas.GetTop(bullet)  && Canvas.GetLeft(t) + Math.Round(t.Width / 3) > Canvas.GetLeft(bullet) )
                    {
                        Canvas.SetTop(bullet, Canvas.GetTop(bullet) + 1);
                        Canvas.SetLeft(bullet, Canvas.GetLeft(bullet) + 1);
                    }
                    else if (Canvas.GetTop(t) + Math.Round(t.Height / 3) > Canvas.GetTop(bullet)  && Canvas.GetLeft(t) + Math.Round(t.Width / 3) < Canvas.GetLeft(bullet))
                    {
                        Canvas.SetTop(bullet, Canvas.GetTop(bullet) + 1);
                        Canvas.SetLeft(bullet, Canvas.GetLeft(bullet) - 1);
                    }
                    else if (Canvas.GetTop(t) + Math.Round(t.Height / 3) < Canvas.GetTop(bullet)  && Canvas.GetLeft(t) + Math.Round(t.Width / 3) > Canvas.GetLeft(bullet) )
                    {
                        Canvas.SetTop(bullet, Canvas.GetTop(bullet) - 1);
                        Canvas.SetLeft(bullet, Canvas.GetLeft(bullet) + 1);
                    }
                    else if (Canvas.GetTop(t) + Math.Round(t.Height / 3) == Canvas.GetTop(bullet)  && Canvas.GetLeft(t) + Math.Round(t.Width / 3) > Canvas.GetLeft(bullet))
                    {
                        Canvas.SetLeft(bullet, Canvas.GetLeft(bullet) + 1);
                    }
                    else if (Canvas.GetTop(t) + Math.Round(t.Height / 3) > Canvas.GetTop(bullet) && Canvas.GetLeft(t) + Math.Round(t.Width / 3) == Canvas.GetLeft(bullet))
                    {
                        Canvas.SetTop(bullet, Canvas.GetTop(bullet) + 1);
                    }
                    else if (Canvas.GetTop(t) + Math.Round(t.Height / 3) < Canvas.GetTop(bullet) && Canvas.GetLeft(t) + Math.Round(t.Width / 3) == Canvas.GetLeft(bullet))
                    {
                        Canvas.SetTop(bullet, Canvas.GetTop(bullet) - 1);
                    }
                    else if (Canvas.GetTop(t) + Math.Round(t.Height / 3) == Canvas.GetTop(bullet)  && Canvas.GetLeft(t) + Math.Round(t.Width / 3) < Canvas.GetLeft(bullet))
                    {
                        Canvas.SetLeft(bullet, Canvas.GetLeft(bullet) - 1);
                    }
                    await Task.Delay(TimeSpan.FromMilliseconds(0.5));
                }
            }
        }
        public void SwitchUpgrade()
        {
            UpgradeButton.IsEnabled=!UpgradeButton.IsEnabled;
        }
        private AttackerControl CheckRadius()
        {
            for (int i = 0; i < buttons.Children.Count; i++)
            {
                if (buttons.Children[i] is AttackerControl&& (Math.Pow(Canvas.GetTop(buttons.Children[i] as AttackerControl) - Canvas.GetTop(this) + 75, 2) + Math.Pow(Canvas.GetLeft(buttons.Children[i] as AttackerControl) - Canvas.GetLeft(this) + 75, 2)) <= Math.Pow(tower.Radius, 2))
                {
                    return (AttackerControl)buttons.Children[i];
                }
            }
            return null;
        }
        public double GetCost()
        {
            return tower.Cost;
        }
        public void MakeUndroppable()
        {
            this.MouseMove-= UserControl_MouseMove;
        }
        public TowerControl(ITower temp, Canvas b,Label goldCount)
        {
            InitializeComponent();
            tower = temp;
            buttons = b;
            moneyCount = goldCount;
            TowerImage.Source = tower.CurrentSprite.Source;
            TowerName.Content = $"{tower.GetType().Name} Level {tower.Level}";
            earthbrush.ImageSource = new ImageSourceConverter().ConvertFromString("Textures/earth.png") as ImageSource;
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(tower.AttackDelay);
            dispatcherTimer.Tick += Tick;
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
            if (tower.Level < 3&& Convert.ToDouble(moneyCount.Content) - GetCost() >= 0)
            {
                this.tower.Level++;
                tower.AttackDelay -= 300;
                tower.Damage+=tower.Damage;
                dispatcherTimer.Interval = TimeSpan.FromMilliseconds(tower.AttackDelay);
                TowerImage.Source = tower.CurrentSprite.Source;
                TowerName.Content = $"{tower.GetType().Name} Level {tower.Level}";
                moneyCount.Content = Convert.ToDouble(moneyCount.Content) - GetCost();

                if (tower.Level == 3 || Convert.ToDouble(moneyCount.Content)-GetCost() < 0)
                    UpgradeButton.IsEnabled = false;
            }
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                if (tower.Cost > Convert.ToDouble(moneyCount.Content))
                    return;
                foreach (var item in buttons.Children)
                {
                    if (item is Button)
                        ((Button)item).Visibility = Visibility.Visible;
                }
                if(tower is InfernalTower)
                    DragDrop.DoDragDrop(this, new TowerControl(new InfernalTower(), buttons,moneyCount) { Width = 150, Height = 150 }, DragDropEffects.Copy);
                else if(tower is ArcherTower)
                    DragDrop.DoDragDrop(this, new TowerControl(new ArcherTower(), buttons,moneyCount) { Width = 150, Height = 150 }, DragDropEffects.Copy);
                else if (tower is CannonTower)
                    DragDrop.DoDragDrop(this, new TowerControl(new CannonTower(), buttons,moneyCount) { Width=150,Height=150}, DragDropEffects.Copy);

            }
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            UpgradeButton.Visibility = Visibility.Visible;
            RemoveButton.Visibility = Visibility.Visible;
            TowerName.Visibility = Visibility.Visible;
            if (Convert.ToDouble(moneyCount.Content) < tower.Cost)
                UpgradeButton.IsEnabled= false;
            else
                UpgradeButton.IsEnabled = true;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            UpgradeButton.Visibility = Visibility.Hidden;
            RemoveButton.Visibility = Visibility.Hidden;
            TowerName.Visibility = Visibility.Hidden;            
        }
    }
}
