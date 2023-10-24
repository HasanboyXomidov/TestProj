using BurgerMenu_Desktop.Entities.Products;
using BurgerMenu_Desktop.Interfaces.Products;
using BurgerMenu_Desktop.Repositories.Products;
using BurgerMenu_Desktop.ViewModels.Products;
using BurgerMenu_Desktop.Windows.Products;
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
            refreshAsync();
        }
        public async void refreshAsync()
        {
            var dbResult = await _productRepository.GetAllWithCategoryAndSubcategoryIdById();

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

        private void productDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            
        }

        private void btnUpdateInWareHousePage_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItems.Count == 1)
            {
                // Check if a row is selected
                if (DataGrid.SelectedItem != null)
                {
                    // Get the selected row's object
                    var selectedRow = DataGrid.SelectedItem as ProductWithDetailsViewModel;

                    // Create and show the window
                    ProductUpdateWindowForWareHouse window = new ProductUpdateWindowForWareHouse();
                    window.setDataFromWareHouse(selectedRow);
                    window.RefreshPage = refreshAsync;
                    window.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите один продукт для обновления");
            }
        }

        private void btnQuickAdd_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItems.Count == 1)
            {
                // Check if a row is selected
                if (DataGrid.SelectedItem != null)
                {
                    // Get the selected row's object
                    var selectedRow = DataGrid.SelectedItem as ProductWithDetailsViewModel;

                    // Create and show the window
                    ProductUpdateWindowForWareHouse window = new ProductUpdateWindowForWareHouse();
                    window.setDataFromWareHouse(selectedRow);
                    window.RefreshPage = refreshAsync;
                    window.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите один продукт для обновления");
            }
        }
    }
}
