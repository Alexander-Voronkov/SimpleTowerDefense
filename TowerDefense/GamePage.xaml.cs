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
using System.Threading;
using System.Windows.Media.Effects;
namespace TowerDefense
{
    /// <summary>
    /// Логика взаимодействия для GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        Random random = new Random();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        Frame frame;
        List<Point> points=new List<Point>();
        DateTime t = DateTime.MinValue;
        Settings settings;
        private short _lives = 3;
        public short lives
        {
            get { return _lives; }
            set
            {
                switch (value)
                {
                    case 3:
                        heartsImg3.Visibility = Visibility.Visible;heartsImg2.Visibility=Visibility.Visible; heartsImg1.Visibility = Visibility.Visible;
                        _lives = value; break;
                    case 2:
                        heartsImg3.Visibility = Visibility.Hidden;
                        _lives = value; break;
                    case 1:
                        heartsImg2.Visibility = Visibility.Hidden;
                        _lives = value; break;
                    case 0: heartsImg1.Visibility = Visibility.Hidden; _lives = value; Lose(); break;
                }
            }
        }
        private void Tick(object sender,EventArgs e)
        {
            goldCount.Content = Convert.ToDouble(goldCount.Content)+1;
            t = t.AddSeconds(1);
            TimeLabel.Content = t.ToLongTimeString();
            bool f = false;
            foreach (var item in GameMap.Children)
            {
                if (item is AttackerControl)
                    f = true;
            }
            if (!f)
                Win();
        }
        public GamePage(Frame f,Settings s)
        {
            InitializeComponent();
            frame = f;
            Init();
            dispatcherTimer.Tick += Tick;
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            TimeLabel.Content = t.ToLongTimeString();
            settings = s;

        }
        private async void Lose()
        {
            LoseMusic.Position = TimeSpan.MinValue;
            EndGameBtn_Click(null,null);
            PlanningMusic.Stop();
            Image image = new Image() { Width=1920,Height=1080};
            image.Source = new ImageSourceConverter().ConvertFromString("Textures/lose.gif") as ImageSource;
            maingrid.Children.Add(image);
            Grid.SetZIndex(image, 2);
            Grid.SetColumn(image, 0);
            Grid.SetRow(image, 0);
            Grid.SetRowSpan(image, 2);
            Grid.SetColumnSpan(image, 2);
            GameMap.Effect = new BlurEffect() { Radius=20,KernelType=KernelType.Gaussian};
            LoseMusic.Play();
            await Task.Delay(TimeSpan.FromSeconds(1));
            GameMap.Effect = null;
            maingrid.Children.Remove(image);
            LoseMusic.Stop();
            PlanningMusic.Play();
        }
        private async void Win()
        {
            WinMusic.Position = TimeSpan.MinValue;
            EndGameBtn_Click(null, null);
            PlanningMusic.Stop();
            Image image = new Image() { Width = 1920, Height = 1080 };
            image.Source = new ImageSourceConverter().ConvertFromString("Textures/win.png") as ImageSource;
            maingrid.Children.Add(image);
            Grid.SetZIndex(image, 2);
            Grid.SetColumn(image, 0);
            Grid.SetRow(image, 0);
            Grid.SetRowSpan(image, 2);
            Grid.SetColumnSpan(image, 2);
            GameMap.Effect = new BlurEffect() { Radius = 20, KernelType = KernelType.Gaussian };
            WinMusic.Play();
            await Task.Delay(TimeSpan.FromSeconds(1));
            GameMap.Effect = null;
            maingrid.Children.Remove(image);
            lives = 3;
            WinMusic.Stop();
            PlanningMusic.Play();
        }
        private void Init()
        {
            foreach (var item in (Path1.Data as PathGeometry).Figures)
            {
                points.Add(new Point(item.StartPoint.X - 40, item.StartPoint.Y - 40));
                foreach (var item1 in item.Segments)
                {
                    foreach (var item2 in (item1 as PolyLineSegment).Points)
                    {
                        points.Add(new Point(item2.X - 40, item2.Y - 40));
                    }
                }
            }
            goldCount.Content = 100.ToString();
            PlanningMusic.Play();
            Background.ImageSource = (new Random().Next(0, 10) % 2 == 0) ? new ImageSourceConverter().ConvertFromString("Textures/BackgroundObjects/Winter/Background.jpg") as ImageSource : new ImageSourceConverter().ConvertFromString("Textures/BackgroundObjects/Summer/Background.jpg") as ImageSource;
            RandomBackObjectsGenerator();
            StoneBrush.ImageSource = new ImageSourceConverter().ConvertFromString("Textures/stone.jpg") as ImageSource;
            
            gold1.Source = new ImageSourceConverter().ConvertFromString("Textures/money.png") as ImageSource;
            gold2.Source = new ImageSourceConverter().ConvertFromString("Textures/money.png") as ImageSource;
            gold3.Source = new ImageSourceConverter().ConvertFromString("Textures/money.png") as ImageSource;
            TowerControl inf = new TowerControl(new InfernalTower(), GameMap,goldCount);
            inf.TurnModifiersOff();
            TowerControl can = new TowerControl(new CannonTower(), GameMap, goldCount);
            can.TurnModifiersOff();
            TowerControl arc = new TowerControl(new ArcherTower(), GameMap, goldCount);
            arc.TurnModifiersOff();
            TowerGrid.Children.Add(inf);
            TowerGrid.Children.Add(can);
            TowerGrid.Children.Add(arc);
            InfernalTowerCost.Content = inf.GetCost().ToString();
            ArcherTowerCost.Content = arc.GetCost().ToString();
            CannonTowerCost.Content = can.GetCost().ToString();
            Grid.SetRow(inf, 0);
            Grid.SetRow(can, 1);
            Grid.SetRow(arc, 2);
            goldImg.Source = new ImageSourceConverter().ConvertFromString("Textures/money.png") as ImageSource;
            heartsImg1.Source = new ImageSourceConverter().ConvertFromString("Textures/heart.png") as ImageSource;
            heartsImg2.Source = new ImageSourceConverter().ConvertFromString("Textures/heart.png") as ImageSource;
            heartsImg3.Source = new ImageSourceConverter().ConvertFromString("Textures/heart.png") as ImageSource;


            for (int j = 0; j < 1080; j += 100)
            {
                for (int i = 0; i < (1920 - (1920 / 100 * 20)); i += 100)
                {
                    Button b = new Button() { Opacity = 0.6, AllowDrop = true, Width = 100, Height = 100, Visibility = Visibility.Hidden, Background = Brushes.Transparent };
                    GameMap.Children.Add(b);
                    Canvas.SetLeft(b, i);
                    Canvas.SetTop(b, j);
                    b.DragOver += DragOverButton;
                    b.DragLeave += DragLeaveButton;
                    b.Drop += DragDropButton;
                }
            }
            foreach (var item in GameMap.Children)
            {
                if (item is Button)
                {
                    for (int i = 0; i < points.Count - 1; i++)
                    {
                        if ((Canvas.GetTop(item as Button) >= points[i].Y-60&&Canvas.GetLeft(item as Button)>=points[i].X&&Canvas.GetTop(item as Button)<=points[i+1].Y&&Canvas.GetLeft(item as Button)<=points[i+1].X) || 
                            (Canvas.GetTop(item as Button) <= points[i].Y && Canvas.GetLeft(item as Button) >= points[i].X-60 && Canvas.GetTop(item as Button) >= points[i + 1].Y && Canvas.GetLeft(item as Button) <= points[i + 1].X)||
                            (Canvas.GetTop(item as Button) >= points[i+1].Y - 60 && Canvas.GetLeft(item as Button) >= points[i+1].X && Canvas.GetTop(item as Button) <= points[i].Y && Canvas.GetLeft(item as Button) <= points[i].X)||
                            (Canvas.GetTop(item as Button) <= points[i+1].Y && Canvas.GetLeft(item as Button) >= points[i].X - 60 && Canvas.GetTop(item as Button) >= points[i].Y && Canvas.GetLeft(item as Button) <= points[i].X)||

                            (Canvas.GetTop(item as Button) <= points[i].Y + 60 && Canvas.GetLeft(item as Button) >= points[i].X && Canvas.GetLeft(item as Button) <= points[i + 1].X)&&Canvas.GetTop(item as Button)>=points[i+1].Y ||
                            (Canvas.GetTop(item as Button) <= points[i].Y && Canvas.GetLeft(item as Button) <= points[i].X + 60 && Canvas.GetTop(item as Button) >= points[i + 1].Y )&&Canvas.GetLeft(item as Button)>=points[i+1].X ||
                            (Canvas.GetTop(item as Button) >= points[i].Y && Canvas.GetLeft(item as Button) >= points[i].X && Canvas.GetTop(item as Button) <= points[i+1].Y && Canvas.GetLeft(item as Button) <= points[i+1].X+60) ||
                            (Canvas.GetTop(item as Button) <= points[i + 1].Y+60 && Canvas.GetLeft(item as Button) <= points[i].X && Canvas.GetTop(item as Button) >= points[i+1].Y && Canvas.GetLeft(item as Button) >= points[i+1].X)) 
                        {
                            (item as Button).Background = Brushes.PaleVioletRed;
                            (item as Button).AllowDrop = false;
                        }
                    }
                }
            }
        }

