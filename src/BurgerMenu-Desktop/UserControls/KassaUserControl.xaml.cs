using BurgerMenu_Desktop.Entities.CasheRegisters;
using BurgerMenu_Desktop.Interfaces.CasheRegisters;
using BurgerMenu_Desktop.Pages.AuthPages;
using BurgerMenu_Desktop.Repositories.CasheRegisters;
using BurgerMenu_Desktop.ViewModels.CasheRegisters;
using BurgerMenu_Desktop.ViewModels.Categories;
using BurgerMenu_Desktop.Windows.MyCasheRegisterWindows;
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

namespace BurgerMenu_Desktop.UserControls
{
    /// <summary>
    /// Interaction logic for KassaUserControl.xaml
    /// </summary>
    public partial class KassaUserControl : UserControl
    {
        public delegate void RefreshMyCasheRegisterPage();
        public RefreshMyCasheRegisterPage? RefreshPage {  get; set; }
        private long KassaId {  get; set; } 
        public KassaViewModel kassaViewModel { get; set; }
        private readonly IKassaRepository _kassaRepository;
        public KassaUserControl()
        {
            InitializeComponent();
            this._kassaRepository = new KassaRepository();
        }
        public void setData(KassaViewModel kassa)
        {
            this.kassaViewModel = new KassaViewModel()
            {
                Id = kassa.Id,
                Name = kassa.Name,
                ShopId = kassa.ShopId,
            };
            //kassaViewModel = kassa;
            lblCasheRegister.Text = kassa.Name;
            this.KassaId = kassa.Id;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы хотите удалить?",
                  "Удалить Кассa",
           MessageBoxButton.YesNo,
                  MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var dll = await _kassaRepository.DeleteAsync(KassaId);
                if (dll > 0)
                {
                    MessageBox.Show("Удален!!");
                    RefreshPage?.Invoke();
                }
                else MessageBox.Show("Не удалено!!");
            }
            else MessageBox.Show("Не удалено!!2");
        }

        private async void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            Kassa kassa = new Kassa()
            {
                Id = kassaViewModel.Id,
                Name = kassaViewModel.Name,
                ShopId = kassaViewModel.ShopId,
            };
            KassaUpdateWindow kassaUpdateWindow = new KassaUpdateWindow();
            kassaUpdateWindow.setData(kassaViewModel);
            kassaUpdateWindow.RefreshPage = refreshPage;
            kassaUpdateWindow.ShowDialog();            
        }
        public void refreshPage()
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
            if (Window.GetWindow(this) is MainWindow mainWindow)
            {
                PaymentWindow paymentWindow = new PaymentWindow();
                paymentWindow.setData(KassaId);
                paymentWindow.OpenWindow = ShowMainWindow;
                HideMainWindow();
                paymentWindow.ShowDialog();


                //mainWindow.PageNavigator.Navigate(subCategoryPage);
                //Button? btnBackToCategory = mainWindow.FindName("btnBacktoCategory") as Button;
                //if (btnBackToCategory !=null) btnBackToCategory.Visibility = Visibility.Visible;
            }
        }
        private void HideMainWindow()
        {
            Window mainWindow = Window.GetWindow(this);

            if (mainWindow != null)
            {
                // Hide the main window
                mainWindow.Visibility = Visibility.Hidden;
            }
        }
        private void ShowMainWindow()
        {
            Window mainWindow = Window.GetWindow(this);

            if (mainWindow != null)
            {
                // Show the main window
                mainWindow.Visibility = Visibility.Visible;
            }
        }

    }
}
