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
    /// Логика взаимодействия для GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        Random random = new Random();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        Frame frame;
        List<Point> points=new List<Point>();
        int moneyCount = 100;
        public GamePage(Frame f)
        {
            InitializeComponent();
            frame = f;
            Init();
            
        }

        private void Init()
        {
            PlanningMusic.Play();
            Background.ImageSource = (new Random().Next(0, 10) % 2 == 0) ? new ImageSourceConverter().ConvertFromString("Textures/BackgroundObjects/Winter/Background.jpg") as ImageSource : new ImageSourceConverter().ConvertFromString("Textures/BackgroundObjects/Summer/Background.jpg") as ImageSource;
            RandomBackObjectsGenerator();
            StoneBrush.ImageSource = new ImageSourceConverter().ConvertFromString("Textures/stone.jpg") as ImageSource;
            foreach (var item in (Path1.Data as PathGeometry).Figures)
            {
                points.Add(item.StartPoint);
                foreach (var item1 in item.Segments)
                {
                    points.AddRange((item1 as PolyLineSegment).Points);
                }
            }
            TowerControl inf = new TowerControl(new InfernalTower(), GameMap);
            inf.TurnModifiersOff();
            TowerControl can = new TowerControl(new CannonTower(), GameMap);
            can.TurnModifiersOff();
            TowerControl arc = new TowerControl(new ArcherTower(), GameMap);
            arc.TurnModifiersOff();
            TowerGrid.Children.Add(inf);
            TowerGrid.Children.Add(can);
            TowerGrid.Children.Add(arc);
            Grid.SetRow(inf, 0);
            Grid.SetRow(can, 1);
            Grid.SetRow(arc, 2);
            goldImg.Source = new ImageSourceConverter().ConvertFromString("Textures/money.png") as ImageSource;
            heartsImg1.Source = new ImageSourceConverter().ConvertFromString("Textures/heart.png") as ImageSource;
            heartsImg2.Source = new ImageSourceConverter().ConvertFromString("Textures/heart.png") as ImageSource;
            heartsImg3.Source = new ImageSourceConverter().ConvertFromString("Textures/heart.png") as ImageSource;
            goldCount.Content = moneyCount.ToString();


            for (int j = 0; j < 1080; j += 150)
            {
                for (int i = 0; i < (1920 - (1920 / 100 * 20)); i += 150)
                {
                    Button b = new Button() { Opacity = 0.6, AllowDrop = true, Width = 150, Height = 150, Visibility = Visibility.Hidden, Background = Brushes.Transparent };
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
                        if ((Canvas.GetTop(item as Button) - 30 >= points[i + 1].Y && Canvas.GetTop(item as Button) - 30 <= points[i].Y && Canvas.GetLeft(item as Button) - 30 >= points[i + 1].X && Canvas.GetLeft(item as Button) - 30 <= points[i].X))
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
                int qqq = random.Next(0, 1080);
                Canvas.SetTop(img,qqq);
                int qqq1 = random.Next(0, 1500);
                Canvas.SetLeft(img,qqq1);
            }
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
        }

        private void PlanningMusic_MediaEnded(object sender, RoutedEventArgs e)
        {
            PlanningMusic.Stop();
            PlanningMusic.Position = TimeSpan.MinValue;
            PlanningMusic.Play();
        }

        private void StartGameBtn_Click(object sender, RoutedEventArgs e)
        {
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
            PlanningMusic.Position = TimeSpan.MinValue;
            PlanningMusic.Play();
            StartGameBtn.Content = "Start";
            PlayMusic.Stop();
            StartGameBtn.Click -= EndGameBtn_Click;
            StartGameBtn.Click += StartGameBtn_Click;
            ClearAttackers();
        }
        private void RandomCharGenerator()
        {
            int q = random.Next(3, 5);
            for (int i = 0; i < q; i++)
            {
                if (random.Next(3,5) % 3 == 0)
                {
                    GameMap.Children.Add(new AttackerControl(new Zombie(),points[0],points));
                }
                else if(random.Next(3,5) % 4==0)
                {
                    GameMap.Children.Add(new AttackerControl(new Skeleton(), points[0], points));
                }
                else if (random.Next(3,5) % 5 == 0)
                {
                    GameMap.Children.Add(new AttackerControl(new Dragon(), points[0], points));
                }
            }
            foreach (var item in GameMap.Children)
            {
                if (item is AttackerControl)
                {
                    (item as AttackerControl).Move();
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
    }
}
