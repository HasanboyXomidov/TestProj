using BurgerMenu_Desktop.Entities.Shops;
using BurgerMenu_Desktop.Interfaces.Shops;
using BurgerMenu_Desktop.Repositories.Shops;
using BurgerMenu_Desktop.ViewModels.Shops;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

namespace BurgerMenu_Desktop.Windows.ShopWindows
{
    /// <summary>
    /// Interaction logic for UpdateShopWindow.xaml
    /// </summary>
    public partial class UpdateShopWindow : Window
    {
        public long Id { get; set; }
        private readonly IShopRepository _shopRepository;
        public Func<Task> Refresh { get; set; }
        public UpdateShopWindow()
        {
            InitializeComponent();
            this._shopRepository = new ShopRepository();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            int count = 0;
            string imagePath = ImgBImage.ImageSource.ToString();
            if (string.IsNullOrEmpty(imagePath) == false) count++;
            else MessageBox.Show("Check Image");
            if (tbShopName.Text.Length >= 4) count++;
            else MessageBox.Show("Check ShopName Must be minimum 4 letters !");
            if( count == 2)
            {
                Shop shop = new Shop();
                shop.Name = tbShopName.Text;
                shop.ImagePath = ImgBImage.ImageSource.ToString();
                if (MessageBox.Show("Do you want to Update ?",
                   "Update Shop",
                    MessageBoxButton.YesNo,
                   MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var updateResult = await _shopRepository.UpdateAsync(Id, shop);
                    if (updateResult > 0) 
                    {
                        MessageBox.Show("Updated!!");
                        await Refresh();
                        this.Close();

                    }
                    else MessageBox.Show("Not Updated!!");
                }
                else MessageBox.Show("Something wrong ");
            }

        }
        public void setData(Shop shop)
        {
            this.Id = shop.Id;
            ImgBImage.ImageSource = new BitmapImage(new System.Uri(shop.ImagePath, UriKind.Relative));
            tbShopName.Text = shop.Name;
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void ImgCourseImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string ImagePath = openFileDialog.FileName;
                ImgBImage.ImageSource = new BitmapImage(new Uri(ImagePath, UriKind.Relative));
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
