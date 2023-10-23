using BurgerMenu_Desktop.Entities.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace BurgerMenu_Desktop.Windows.Products
{
    /// <summary>
    /// Interaction logic for ProductCreateWindow.xaml
    /// </summary>
    public partial class ProductCreateWindow : Window
    {
        public ProductCreateWindow()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }        
        private bool ContainsPunctuation(string text)
        {
            // Define the regular expression pattern to match punctuation characters
            string pattern = @"[\p{P}]";
            Regex regex = new Regex(pattern);

            // Check if the text contains any punctuation characters
            return regex.IsMatch(text);
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
        public bool ContainsNonLatinCharacters(string input)
        {

            string latinAlphaNumericPattern = @"^[a-zA-Z0-9]+$";
            // Check if the input contains any Latin alphabets
            return Regex.IsMatch(input, latinAlphaNumericPattern);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            if (tbProductName.Text.Length == 0)
            {
                MessageBox.Show("требуется название Категория");
            }
            else
            {
                //string imagePath = ImgBImage.ImageSource.ToString();
                if (ContainsPunctuation(tbProductName.Text) == false) count++;
                else MessageBox.Show("без знаков препинания");
                if (tbProductName.Text.Length >= 4) count++;
                else MessageBox.Show("Проверьте имя Категория. Должно быть минимум 4 буквы!");
                if (count == 2)
                {

                 
                    //var dbResult = await _categoryRepository.CreateAsync(category);
                    //if (dbResult > 0)
                    //{
                    //    MessageBox.Show("Создан Категория");
                    //    //RefreshPage?.Invoke();
                    //    this.Close();
                    //}
                    //else MessageBox.Show("Название Категория должно быть уникальным!");

                }
            }
        }
    }
}
