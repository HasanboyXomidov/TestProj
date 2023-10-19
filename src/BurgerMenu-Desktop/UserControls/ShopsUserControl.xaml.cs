using BurgerMenu_Desktop.Entities.Shops;
using BurgerMenu_Desktop.Interfaces;
using BurgerMenu_Desktop.Interfaces.Shops;
using BurgerMenu_Desktop.Pages;
using BurgerMenu_Desktop.Pages.AuthPages;
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
            lblShoName.Text = shopsViewModel.Name;
            //bgImgShops.ImageSource = new BitmapImage(new System.Uri(shopsViewModel.ImagePath,UriKind.Relative));
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
            //shop.ImagePath = shopsViewModel.ImagePath;
            UpdateShopWindow updateShopWindow = new UpdateShopWindow();
            updateShopWindow.setData(shop);
            updateShopWindow.RefreshPage = RefreshPageHandler;
            updateShopWindow.ShowDialog();
            
        }
        private void RefreshPageHandler()
        {
            RefreshPage?.Invoke();
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Border border)
            {
                border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6A9C89"));
                Cursor = Cursors.Hand;
            }
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Border border)
            {
                border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Transparent"));
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Get the parent Frame control
            Frame frame = FindParent<Frame>(this);

            // Navigate to the new page
            CategoriesPage categoriesPage = new CategoriesPage();
            categoriesPage.setData(shopsViewModel.Id);
            frame.Navigate(categoriesPage);
        }
        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);

            if (parent == null)
                return null;

            T typedParent = parent as T;
            return typedParent ?? FindParent<T>(parent);
        }
    }
}
