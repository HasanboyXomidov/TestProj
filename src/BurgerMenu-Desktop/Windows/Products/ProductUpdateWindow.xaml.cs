using BurgerMenu_Desktop.Entities.Products;
using BurgerMenu_Desktop.Interfaces.Products;
using BurgerMenu_Desktop.Repositories.Products;
using BurgerMenu_Desktop.ViewModels.Products;
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
    /// Interaction logic for ProductUpdateWindow.xaml
    /// </summary>
    public partial class ProductUpdateWindow : Window
    {
        public delegate void RefreshDelegateForMyShopPage();
        public RefreshDelegateForMyShopPage? RefreshPage { get; set; }
        private ProductsViewModel viewModel { get; set; }
        private long productId { get; set; }
        private readonly IProductInterface _product;

        public ProductUpdateWindow()
        {
            InitializeComponent();
            this._product = new ProductRepository();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
        public string FormatPrice(string Price)
        {
            float number = float.Parse(Price);
            string formattedNumber = number.ToString("#,##0").Replace(",", " ");
            return formattedNumber;
        }
        public void setData(ProductsViewModel product,string shopName, string categoryName, string subCategoryname)
        {
            tbProductName.Text = product.ProductName;
            tbQuantity.Text = (product.Quantity).ToString();

            string FormattedStartingPrice = FormatPrice((product.StartingPrice).ToString());
            tbStartingPrice.Text = FormattedStartingPrice;
            string FormattedSoldPrice = FormatPrice((product.SoldPrice).ToString());
            tbSoldPrice.Text = FormattedSoldPrice;

   
            tbBarCode.Text = (product.BarCode).ToString();

            this.productId = product.Id;

            lblShopName1.Content = shopName;
            lblSubCategoryName.Content = categoryName;
            lblSubCategoryName3.Content = subCategoryname;

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true; // Block the space key input
            }
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
        public bool IsDigitOnly(string input)
        {
            bool isDigitsOnly = Regex.IsMatch(input, @"^\d+$");
            return isDigitsOnly;
        }
        private void TextBox_PreviewTextInputOnlyDigit(object sender, TextCompositionEventArgs e)
        {
            //if (!char.IsDigit(e.Text, e.Text.Length - 1))
            //{
            //    e.Handled = true; // Prevents the non-digit character from being entered
            //}
            // Allow only digits and a single dot (.)
            if (!char.IsDigit(e.Text, e.Text.Length - 1) && e.Text != ".")
            {
                e.Handled = true;
            }
        }
        private string ReformatNumericString(string numericString)
        {
            string stringWithoutSpaces = Regex.Replace(numericString, @"\s+", "");
            return stringWithoutSpaces;
        }
        private float GetFloatValueFromFormattedText(string formattedText)
        {
            // Remove dots and other non-numeric characters
            string numericText = new string(formattedText.Where(char.IsDigit).ToArray());

            // Parse as float
            if (float.TryParse(numericText, out float result))
            {
                return result;
            }

            // Return default value if parsing fails
            return 0.0f;
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            if (tbProductName.Text.Length == 0 && tbQuantity.Text.Length == 0 && tbStartingPrice.Text.Length == 0  && tbSoldPrice.Text.Length == 0
                && tbBarCode.Text.Length == 0)
            {
                MessageBox.Show("Пожалуйста, заполните все поля");
            }
            else
            {
                //string imagePath = ImgBImage.ImageSource.ToString();
                if (ContainsPunctuation(tbProductName.Text) == false) count++;
                else MessageBox.Show("без знаков препинания");
                if (tbProductName.Text.Length >= 4) count++;
                else MessageBox.Show("Проверьте имя Продукт. Должно быть минимум 4 буквы!");
                if (IsDigitOnly(tbQuantity.Text) == true) count++;
                else MessageBox.Show("Только цифра для количества");
                string ReFormattedStartPrice = ReformatNumericString(tbStartingPrice.Text);
                if (IsDigitOnly(ReFormattedStartPrice) == true) count++;
                else MessageBox.Show("Только цифра для Начальная цена");
                string ReFormattedSoldString = ReformatNumericString(tbSoldPrice.Text);
                if (IsDigitOnly(ReFormattedSoldString) == true) count++;
                else MessageBox.Show("Только цифра для Цена продажи");
                if (IsDigitOnly(tbBarCode.Text) == true) count++;
                else MessageBox.Show("Только цифра для Штрих-код");
                if (count == 6)
                {
                    Product product = new Product();
                    product.ProductName = tbProductName.Text;
                    product.Quantity = int.Parse(tbQuantity.Text);
                    float startingPrice = GetFloatValueFromFormattedText(tbStartingPrice.Text);
                    product.StartingPrice = startingPrice;
                    float soldPrice = GetFloatValueFromFormattedText(tbSoldPrice.Text);
                    product.SoldPrice = soldPrice;
                    product.BarCode = long.Parse(tbBarCode.Text);
                    var dbResult = await _product.UpdateAsync(productId, product);
                    if (dbResult > 0)
                    {
                        MessageBox.Show("Обновлено Продукт");
                        RefreshPage?.Invoke();
                        this.Close();
                    }
                    else MessageBox.Show("Штрих-код должно быть уникальным!");
                }
            }
        }

        private void tbStartingPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // Remove leading zeros
                textBox.Text = textBox.Text.TrimStart('0');

                // Remove existing dots
                textBox.Text = textBox.Text.Replace(".", "");

                // Format with dots
                if (!string.IsNullOrEmpty(textBox.Text) && double.TryParse(textBox.Text, out double number))
                {
                    textBox.TextChanged -= tbStartingPrice_TextChanged; // Remove event handler temporarily

                    textBox.Text = number.ToString("#,##0"); // Apply formatting with dots

                    textBox.CaretIndex = textBox.Text.Length; // Set caret position to end

                    textBox.TextChanged += tbStartingPrice_TextChanged; // Reattach event handler
                }
            }
        }
    }
}
