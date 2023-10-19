using BurgerMenu_Desktop.Interfaces.Categories;
using BurgerMenu_Desktop.Repositories.Categories;
using BurgerMenu_Desktop.Windows.CategoryWindows;
using BurgerMenu_Desktop.Windows.ShopWindows;
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
    /// Interaction logic for CategoriesPage.xaml
    /// </summary>
    public partial class CategoriesPage : Page
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesPage()
        {
            InitializeComponent();
            this._categoryRepository = new CategoryRepository();
        }
        public void setData(long id)
        {
        }
        public async Task refreshAsync()
        {
            WpCategories.Children.Clear();
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
            WpCategories.Children.Add(button);
            button.Click += btnCreateCategory;

        }
        private void btnCreateCategory(object sender, RoutedEventArgs e)
        {
            CategoryCreateWindow categoryCreateWindow = new CategoryCreateWindow();
            categoryCreateWindow.ShowDialog();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await refreshAsync();
        }
    }
}
