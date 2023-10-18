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
using System.Text.RegularExpressions;
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
        public delegate void RefreshDelegateForMyShopPage();
        public RefreshDelegateForMyShopPage RefreshPage { get; set; }
        public long Id { get; set; }
        private readonly IShopRepository _shopRepository;
        public UpdateShopWindow()
        {
            InitializeComponent();
            this._shopRepository = new ShopRepository();
        }
        private bool ContainsPunctuation(string text)
        {
            // Define the regular expression pattern to match punctuation characters
            string pattern = @"[\p{P}]";
            Regex regex = new Regex(pattern);

            // Check if the text contains any punctuation characters
            return regex.IsMatch(text);
        }
      
        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            int count = 0;
            if (ImgBImage.ImageSource==null)
            {
                MessageBox.Show("Требуется изображение");
            }
            else if (tbShopName.Text.Length == 0)
            {
                MessageBox.Show("требуется название магазина");
            }
            else
            {
                string imagePath = ImgBImage.ImageSource.ToString();
                if (ContainsPunctuation(tbShopName.Text) == false) count++;
                else MessageBox.Show("без знаков препинания");
                if (string.IsNullOrEmpty(imagePath) == false) count++;
                else MessageBox.Show("Проверить изображение");
                if (tbShopName.Text.Length >= 4) count++;
                else MessageBox.Show("Проверьте имя магазина. Должно быть минимум 4 буквы!");
                if (count == 3)
                {
                    Shop shop = new Shop();
                    shop.Name = tbShopName.Text;
                    shop.ImagePath = ImgBImage.ImageSource.ToString();
                    if (MessageBox.Show("Вы хотите обновить?",
                       "Обновить магазин",
                        MessageBoxButton.YesNo,
                       MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        var updateResult = await _shopRepository.UpdateAsync(Id, shop);
                        if (updateResult > 0)
                        {
                            MessageBox.Show("Обновлено!!");
                            RefreshPage?.Invoke();
                            this.Close();

                        }
                        else MessageBox.Show("Название магазина должно быть уникальным!");
                    }
                    else MessageBox.Show("Что-то не так");
                }
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
            // Define the regular expression pattern to allow only alphanumeric characters and spaces
            string pattern = @"^[a-zA-Z0-9\u0020\u0400-\u04FF]+$";
            Regex regex = new Regex(pattern);

            // Check if the entered text does not match the pattern
            if (!regex.IsMatch(e.Text))
            {
                e.Handled = true; // Prevent the input by setting Handled to true
            }
        }

        private void ImgCourseImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png, *.gif, *.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;
                ImgBImage.ImageSource = new BitmapImage(new Uri(selectedFilePath, UriKind.Relative));
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
