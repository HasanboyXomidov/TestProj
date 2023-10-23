using BurgerMenu_Desktop.Entities.Products;
using BurgerMenu_Desktop.Interfaces;
using BurgerMenu_Desktop.Interfaces.Products;
using BurgerMenu_Desktop.Repositories.Products;
using BurgerMenu_Desktop.ViewModels.Products;
using BurgerMenu_Desktop.Windows.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace BurgerMenu_Desktop.UserControls
{
    /// <summary>
    /// Interaction logic for ProductUserControl.xaml
    /// </summary>
    public partial class ProductUserControl : UserControl
    {
        public delegate void RefreshDelegateForMyShopPage();
        public RefreshDelegateForMyShopPage? RefreshPage { get; set; }
        private string ShopName { get; set; }
        private string CategoryName { get; set; }
        private string SubCategoryName { get; set; }
        public ProductsViewModel productsViewModelp {  get; set; }
        private IProductInterface _productInterface;
        public ProductUserControl()
        {
            InitializeComponent();
            this._productInterface = new ProductRepository();
        }
        public void setData(ProductsViewModel productsViewModel,string shopName, string categoryName, string subCategoryname)
        {

            lblProductName.Text = productsViewModel.ProductName;
            lblQuantity.Text = (productsViewModel.Quantity).ToString();

            string FormattedStartingPrice = FormatPrice((productsViewModel.StartingPrice).ToString());
            lblSstartPrice.Text = FormattedStartingPrice;
            string FormattedSoldPrice = FormatPrice((productsViewModel.SoldPrice).ToString());
            lblSoldPrice.Text = FormattedSoldPrice;

            lblBarCode.Text = (productsViewModel.BarCode).ToString();
            productsViewModelp = productsViewModel;

            this.ShopName = shopName;
            this.CategoryName = categoryName;
            this.SubCategoryName = subCategoryname;
           
        }
        public string FormatPrice(string Price)
        {
            float number = float.Parse(Price);
            string formattedNumber = number.ToString("#,##0").Replace(",", " ");
            return formattedNumber;
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы хотите удалить?",
                 "Удалить Продукт",
          MessageBoxButton.YesNo,
                 MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var dll = await _productInterface.DeleteAsync(productsViewModelp.Id);
                if (dll > 0)
                {
                    MessageBox.Show("Удален!!");
                    RefreshPage?.Invoke();
                }
                else MessageBox.Show("Не удалено!!");
            }
            else MessageBox.Show("Не удалено!!2");
        }

        private void updateBtn(object sender, RoutedEventArgs e)
        {
            ProductUpdateWindow productUpdateWindow = new ProductUpdateWindow();
            productUpdateWindow.setData(productsViewModelp, this.ShopName, this.CategoryName, this.SubCategoryName);
            productUpdateWindow.RefreshPage=refreshPage;
            productUpdateWindow.ShowDialog();
        }
        public void refreshPage()
        {
            RefreshPage?.Invoke();
        }
    }
}
