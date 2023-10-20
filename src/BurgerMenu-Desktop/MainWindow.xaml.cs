using BurgerMenu_Desktop.Pages;
using BurgerMenu_Desktop.Pages.AuthPages;
using BurgerMenu_Desktop.UserControls;
using BurgerMenu_Desktop.ViewModels.Shops;
using BurgerMenu_Desktop.Windows;
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

namespace BurgerMenu_Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

        }
        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
 
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void rbMyShopClick(object sender, RoutedEventArgs e)
        {
            RefreshPage();
        }
                
        public void RefreshPage()
        {
            MyShopPage myShopPage = new MyShopPage();
            PageNavigator.Content = myShopPage;            
        }

        private void scrollWindow(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void rbAllShops(object sender, RoutedEventArgs e)
        {
            MyShopPage myShopPage = new MyShopPage();
            PageNavigator.Content = myShopPage;
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnHome_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void btnHome_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void btnBackto_Click(object sender, RoutedEventArgs e)
        {
            //if (PageNavigator.CanGoBack)
            //{
            //    PageNavigator.GoBack();
            //    //btnBackto.Visibility = Visibility.Collapsed;
            //    btnBackto.Visibility = Visibility.Collapsed;
            //}
            if (PageNavigator.Content is MyShopPage)
            {
                btnBackto.Visibility = Visibility.Collapsed;
            }
            else if (PageNavigator.Content is CategoriesPage)
            {
                CategoriesPage categoriesPage = new CategoriesPage();
                btnBackto.Visibility = Visibility.Visible;
                PageNavigator.Content = categoriesPage;
            }
            else if (PageNavigator.Content is SubCategoryPage)
            {
                btnBackto.Visibility = Visibility.Visible;
                SubCategoryPage subCategoryPage = new SubCategoryPage();
                PageNavigator.Content = subCategoryPage;
            }

        }
        public void NavigatePageToMyShops()
        {
            MyShopPage myShopPage = new MyShopPage();
            PageNavigator.Content = myShopPage;
            btnBackto.Visibility = Visibility.Collapsed;
            brProducts.Visibility = Visibility.Collapsed;
            rbMenuButtonMyShops.Visibility = Visibility.Visible;
        }
        public void NavigatePageToCategories()
        {
            CategoriesPage categoriesPage= new CategoriesPage();
            PageNavigator.Content = categoriesPage;
            //btnBackto.Visibility = Visibility.Collapsed;
            //brProducts.Visibility = Visibility.Collapsed;
            //rbMenuButtonMyShops.Visibility = Visibility.Visible;
        }
    }
}
