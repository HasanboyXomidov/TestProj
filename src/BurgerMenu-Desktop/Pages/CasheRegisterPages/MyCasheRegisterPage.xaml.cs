using BurgerMenu_Desktop.Interfaces.CasheRegisters;
using BurgerMenu_Desktop.Interfaces.Shops;
using BurgerMenu_Desktop.Repositories.CasheRegisters;
using BurgerMenu_Desktop.Repositories.Shops;
using BurgerMenu_Desktop.UserControls;
using BurgerMenu_Desktop.Windows.MyCasheRegisterWindows;
using BurgerMenu_Desktop.Windows.ShopWindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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

namespace BurgerMenu_Desktop.Pages.CasheRegisterPages
{
    /// <summary>
    /// Interaction logic for MyCasheRegisterPage.xaml
    /// </summary>
    public partial class MyCasheRegisterPage : Page
    {
        private long ShopId {  get; set; }
        private readonly IKassaRepository _kassaRepository;
        private readonly IShopRepository _shopRepository;
        public MyCasheRegisterPage()
        {
            InitializeComponent();
            this._kassaRepository = new KassaRepository();
            this._shopRepository = new ShopRepository();
        }
        public void setData(long shopId)
        {
            this.ShopId = shopId;   
        }
        public async void refreshAsync()
        {
            WpCasheRegister.Children.Clear();
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
            WpCasheRegister.Children.Add(button);
            button.Click += btnCreateCasheRegister;

            var dbResult2 = await _shopRepository.GetShopById(this.ShopId);
            lblShopName.Content = dbResult2.Name;
            var dbResult = await _kassaRepository.GetAllByIdAsync(this.ShopId);
            foreach (var item in dbResult)
            {
                KassaUserControl kassa = new KassaUserControl();
                kassa.setData(item);
                kassa.RefreshPage = refreshAsync;
                WpCasheRegister.Children.Add(kassa);

            }

        }
        private void btnCreateCasheRegister(object sender, RoutedEventArgs e)
        {
            KassaCreateWindow kassa = new KassaCreateWindow();
            kassa.setData(this.ShopId);
            kassa.RefreshPage =refreshAsync;
            kassa.ShowDialog(); 
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            refreshAsync();
        }

        private void btnBacktoMyShops_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
