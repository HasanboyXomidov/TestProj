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

namespace BurgerMenu_Desktop.Windows.Products
{
    /// <summary>
    /// Interaction logic for ProductQuantityQuickAddWindow.xaml
    /// </summary>
    public partial class ProductQuantityQuickAddWindow : Window
    {
        public ProductQuantityQuickAddWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_PreviewTextInputOnlyDigit(object sender, TextCompositionEventArgs e)
        {

        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void tbStartingPrice_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
