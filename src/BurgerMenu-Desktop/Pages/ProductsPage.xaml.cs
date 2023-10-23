using BurgerMenu_Desktop.Entities.Categories;
using BurgerMenu_Desktop.Entities.SubCategories;
using BurgerMenu_Desktop.Windows.Products;
using BurgerMenu_Desktop.Windows.SubCategoryWindows;
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

namespace BurgerMenu_Desktop.Pages
{
    /// <summary>
    /// Interaction logic for ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        public ProductsPage()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            WpProduct.Children.Clear();
            Button button = new Button
            {
                Width = 150,
                Height = 90,
                Style = (Style)FindResource("SaveBtn"),
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C1D8C3")),
                Content = "Добавить +",
                Cursor = Cursors.Hand,
                FontSize = 40,
                Margin = new Thickness(10)
            };
            WpProduct.Children.Add(button);
            button.Click += btnCreateProduct;
        }
        private  void btnCreateProduct(object sender, RoutedEventArgs e)
        {
           ProductCreateWindow productCreateWindow = new ProductCreateWindow();
           productCreateWindow.ShowDialog();
        }
        public async void refreshAsync()
        {

        }

        private void btnBacktoSubCategories_Click(object sender, RoutedEventArgs e)
        {
                
        }
    }
}
