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
        Settings settings;
        public SettingsPage(Frame f,Settings s)
        {
            InitializeComponent();
            BackgroundImage.Source = new ImageSourceConverter().ConvertFromString("Textures/wood.jpg") as ImageSource;
            frame = f;
            settings = s;
            Volume.Value = settings.Volume*10;
        }

        private async void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            presssound.Position = TimeSpan.MinValue;
            presssound.Play();
            await Task.Delay(300);
            presssound.Stop();
            frame.GoBack();
        }

        private void DiffSlider_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (VolumeSliderLabel != null)
            {
                settings.Volume = Volume.Value/10;
            }
        }
    }
}
