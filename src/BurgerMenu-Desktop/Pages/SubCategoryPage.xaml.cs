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

namespace BurgerMenu_Desktop.Pages.AuthPages
{
    /// <summary>
    /// Interaction logic for SubCategoryPage.xaml
    /// </summary>
    public partial class SubCategoryPage : Page
    {
        public SubCategoryPage()
        {
            InitializeComponent();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            if (NavigationService.CanGoBack)
            {
                if (Window.GetWindow(this) is MainWindow mainWindow)
                {
                    Button? buttonBackTo = mainWindow.FindName("btnBackto") as Button;
                    if (buttonBackTo !=null)
                    {
                        buttonBackTo.Visibility = Visibility.Visible;
                        //buttonBackTo.Content = "Мои магазины";
                    }
                }
            }
        }
    }

}
