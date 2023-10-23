using BurgerMenu_Desktop.Entities.Products;
using BurgerMenu_Desktop.Interfaces.Products;
using BurgerMenu_Desktop.Repositories.Products;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace BurgerMenu_Desktop.Pages.WareHousePages
{
    /// <summary>
    /// Interaction logic for MyWareHousePage.xaml
    /// </summary>
    public partial class MyWareHousePage : Page
    {
        private readonly IProductInterface _productRepository; 
        public MyWareHousePage()
        {
            InitializeComponent();
            this._productRepository = new ProductRepository();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var dbResult = await _productRepository.GetAllAsync();
            DataGrid.ItemsSource = dbResult;
        }
        public void FillingDataGrid()
        {
            Product product = new Product();
            DataTable dataTable = new DataTable();
            DataColumn name = new DataColumn("Product Name ", typeof(string));
            dataTable.Columns.Add(product.ProductName);
            //dataTable.Columns.Add(product.Quantity);
        }
    }
}
