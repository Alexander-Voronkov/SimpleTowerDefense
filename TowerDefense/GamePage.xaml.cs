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
        Frame frame;
        List<Point> points=new List<Point>();
        public GamePage(Frame f)
        {
            InitializeComponent();
            Background.ImageSource = (new Random().Next(0,10)%2==0)? new ImageSourceConverter().ConvertFromString("Textures/BackgroundObjects/Winter/Background.jpg") as ImageSource: new ImageSourceConverter().ConvertFromString("Textures/BackgroundObjects/Summer/Background.jpg") as ImageSource;
            frame = f;
            StoneBrush.ImageSource = new ImageSourceConverter().ConvertFromString("Textures/stone.jpg") as ImageSource;
            points.Add(new Point(500,900));
            points.Add(new Point(500,300));
            points.Add(new Point(900,300));
            points.Add(new Point(900,900));
            points.Add(new Point(1300,900));
            points.Add(new Point(1300,100));
            points.Add(new Point(0,100));            
        }

        private void Page_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                TowerControl tw = new TowerControl(new Dragon(), new Point(0, 900), points);
                GameMap.Children.Add(tw);
                tw.Move();
            }
            else if(e.RightButton==MouseButtonState.Pressed)
            {
                TowerControl tw1 = new TowerControl(new Skeleton(), new Point(0, 900), points);
                GameMap.Children.Add(tw1);
                tw1.Move();
            }
        }
    }
}
