using BurgerMenu_Desktop.Interfaces.Shops;
using BurgerMenu_Desktop.Repositories.Shops;
using BurgerMenu_Desktop.UserControls;
using BurgerMenu_Desktop.ViewModels.Shops;
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
using static BurgerMenu_Desktop.UserControls.ShopsUserControl;

namespace BurgerMenu_Desktop.Pages
{
    /// <summary>
    /// Interaction logic for MyShopPage.xaml
    /// </summary>
    public partial class MyShopPage : Page
    {
        private readonly IShopRepository _shopRepository;
        public MyShopPage()
        {
            InitializeComponent();
            this._shopRepository = new ShopRepository();
        }

        private  async void Page_Loaded(object sender, RoutedEventArgs e)
        {                        
            await refreshAsync();           
        }
                
        public async Task refreshAsync()
        {
            WpShops.Children.Clear();

            Button button = new Button
            {
                Width = 180,
                Height = 150,
                Style = (Style)FindResource("SaveBtn"),
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C1D8C3")),
                Content = "Add + ",
                Cursor = Cursors.Hand,
                FontSize = 40,
                Margin = new Thickness(10)
            };
            WpShops.Children.Add(button);
            button.Click += btnCreateShop;
            int userId = Convert.ToInt32(Properties.Settings.Default.UserId);
            var dbResult = await _shopRepository.GetAllAsync();
            if (dbResult.Count > 0)
            {
                foreach (var shop in dbResult)
                {

                    ShopsUserControl shopsUserControl = new  ShopsUserControl();
                    shopsUserControl.setData(shop);
                    shopsUserControl.RefreshPage = RefreshPageHandler;
                    WpShops.Children.Add(shopsUserControl);

                }
            }
            //else MessageBox.Show("Shops Not Found");

        }
        private async void RefreshPageHandler()
        {
            await refreshAsync();
        }

        private void btnCreateShop(object sender, RoutedEventArgs e)
        {                        
                CreateShopWindow createShopWindow = new CreateShopWindow();
                createShopWindow.Refresh = refreshAsync;            
                createShopWindow.ShowDialog();            
        }
    }
}
