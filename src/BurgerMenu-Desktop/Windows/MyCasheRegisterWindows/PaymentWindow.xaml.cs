﻿using BurgerMenu_Desktop.Entities.Shops;
using BurgerMenu_Desktop.Interfaces.Tabs;
using BurgerMenu_Desktop.Repositories.Tabs;
using BurgerMenu_Desktop.Security;
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
        public delegate void OpenMainWindowDelegate();
        public OpenMainWindowDelegate? OpenWindow { get; set; }
        private readonly ITabsrepository _tabsRepository;
        private long KassaId { get; set; }
        private long TabId { get; set; }    
        public PaymentWindow()
        {
            InitializeComponent();
            this._tabsRepository = new TabRepository();
        }
        private void YourUserControl_UserControlClicked(long id)
        {
            this.TabId=id;
            refreshSecondWrapPanel();

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
            //WpMainPayment.Children.Clear();
            Button tabButton = new Button
            {
                Width = 60,
                Height = 35,
                Style = (Style)FindResource("SaveBtn"),
                //BorderThickness = new Thickness(0),
                //BorderBrush = new SolidColorBrush(Colors.Transparent),
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CD5C09")),
                Foreground = new SolidColorBrush(Colors.White),
                Content = "+",
                Cursor = Cursors.Hand,
                FontSize = 40,
                Margin = new Thickness(5)
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
                    tabUserControl.UserControlClicked += YourUserControl_UserControlClicked;
                    WpPanel.Children.Add(tabUserControl);
                }
            }            
        }
        public void refreshSecondWrapPanel()
        {
            WpMainPayment.Children.Clear();
            Button button = new Button
            {
                Width = 150,
                Height = 90,
                Style = (Style)FindResource("SaveBtn"),
                //BorderThickness=new Thickness(0),
                //BorderBrush = new SolidColorBrush(Colors.Transparent),
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C1D8C3")),
                Content = "Добавить +",
                Cursor = Cursors.Hand,
                FontSize = 40,
                Margin = new Thickness(5)
            };
            WpMainPayment.Children.Add(button);
            button.Click += btnCreateCashe;
            var identity = IdentitySingleton.GetInstance();
            var result = identity.AddToCartList;
            if (result.Count > 0)
            {
                foreach (var product in result)
                {
                    if (product.tab_id == TabId)
                    {
                        SellProductUserControl sellProductUserControl = new SellProductUserControl();
                        sellProductUserControl.setData(product);
                        WpMainPayment.Children.Add(sellProductUserControl);
                    }

                }
            }
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
            ProductsWindow productsWindow = new ProductsWindow();
            productsWindow.setData(TabId);
            productsWindow.RefreshWindow = refreshSecondWrapPanel;
            productsWindow.ShowDialog();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            OpenWindow?.Invoke();
        }

        private void scrollWindow(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
