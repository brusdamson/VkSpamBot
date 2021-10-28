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
using System.Windows.Shapes;
using VkNet;

namespace WPFSpamBot
{
    /// <summary>
    /// Логика взаимодействия для WindowAuthorization.xaml
    /// </summary>
    public partial class WindowAuthorization : Window
    {
        public static VkApi api;
        public WindowAuthorization()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (SaveData.IsChecked == true)
            {
                Properties.Settings.Default.login = LoginBox.Text.ToString();
                Properties.Settings.Default.password = PassBox.Password.ToString();
                Properties.Settings.Default.statusCheck = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.login = null;
                Properties.Settings.Default.password = null;
                Properties.Settings.Default.statusCheck = false;
                Properties.Settings.Default.Save();
            }
            string login = LoginBox.Text;
            string password = PassBox.Password;
            api = Authorize.Auth(login, password);
            if (api.IsAuthorized)
            {
                Close();
            }
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoginBox.Text = Properties.Settings.Default.login.ToString();
            PassBox.Password = Properties.Settings.Default.password.ToString();
            SaveData.IsChecked = Properties.Settings.Default.statusCheck;
        }
    }
}
