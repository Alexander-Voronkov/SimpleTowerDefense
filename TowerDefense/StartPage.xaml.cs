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
namespace TowerDefense
{
    /// <summary>
    /// Логика взаимодействия для StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        Frame frame;
        GamePage gamePage;
        SettingsPage settingsPage;
        Settings s=new Settings();
        public StartPage(Frame f)
        {
            InitializeComponent();
            Init();
            frame = f;
            s.Volume = 1;
            gamePage = new GamePage(frame,s);
            settingsPage = new SettingsPage(frame,s);

        }
        private void Init()
        {
            StartScreen.Source = new ImageSourceConverter().ConvertFromString("Textures/start.jpg") as ImageSource;
            StartScreen.Stretch = Stretch.Fill;
        }
        private async void SettingsButton(object sender, RoutedEventArgs e)
        {
            presssound.Position = TimeSpan.MinValue;
            presssound.Play();
            await Task.Delay(300);
            presssound.Stop();
            frame.Navigate(settingsPage);
            music.Volume = s.Volume;
        }
        private async void StartButton(object sender, RoutedEventArgs e)
        {
            presssound.Position = TimeSpan.MinValue;
            presssound.Play();
            await Task.Delay(300);
            presssound.Stop();
            music.Stop();
            frame.Navigate(gamePage);
            gamePage.LoseMusic.Volume = s.Volume;
            gamePage.WinMusic.Volume = s.Volume;
            gamePage.PlanningMusic.Volume = s.Volume;
            gamePage.PlayMusic.Volume = s.Volume;
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            (sender as MediaElement).Position = TimeSpan.MinValue;
            (sender as MediaElement).Play();
        }

        private void MediaElement_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as MediaElement).Position = TimeSpan.MinValue;
            (sender as MediaElement).Play();
        }
    }
}
