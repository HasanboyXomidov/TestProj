using BurgerMenu_Desktop.Entities.Products;
using BurgerMenu_Desktop.Interfaces.Products;
using BurgerMenu_Desktop.Repositories.Products;
using BurgerMenu_Desktop.ViewModels.Products;
using Google.Protobuf.WellKnownTypes;
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
    /// Interaction logic for ProductUpdateWindowForWareHouse.xaml
    /// </summary>
    public partial class ProductUpdateWindowForWareHouse : Window
    {
        public delegate void RefreshDelegateForMyShopPage();
        public RefreshDelegateForMyShopPage? RefreshPage { get; set; }
        private ProductsViewModel viewModel { get; set; }
        private long productId { get; set; }
        private readonly IProductInterface _product;
        private int ExtraQuantity { get; set; } = 0;
        public ProductUpdateWindowForWareHouse()
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
        public void setDataFromWareHouse(ProductWithDetailsViewModel product)
        {
            tbProductName.Text = product.product_name;
            tbQuantity.Text = (product.quantity).ToString();

            string FormattedStartingPrice = FormatPrice((product.StartingPrice).ToString());
            tbStartingPrice.Text = FormattedStartingPrice;
            string FormattedSoldPrice = FormatPrice((product.SoldPrice).ToString());
            tbSoldPrice.Text = FormattedSoldPrice;

            this.productId = product.id;

            lblSubCategoryName.Content = product.Category;
            lblSubCategoryName3.Content = product.Subcategory;

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
            if (tbProductName.Text.Length == 0 && tbQuantity.Text.Length == 0 && tbStartingPrice.Text.Length == 0  && tbSoldPrice.Text.Length == 0)
            ///*&& tbBarCode.Text.Length == 0*/)
            {
                MessageBox.Show("Пожалуйста, заполните все поля");
            }
            else
            {
                //string imagePath = ImgBImage.ImageSource.ToString();
                if (ContainsPunctuation(tbProductName.Text) == false) count++;
                else MessageBox.Show("без знаков препинания");
                if (tbProductName.Text.Length >= 4 && tbProductName.Text.Length <= 50) count++;
                else MessageBox.Show("Проверьте имя Продукт. Должно быть минимум 4 символов и не более 30 символов ! ");
                string ReFormattedStringForQuantity = ReformatNumericString(tbQuantity.Text);
                if (IsDigitOnly(ReFormattedStringForQuantity) == true)
                {
                    if (ReFormattedStringForQuantity.Length > 0 || ReFormattedStringForQuantity.Length < 13) { count++; }
                    else MessageBox.Show("Проверьте имя Продукт. Должно быть минимум 4 символов и не более 30 символов ! ");
                }
                else MessageBox.Show("Только цифра для количества");             
                string ReFormattedStartPrice = ReformatNumericString(tbStartingPrice.Text);
                if (IsDigitOnly(ReFormattedStartPrice) == true)
                {
                    if (ReFormattedStartPrice.Length > 13) MessageBox.Show("Очень большая сумма");
                    else count++;
                }
                else MessageBox.Show("Только цифра для Начальная цена");
                string ReFormattedSoldString = ReformatNumericString(tbSoldPrice.Text);
                if (IsDigitOnly(ReFormattedSoldString) == true)
                {
                    if (ReFormattedSoldString.Length > 13) MessageBox.Show("Очень большая сумма");
                    else count++;
                }
                else MessageBox.Show("Только цифра для Цена продажи");
                //if (tbAddQuantity.Text.Length>0)
                //{
                //    string ReFormattedStringForAddingQuantity = ReformatNumericString(tbAddQuantity.Text);
                //    if (IsDigitOnly(ReFormattedStringForAddingQuantity) == true)
                //    {
                //        if (ReFormattedSoldString.Length > 9)
                //        {
                //            MessageBox.Show("Невозможно добавить так много количества товаров");
                //            count++;
                //        }
                //        else
                //        {
                //            float quantity = GetFloatValueFromFormattedText(tbAddQuantity.Text);
                //            ExtraQuantity += Convert.ToInt32(quantity);
                //            count++;
                //        }
                //    }
                //    //else MessageBox.Show("Только цифра для количества");

                //}
                //else count++;
                

                if (count == 5 )
                {
                    Product product = new Product();
                    product.ProductName = tbProductName.Text;
                    float quantity = GetFloatValueFromFormattedText(tbQuantity.Text);
                    product.Quantity = Convert.ToInt32(quantity) + ExtraQuantity;
                    float startingPrice = GetFloatValueFromFormattedText(tbStartingPrice.Text);
                    product.StartingPrice = startingPrice;
                    float soldPrice = GetFloatValueFromFormattedText(tbSoldPrice.Text);
                    product.SoldPrice = soldPrice;
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

        private void scrollWindow(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
