using BurgerMenu_Desktop.Entities.Shops;
using BurgerMenu_Desktop.Interfaces;
using BurgerMenu_Desktop.Interfaces.Shops;
using BurgerMenu_Desktop.Repositories.Shops;
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

namespace BurgerMenu_Desktop.UserControls
{
    /// <summary>
    /// Interaction logic for ShopsUserControl.xaml
    /// </summary>
    public partial class ShopsUserControl : UserControl
    {

        public delegate void RefreshDelegate();
        public RefreshDelegate RefreshPage { get; set; }
        ShopsViewModel shopsViewModel { get; set; }
        private readonly IShopRepository _shopRepository;
        public ShopsUserControl()
        {
            InitializeComponent();
            this._shopRepository = new ShopRepository();
        }
        public void setData(ShopsViewModel shopsViewModel)
        {
            this.shopsViewModel = shopsViewModel;
            lblShoName.Content = shopsViewModel.Name;
            bgImgShops.ImageSource = new BitmapImage(new System.Uri(shopsViewModel.ImagePath,UriKind.Relative));
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы хотите удалить?",
                   "Удалить магазин",
            MessageBoxButton.YesNo,
                   MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var dll = await _shopRepository.DeleteAsync(shopsViewModel.Id);            
                if (dll > 0) 
                {
                    MessageBox.Show("Удален!!");
                    RefreshPage?.Invoke();
                }
                else MessageBox.Show("Не удалено!!");
            }
            else MessageBox.Show("Что-то не так");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private  void updateBtn(object sender, RoutedEventArgs e)
        {
            Shop shop = new Shop();
            shop.Id = shopsViewModel.Id;
            shop.Name = shopsViewModel.Name;
            shop.ImagePath = shopsViewModel.ImagePath;

            UpdateShopWindow updateShopWindow = new UpdateShopWindow();
            updateShopWindow.setData(shop);
            updateShopWindow.RefreshPage = RefreshPageHandler;
            updateShopWindow.ShowDialog();
            
        }
        private void RefreshPageHandler()
        {
            RefreshPage?.Invoke();
        }
    }
}
