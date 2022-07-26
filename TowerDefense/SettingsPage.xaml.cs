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
    /// Логика взаимодействия для SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        Frame frame;
        Settings settings = new Settings();
        public SettingsPage(Frame f)
        {
            InitializeComponent();
            BackgroundImage.Source = new ImageSourceConverter().ConvertFromString("Textures/wood.jpg") as ImageSource;
            frame = f;
        }

        private async void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            presssound.Position = TimeSpan.MinValue;
            presssound.Play();
            await Task.Delay(300);
            presssound.Stop();
            frame.GoBack();
        }
    }
}
