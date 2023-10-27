using BurgerMenu_Desktop.Interfaces.CasheRegisters;
using BurgerMenu_Desktop.Pages;
using BurgerMenu_Desktop.Pages.AuthPages;
using BurgerMenu_Desktop.Pages.CasheRegisterPages;
using BurgerMenu_Desktop.Pages.WareHousePages;
using BurgerMenu_Desktop.Repositories.CasheRegisters;
using BurgerMenu_Desktop.UserControls;
using BurgerMenu_Desktop.ViewModels.Shops;
using BurgerMenu_Desktop.Windows;
using BurgerMenu_Desktop.Windows.ShopWindows;
using Google.Protobuf.WellKnownTypes;
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
        //private readonly IKassaRepository _kassaRepository;
        public event EventHandler<long> ButtonClicked;
        private long ShopId { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            ButtonClicked += MainWindow_ButtonClicked;

            //this._kassaRepository = new KassaRepository();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //GridNav.Width = 210;
            Tg_Btn.IsChecked = true;
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
            //btnBacktoCategory.Visibility = Visibility.Visible;
            btnBacktoCategory.Visibility = Visibility.Collapsed;
           
           
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
            //MyShopPage myShopPage = new MyShopPage();
            //PageNavigator.Content = myShopPage;

            //btnBackto.Visibility = Visibility.Collapsed;
            //brProducts.Visibility = Visibility.Visible;           
            //rbMenuButtonMyWareHouse.Visibility = Visibility.Collapsed;
            ////btnBacktoHome.Visibility = Visibility.Visible;
            //rbMenuButtonMyCashRegister.Visibility = Visibility.Collapsed;
            //rbMenuButtonMyShops.IsChecked = true;
        }

        private void rbMyWareHouseClick(object sender, RoutedEventArgs e)
        {
            MyWareHousePage myWareHousePage = new MyWareHousePage();
            PageNavigator.Content = myWareHousePage;
       
            //btnBacktoCategory.Visibility = Visibility.Visible;
       
        }

        private void btnBacktoHome_Click(object sender, RoutedEventArgs e)
        {
            rbMenuButtonMyShops.Visibility = Visibility.Visible;
            brProducts.Visibility = Visibility.Collapsed;
            rbMenuButtonMyWareHouse.Visibility = Visibility.Collapsed;
            btnBackto.Visibility = Visibility.Collapsed;
            //btnBacktoHome.Visibility = Visibility.Collapsed;
            PageNavigator.Content = null;
            rbMenuButtonMyCashRegister.Visibility = Visibility.Collapsed;
            rbMenuButtonMyShops.IsChecked = false;
        }

        private void rbMenuButtonMyCashRegister_Click(object sender, RoutedEventArgs e)
        {
            MyCasheRegisterPage myCasheRegisterPage = new MyCasheRegisterPage();
            PageNavigator.Content = myCasheRegisterPage;
            btnBacktoCategory.Visibility = Visibility.Visible;

            ButtonClicked?.Invoke(this, CategoriesPage.ShopId2);

        }
        private void MainWindow_ButtonClicked(object sender, long ShopId)
        {
            // Handle the event and retrieve the ID
            this.ShopId = ShopId;
            MyCasheRegisterPage myCasheRegisterPage = new MyCasheRegisterPage();
            myCasheRegisterPage.setData(ShopId);
            PageNavigator.Content = myCasheRegisterPage;
        }

        private void btnBacktoCategory_Click(object sender, RoutedEventArgs e)
        {
            MyShopPage myShopPage = new MyShopPage();
            PageNavigator.Content=myShopPage;
            btnBacktoCategory.Visibility = Visibility.Collapsed;
            rbMenuButtonMyShops.IsChecked = true;
            //btnBacktoCategory.Visibility = Visibility.Collapsed;
            //btnBacktoHome.Visibility=Visibility.Visible;

            rbMenuButtonMyWareHouse.Visibility=Visibility.Collapsed;
            rbMenuButtonMyCashRegister.Visibility=Visibility.Collapsed;
            brProducts.Visibility=Visibility.Collapsed;
            rbMenuButtonMyShops.Visibility=Visibility.Visible;
        }

        private void Tg_Btn_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
