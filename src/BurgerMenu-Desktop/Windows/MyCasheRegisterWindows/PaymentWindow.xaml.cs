using BurgerMenu_Desktop.Entities.Shops;
using BurgerMenu_Desktop.Interfaces.Tabs;
using BurgerMenu_Desktop.Repositories.Tabs;
using BurgerMenu_Desktop.UserControls;
using BurgerMenu_Desktop.Windows.CategoryWindows;
using BurgerMenu_Desktop.Windows.TabWindows;
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
using System.Windows.Shapes;

namespace BurgerMenu_Desktop.Windows.MyCasheRegisterWindows
{
    /// <summary>
    /// Interaction logic for PaymentWindow.xaml
    /// </summary>
    public partial class PaymentWindow : Window
    {
        private readonly ITabsrepository _tabsRepository;
        private long KassaId { get; set; }
        public PaymentWindow()
        {
            InitializeComponent();
            this._tabsRepository = new TabRepository();
        }
        public void setData(long KassaId)
        {
            this.KassaId = KassaId;
        }
        private void brPayment_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            refreshAsync();
        }
        public async void refreshAsync()
        {
            WpPanel.Children.Clear();
            WpMainPayment.Children.Clear();

            Button tabButton = new Button
            {
                Width = 60,
                Height = 35,
                Style = (Style)FindResource("SaveBtn"),
                BorderThickness = new Thickness(0),
                BorderBrush = new SolidColorBrush(Colors.Transparent),
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CD5C09")),
                Foreground = new SolidColorBrush(Colors.White),
                Content = "+",
                Cursor = Cursors.Hand,
                FontSize = 40,
                Margin = new Thickness(10)
            };
            WpPanel.Children.Add(tabButton);
            tabButton.Click +=btnCreateTab;
            var dbResult = await _tabsRepository.GetTabsByKassaIdAsync(KassaId);
            if (dbResult.Count > 0)
            {
                foreach (var item in dbResult)
                {
                    TabUserControl tabUserControl = new TabUserControl();
                    tabUserControl.setData(item);
                    WpPanel.Children.Add(tabUserControl);
                }

            }

            Button button = new Button
            {
                Width = 120,
                Height = 60,
                Style = (Style)FindResource("SaveBtn"),
                BorderThickness=new Thickness(0),
                BorderBrush = new SolidColorBrush(Colors.Transparent),
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C1D8C3")),
                Content = "Добавить +",
                Cursor = Cursors.Hand,
                FontSize = 40,
                Margin = new Thickness(10)
            };
            WpMainPayment.Children.Add(button);
            button.Click += btnCreateCashe ;

        }
        private void btnCreateTab(object sender, RoutedEventArgs e)
        {
            TabCreateWindow tabCreateWindow = new TabCreateWindow();
            tabCreateWindow.setData(KassaId);
            tabCreateWindow.RefreshPage = refreshAsync;
            tabCreateWindow.ShowDialog();
        }
        private void btnCreateCashe(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void scrollWindow(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
