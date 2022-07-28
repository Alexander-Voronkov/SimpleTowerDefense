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
    /// Логика взаимодействия для AttackerControl.xaml
    /// </summary>
    /// 

    public partial class AttackerControl : UserControl
    {
        Random random;
        private IAttacker attacker;
        private Point CurrentTarget;
        private List<Point> Path;
        private Image prev;
        GamePage page;
        public void GetDamaged(TowerControl t)
        {
            if (attacker.Health - t.Damage <= 0)
            { 
                (this.Parent as Canvas).Children.Remove(this);
                page.goldCount.Content = (Convert.ToDouble(page.goldCount.Content) + attacker.Health / 4).ToString();
            }
            else
            { 
                attacker.Health -= t.Damage;
                CurrentHp.Value -= t.Damage;
            }
        }
        public AttackerControl(IAttacker a, List<Point> path,Random r,GamePage p)
        {
            InitializeComponent();
            Canvas.SetTop(this, path[0].Y);
            Canvas.SetLeft(this, path[0].X);
            attacker = a;
            Path = path;
            random = r;
            CurrentTarget = Path[1];
            Init();
            ChangeImage();
            page = p;
        }
        private void Init()
        {
            CurrentHp.Value = attacker.Health;
            CurrentHp.Maximum = attacker.Health;
            CurrentHp.Minimum = 0;
        }
        private void Finish()
        {
            if (attacker.Health > 0 && CurrentHp.Value > 0&&page.StartGameBtn.Content.ToString()!="Start")
            {
                if (page.lives > 1)
                { 
                    page.lives--;
                    if(this.Parent is Canvas)
                        (this.Parent as Canvas).Children.Remove(this);
                }
                else if(page.lives == 1)
                {
                    page.lives--;
                }
            }
            else
            {
                if(this.Parent is Canvas)
                    (this.Parent as Canvas).Children.Remove(this);
            }
        }

        public async void Move()
        {
                while (true)
                {
                if (Canvas.GetTop(this) < CurrentTarget.Y && Canvas.GetLeft(this) == CurrentTarget.X)
                { Canvas.SetTop(this, Canvas.GetTop(this) + 1); }
                else if (Canvas.GetTop(this) > CurrentTarget.Y && Canvas.GetLeft(this) == CurrentTarget.X)
                { Canvas.SetTop(this, Canvas.GetTop(this) - 1); }
                else if (Canvas.GetTop(this) == CurrentTarget.Y && Canvas.GetLeft(this) > CurrentTarget.X)
                { Canvas.SetLeft(this, Canvas.GetLeft(this) - 1);  }
                else if (Canvas.GetTop(this) == CurrentTarget.Y && Canvas.GetLeft(this) < CurrentTarget.X)
                { Canvas.SetLeft(this, Canvas.GetLeft(this) + 1);  }
                else if (Canvas.GetTop(this) == CurrentTarget.Y && Canvas.GetLeft(this) == CurrentTarget.X)
                {
                    if (Path.IndexOf(CurrentTarget) + 1 != Path.Count)
                    {
                        CurrentTarget = Path[Path.IndexOf(CurrentTarget) + 1];
                    }
                    else
                    {
                        Finish();
                        return;
                    }
                }
                    ChangeImage();
                    await Task.Delay(TimeSpan.FromMilliseconds(attacker.Speed));
                }
        }

        private void ChangeImage()
        {
            if (Sprite.Source != null)
            {
                    if (Canvas.GetTop(this) <= CurrentTarget.Y && Canvas.GetLeft(this) == CurrentTarget.X)
                    {
                        if (attacker.Sprites.IndexOf(prev) < 2)
                        {
                            prev = (attacker).Sprites[attacker.Sprites.IndexOf(prev) + 1];
                            Sprite.Source = (attacker).Sprites[(attacker).Sprites.IndexOf(prev) ].Source;
                        }
                        else if ((attacker).Sprites.IndexOf(prev) >= 2)
                        {
                            prev = (attacker).Sprites[0];
                            Sprite.Source = (attacker).Sprites[0].Source;
                        }
                    }
                    else if (Canvas.GetTop(this) >= CurrentTarget.Y && Canvas.GetLeft(this) == CurrentTarget.X)
                    {
                        if ((attacker).Sprites.IndexOf(prev) < 5)
                        {
                            prev = (attacker).Sprites[(attacker).Sprites.IndexOf(prev) + 1];
                            Sprite.Source = (attacker).Sprites[(attacker).Sprites.IndexOf(prev) ].Source;
                        }
                        else if ((attacker).Sprites.IndexOf(prev) >= 5)
                        {
                            prev = (attacker).Sprites[3];
                            Sprite.Source = (attacker).Sprites[3].Source;
                        }
                    }
                    else if (Canvas.GetTop(this) == CurrentTarget.Y && Canvas.GetLeft(this) <= CurrentTarget.X)
                    {
                        if ((attacker).Sprites.IndexOf(prev) < 11)
                        {
                            prev = (attacker).Sprites[(attacker).Sprites.IndexOf(prev) + 1];
                            Sprite.Source = (attacker).Sprites[(attacker).Sprites.IndexOf(prev) ].Source;
                        }
                        else if ((attacker).Sprites.IndexOf(prev) >= 11)
                        {
                            prev = (attacker).Sprites[9];
                            Sprite.Source = (attacker).Sprites[9].Source;
                        }
                    }
                    else if (Canvas.GetTop(this) == CurrentTarget.Y && Canvas.GetLeft(this) >= CurrentTarget.X)
                    {
                        if ((attacker).Sprites.IndexOf(prev) < 8)
                        {
                            prev = (attacker).Sprites[(attacker).Sprites.IndexOf(prev) + 1];
                            Sprite.Source = (attacker).Sprites[(attacker).Sprites.IndexOf(prev) ].Source;
                        }
                        else if ((attacker).Sprites.IndexOf(prev) >= 8)
                        {
                            prev = (attacker).Sprites[6];
                            Sprite.Source = (attacker).Sprites[6].Source;

                        }
                    }
                }
            else
            {
                    if (Canvas.GetTop(this) <= CurrentTarget.Y && Canvas.GetLeft(this) == CurrentTarget.X)
                    {
                            prev = (attacker).Sprites[0];
                            Sprite.Source = (attacker).Sprites[0].Source;
                    }
                    else if (Canvas.GetTop(this) >= CurrentTarget.Y && Canvas.GetLeft(this) == CurrentTarget.X)
                    {
                        prev = (attacker).Sprites[3];
                            Sprite.Source = (attacker).Sprites[3].Source;
                    }
                    else if (Canvas.GetTop(this) == CurrentTarget.Y && Canvas.GetLeft(this) <=CurrentTarget.X)
                    {
                        prev = (attacker).Sprites[9];
                            Sprite.Source = (attacker).Sprites[9].Source;
                    }
                    else if (Canvas.GetTop(this) == CurrentTarget.Y && Canvas.GetLeft(this) >= CurrentTarget.X)
                    {
                            prev = (attacker).Sprites[6];
                            Sprite.Source = (attacker).Sprites[6].Source;
                    }
                }
            }
        }
    }
