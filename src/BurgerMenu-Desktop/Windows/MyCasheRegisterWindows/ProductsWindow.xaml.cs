using BurgerMenu_Desktop.Entities.Categories;
using BurgerMenu_Desktop.Interfaces.Products;
using BurgerMenu_Desktop.Repositories.Products;
using BurgerMenu_Desktop.Security;
using BurgerMenu_Desktop.ViewModels.Products;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BurgerMenu_Desktop.Windows.MyCasheRegisterWindows
{
    /// <summary>
    /// Interaction logic for ProductsWindow.xaml
    /// </summary>
    public partial class ProductsWindow : Window
    {
        private long TabId {  get; set; }
        private readonly IProductInterface _productRepository;
        public delegate void RefreshProPaymentWindow();
        public RefreshProPaymentWindow? RefreshWindow { get; set; }

        public ProductsWindow()
        {
            InitializeComponent();
            this._productRepository = new ProductRepository();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void scrollWindow(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        public async void refreshAsync()
        {
            var dbResult = await _productRepository.GetAllWithCategoryAndSubcategoryIdById();
            DataGrid.ItemsSource = dbResult;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            refreshAsync();
        }

        private void productDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        public void setData(long TabId)
        {
            this.TabId = TabId;
        }

        private void MyDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Handle the double-click event here
            // You can access the selected item or perform any desired action

            if (sender is DataGrid dataGrid)
            {
                if (dataGrid.SelectedItem != null)
                {
                    // Get the selected item from the DataGrid
                    var selectedItem= dataGrid.SelectedItem as ProductWithDetailsViewModel;
                    ProductWithTabDetails productWithTabDetails = new ProductWithTabDetails()
                    {
                        id = selectedItem.id,
                        product_name = selectedItem.product_name,
                        quantity = selectedItem.quantity,
                        BarCode = selectedItem.BarCode,
                        StartingPrice = selectedItem.StartingPrice,
                        SoldPrice = selectedItem.SoldPrice,
                        Category = selectedItem.Category,
                        Subcategory = selectedItem.Subcategory,
                        tab_id = this.TabId
                    };
                    // Perform any desired action with the selected item
                    // For example:
                    //MessageBox.Show($"Double-clicked on row: {selectedItem}");
                    var identity = IdentitySingleton.GetInstance();
                    if (selectedItem != null)
                    {
                        identity.AddToCartList.Add(productWithTabDetails);
                        RefreshWindow?.Invoke();
                        this.Close();
                    }
                    else MessageBox.Show("SomeThing Wrong ! try again ");
                    
                }
            }
        }
    }
}