        private void RandomBackObjectsGenerator()
        {
            string path=null;
            if (Background.ImageSource.ToString() == "Textures/BackgroundObjects/Summer/Background.jpg")
            {
                path = "Textures/BackgroundObjects/Summer/";
            }
            else
                path = "Textures/BackgroundObjects/Winter/";
            int q = random.Next(5,10);
            for (int i = 0; i < q; i++)
            {
                string t=(random.Next()%2==0)?$"{path}1.png": $"{path}2.png";
                Image img = new Image() { Width=150,Height=150};
                GameMap.Children.Add(img);
                img.Source = new ImageSourceConverter().ConvertFromString(t) as ImageSource;
                int qqq;
                int qqq1;
                do
                {
                    qqq = random.Next(0, 1080);
                    qqq1 = random.Next(0, 1500);
                }
                while (!IsAllowedArea(new Point(qqq1,qqq)));
                
                Canvas.SetTop(img, qqq);
                Canvas.SetLeft(img,qqq1);
            }
        }
        private bool IsAllowedArea(Point p)
        {
            for (int i = 0; i < points.Count - 1; i++)
            {
                if ((p.Y >= points[i].Y - 60 && p.X >= points[i].X && p.Y <= points[i + 1].Y && p.X <= points[i + 1].X) ||
                    (p.Y <= points[i].Y && p.X >= points[i].X - 60 && p.Y >= points[i + 1].Y && p.X <= points[i + 1].X) ||
                    (p.Y >= points[i + 1].Y - 60 && p.X >= points[i + 1].X && p.Y <= points[i].Y && p.X <= points[i].X) ||
                    (p.Y <= points[i + 1].Y && p.X >= points[i].X - 60 && p.Y >= points[i].Y && p.X <= points[i].X) ||
                    (p.Y <= points[i].Y + 60 && p.X >= points[i].X && p.X <= points[i + 1].X) && p.Y >= points[i + 1].Y ||
                    (p.Y <= points[i].Y && p.X <= points[i].X + 60 && p.Y >= points[i + 1].Y) && p.X >= points[i + 1].X ||
                    (p.Y >= points[i].Y && p.X >= points[i].X && p.Y <= points[i + 1].Y && p.X <= points[i + 1].X + 60) ||
                    (p.Y <= points[i + 1].Y + 60 && p.X <= points[i].X && p.Y >= points[i + 1].Y && p.X >= points[i + 1].X))
                    {
                        return false;
                    }
            }
            return true;
        }
        private void DragOverButton(object sender, DragEventArgs d)
        {
            (sender as Button).Background = Brushes.LightGreen;
        }
        private void DragLeaveButton(object sender,DragEventArgs d)
        {
            (sender as Button).Background = Brushes.Transparent;
        }
        private void DragDropButton(object sender, DragEventArgs d)
        {
            (sender as Button).Background = Brushes.PaleVioletRed;
            (sender as Button).AllowDrop = false;
            foreach (var item in GameMap.Children)
            {
                if (item is Button)
                {
                    (item as Button).Visibility = Visibility.Hidden;
                }
            }
            TowerControl newcontrol = (d.Data.GetData(typeof(TowerControl)) as TowerControl);
            newcontrol.MakeUndroppable();
            GameMap.Children.Add(newcontrol);
            Canvas.SetLeft(newcontrol, Canvas.GetLeft(sender as Button));
            Canvas.SetTop(newcontrol, Canvas.GetTop(sender as Button));
            goldCount.Content = Convert.ToDouble(goldCount.Content)-newcontrol.GetCost();
            newcontrol.dispatcherTimer.Start();
        }

