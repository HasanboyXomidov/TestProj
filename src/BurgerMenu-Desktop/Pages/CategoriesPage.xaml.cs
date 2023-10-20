using BurgerMenu_Desktop.Interfaces.Categories;
using BurgerMenu_Desktop.Repositories.Categories;
using BurgerMenu_Desktop.UserControls;
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
        private long ShopId { get; set; }
        private string ShopName { get; set; }
        private readonly ICategoryRepository? _categoryRepository;
        public CategoriesPage()
        {
            InitializeComponent();
            this._categoryRepository = new CategoryRepository();
        }
        public void setData(long id,string shopName)
        {
            this.ShopId = id;
            this.ShopName = shopName;
            lblShopName.Content = shopName;
        }
        public async void refreshAsync()
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
            var dbResult = await _categoryRepository.GetAllByIdAsync(ShopId);
            if(dbResult.Count > 0)
            {
                foreach (var category in dbResult)
                {
                    CategoryUserControl  categoryUserControl = new CategoryUserControl();
                    categoryUserControl.setData(category);
                    categoryUserControl.RefreshPage=refreshAsync;
                    WpCategories.Children.Add(categoryUserControl);
                }
            }

        }
        private void btnCreateCategory(object sender, RoutedEventArgs e)
        {
            CategoryCreateWindow categoryCreateWindow = new CategoryCreateWindow();
            categoryCreateWindow.getShopID(ShopId);
            categoryCreateWindow.RefreshPage=refreshAsync;
            categoryCreateWindow.ShowDialog();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            refreshAsync();
        }
    }
}
