using BurgerMenu_Desktop.Entities.Categories;
using BurgerMenu_Desktop.Entities.SubCategories;
using BurgerMenu_Desktop.Interfaces.Products;
using BurgerMenu_Desktop.Pages.AuthPages;
using BurgerMenu_Desktop.Repositories.Products;
using BurgerMenu_Desktop.UserControls;
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
        private readonly IProductInterface _productInterface;
        private string ShopName { get; set; }
        private string CategoryName { get; set; }
        private string SubCategoryName { get; set; }
        private long SubCategoryId { get; set; }
        public ProductsPage()
        {
            InitializeComponent();
            this._productInterface = new ProductRepository();
        }
        public void setData(long SubCategoryId,string shopName , string CategoryName,string SubcategoryName)
        {
            this.SubCategoryId = SubCategoryId;
            lblShopName1.Content = shopName;
            lblSubCategoryName.Content = CategoryName;
            lblSubCategoryName3.Content = SubcategoryName;

            this.ShopName = shopName;
            this.CategoryName = CategoryName;
            this.SubCategoryName = SubcategoryName;

        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
           
            refreshAsync();
        }
        public async void refreshAsync()
        {
            WpProduct.Children.Clear();
            Button button = new Button
            {
                Width = 150,
                Height = 150,
                Style = (Style)FindResource("SaveBtn"),
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C1D8C3")),
                Content = "Добавить +",
                Cursor = Cursors.Hand,
                FontSize = 40,
                Margin = new Thickness(10)
            };
            WpProduct.Children.Add(button);
            button.Click += btnCreateProduct;
            var dbResult = await _productInterface.GetAllProductsBySubcategoryIdAsync(SubCategoryId);
            foreach (var dbProduct in dbResult)
            {
                ProductUserControl productUserControl = new ProductUserControl();
                productUserControl.setData(dbProduct, this.ShopName, this.CategoryName, this.SubCategoryName);
                productUserControl.RefreshPage=refreshAsync;
                WpProduct.Children.Add(productUserControl);


            }
        }
        private  void btnCreateProduct(object sender, RoutedEventArgs e)
        {
           ProductCreateWindow productCreateWindow = new ProductCreateWindow();
           productCreateWindow.setData(SubCategoryId,this.ShopName,this.CategoryName,this.SubCategoryName);
           productCreateWindow.RefreshPage = refreshAsync;
           productCreateWindow.ShowDialog();
        }
   
   
        private void btnBacktoCategories_Click(object sender, RoutedEventArgs e)
        {
            //SubCategoryPage subCategoryPage = new SubCategoryPage();
            //NavigationService?.Navigate(subCategoryPage);
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}