        private void PlanningMusic_MediaEnded(object sender, RoutedEventArgs e)
        {
            (sender as MediaElement).Stop();
            (sender as MediaElement).Position = TimeSpan.MinValue;
            (sender as MediaElement).Play();
        }

        private void StartGameBtn_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Start();
            PlayMusic.Position = TimeSpan.MinValue;
            PlayMusic.Play();
            PlanningMusic.Stop();
            StartGameBtn.Content = "Stop";
            StartGameBtn.Click -= StartGameBtn_Click;
            StartGameBtn.Click += EndGameBtn_Click; 
            RandomCharGenerator();
        }
        private void EndGameBtn_Click(object sender, RoutedEventArgs e)
        {
            lives = 3;
            dispatcherTimer.Stop();
            t = DateTime.MinValue;
            TimeLabel.Content = t.ToLongTimeString();
            PlanningMusic.Position = TimeSpan.MinValue;
            PlanningMusic.Play();
            StartGameBtn.Content = "Start";
            PlayMusic.Stop();
            StartGameBtn.Click -= EndGameBtn_Click;
            StartGameBtn.Click += StartGameBtn_Click;
            ClearAttackers();
            goldCount.Content = 100.ToString();
            for (int i = 0; i < GameMap.Children.Count; i++)
            {
                if (GameMap.Children[i] is TowerControl)
                {
                    if ((GameMap.Children[i] as TowerControl).Parent is Grid)
                    {
                        (GameMap.Children[i] as TowerControl).dispatcherTimer.Stop();
                        ((GameMap.Children[i] as TowerControl).Parent as Grid).Children.Remove((GameMap.Children[i--] as TowerControl));
                    }
                    else if ((GameMap.Children[i] as TowerControl).Parent is Canvas)
                    {
                        Point p = new Point(Canvas.GetLeft((GameMap.Children[i] as TowerControl)), Canvas.GetTop((GameMap.Children[i] as TowerControl)));
                       (GameMap.Children[i] as TowerControl).dispatcherTimer.Stop();
                       ((GameMap.Children[i] as TowerControl).Parent as Canvas).Children.Remove((GameMap.Children[i--] as TowerControl));
                        foreach (var item1 in GameMap.Children)
                        {
                            if (item1 is Button && p == new Point(Canvas.GetLeft(item1 as Button), Canvas.GetTop(item1 as Button)))
                            {
                                (item1 as Button).Background = Brushes.Transparent;
                                (item1 as Button).AllowDrop = true;
                            }
                        }
                    }
                }
                if
                    (GameMap.Children[i] is Image&& (((GameMap.Children[i] as Image).Source.ToString()=="Textures/Animations/Arrow.png"||
                    ((GameMap.Children[i] as Image).Source.ToString() =="Textures/Animations/Cannonball.png"||
                    (GameMap.Children[i] as Image).Source.ToString() == "Textures/Animations/Arrow.png" ))))
                    {
                        ((GameMap.Children[i] as Image).Parent as Canvas).Children.Remove((GameMap.Children[i] as Image));
                    }
            }
        }
        private void RandomCharGenerator()
        {
            int q = random.Next(3, 10);
            for (int i = 0; i < q; i++)
            {
                int q1=random.Next(0, 10);
                if (q1 % 3 == 0)
                {
                    GameMap.Children.Add(new AttackerControl(new Zombie(), points, random, this, settings) { Width=150,Height=150});
                }
                else if(q1 % 3==1)
                {
                    GameMap.Children.Add(new AttackerControl(new Skeleton(), points, random,this, settings) { Width = 150, Height = 150 });
                }
                else if (q1 % 3 == 2)
                {
                    GameMap.Children.Add(new AttackerControl(new Dragon(), points, random,this, settings) { Width = 150, Height = 150 });
                }
            }
            WaitMove();
        }
        private async void WaitMove()
        {
            for (int i = 0; i < GameMap.Children.Count; i++)
            {
                if (GameMap.Children[i] is AttackerControl)
                {
                    (GameMap.Children[i] as AttackerControl).Move();
                    await Task.Delay(TimeSpan.FromMilliseconds(random.Next(0, 2000)));
                }
            }
        }
        private void ClearAttackers()
        {
            for (int i = 0; i < GameMap.Children.Count; i++)
            {
                if(GameMap.Children[i] is AttackerControl)
                {
                    GameMap.Children.Remove(GameMap.Children[i--]);
                }
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            EndGameBtn_Click(null,null);
            presssound.Position = TimeSpan.MinValue;
            presssound.Play();
            await Task.Delay(300);
            presssound.Stop();
            frame.GoBack();
        }
    }
}
