﻿using BurgerMenu_Desktop.Interfaces.Shops;
using BurgerMenu_Desktop.Repositories.Shops;
using BurgerMenu_Desktop.UserControls;
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

namespace BurgerMenu_Desktop.Pages
{
    /// <summary>
    /// Interaction logic for AllShopsPage.xaml
    /// </summary>
    public partial class AllShopsPage : Page
    {
        private readonly IShopRepository _shopRepository;

        public AllShopsPage()
        {
            InitializeComponent();
            this._shopRepository = new ShopRepository();

        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await refreshAsync();
        }

        public async Task refreshAsync()
        {
            WpShops.Children.Clear();
            Button button = new Button
            {
                Width = 120,
                Height = 90,
                Style = (Style)FindResource("SaveBtn"),
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C1D8C3")),
                Content = "+",
                Cursor = Cursors.Hand,
                FontSize = 40,
                Margin = new Thickness(10)
            };
            WpShops.Children.Add(button);
            button.Click += btnCreateShop;
            var dbResult = await _shopRepository.GetAllAsync();
            if (dbResult.Count > 0)
            {
                foreach (var shop in dbResult)
                {

                    ShopsUserControl shopsUserControl = new ShopsUserControl();
                    shopsUserControl.setData(shop);
                    WpShops.Children.Add(shopsUserControl);

                }
            }
            //else MessageBox.Show("Shops Not Found");
        }

        private void btnCreateShop(object sender, RoutedEventArgs e)
        {

        }
    }
}
