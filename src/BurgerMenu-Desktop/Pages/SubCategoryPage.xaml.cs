using BurgerMenu_Desktop.Entities.Shops;
using BurgerMenu_Desktop.Interfaces.SubCategories;
using BurgerMenu_Desktop.Repositories.SubCategories;
using BurgerMenu_Desktop.UserControls;
using BurgerMenu_Desktop.Windows.CategoryWindows;
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

namespace BurgerMenu_Desktop.Pages.AuthPages
{
    /// <summary>
    /// Interaction logic for SubCategoryPage.xaml
    /// </summary>
    public partial class SubCategoryPage : Page
    {
        private long CategoryId {  get; set; }  
        private readonly ISubCategoryRepository _subCategoryRepository;
        private string? ShopName { get; set; }
        public SubCategoryPage()
        {
            InitializeComponent();
            this._subCategoryRepository = new SubCategoryRepository();
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
        public void setCategoryid(long CategoryId,string CategoryName,string ShopName)
        {
            lblSubCategoryName.Content = CategoryName;
            this.CategoryId = CategoryId;
            this.ShopName = ShopName;
        }      

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            lblSubCategoryName1.Content = this.ShopName;
            refreshPage();
        }
        public async void refreshPage()
        {
            WpSubCategory.Children.Clear();
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
            WpSubCategory.Children.Add(button);
            button.Click += btnCreateSubCategory;
            var dbResult = await _subCategoryRepository.GetAllAsync();
            foreach (var subCategory in dbResult)
            {
                SubCategoryUserControl subCategoryUserControl = new SubCategoryUserControl();
                subCategoryUserControl.setData(subCategory);
                subCategoryUserControl.RefreshPage=refreshPage;
                WpSubCategory.Children.Add(subCategoryUserControl);
            }
        }
        private async void btnCreateSubCategory(object sender, RoutedEventArgs e)
        {
            SubCategoryCreateWindow subCategoryCreateWindow = new SubCategoryCreateWindow();
            subCategoryCreateWindow.setData(CategoryId);
            subCategoryCreateWindow.RefreshPage= refreshPage;
            subCategoryCreateWindow.ShowDialog();
        }

        private void btnBacktoCategories_Click(object sender, RoutedEventArgs e)
        {
            CategoriesPage categoriesPage = new CategoriesPage();
            NavigationService?.Navigate(categoriesPage);
        }
    }

}
