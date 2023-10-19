using BurgerMenu_Desktop.Pages.AuthPages;
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
using MySql.Data.MySqlClient;
namespace BurgerMenu_Desktop.Windows.AuthorizationWindows
{
    /// <summary>
    /// Логика взаимодействия для LoginRegisterWindow.xaml
    /// </summary>
    public partial class LoginRegisterWindow : Window
    {
           
     


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoginPage loginPage = new LoginPage();
            PageNavigator.Navigate(loginPage);

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void scrollWindow(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();

        }
    }
}
