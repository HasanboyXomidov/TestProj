using BurgerMenu_Desktop.Entities.Products;
using BurgerMenu_Desktop.Interfaces.Products;
using BurgerMenu_Desktop.Interfaces.Shops;
using BurgerMenu_Desktop.Repositories.Products;
using BurgerMenu_Desktop.Repositories.Shops;
using BurgerMenu_Desktop.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for ProductQuantityQuickAddWindow.xaml
    /// </summary>
    public partial class ProductQuantityQuickAddWindow : Window
    {
        public delegate void RefreshDelegateForWareHouse();
        public RefreshDelegateForWareHouse? RefreshPage {  get; set; }
        private long ProductId { get; set; }
        private int NewQuantity { get; set; } = 0;
        private readonly IProductInterface _productRepository;
        public ProductQuantityQuickAddWindow()
        {
            InitializeComponent();
            this._productRepository = new ProductRepository();
        }
        public string FormatPrice(string Price)
        {
            float number = float.Parse(Price);
            string formattedNumber = number.ToString("#,##0").Replace(",", " ");
            return formattedNumber;
        }
        public void setData( ProductWithDetailsViewModel productWithDetailsViewModel )
        {
            this.ProductId = productWithDetailsViewModel.id;
            this.NewQuantity = productWithDetailsViewModel.quantity;
            lblSubCategoryName.Content = productWithDetailsViewModel.Category;
            lblSubCategoryName3.Content = productWithDetailsViewModel.Subcategory;
            lblSubCategoryName4.Content = productWithDetailsViewModel.product_name;
            string FormattedQuantity = FormatPrice((productWithDetailsViewModel.quantity).ToString());
            lblQuantity.Content = FormattedQuantity;
            string FormattedStartingPrice = FormatPrice((productWithDetailsViewModel.StartingPrice).ToString());
            lblStartingPrice.Content = FormattedStartingPrice;
            string FormattedSoldPrice = FormatPrice((productWithDetailsViewModel.SoldPrice).ToString());
            lblFinishPrice.Content = FormattedSoldPrice;
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            if (tbQuantity.Text.Length > 0 && tbQuantity.Text.Length < 16)
            {
                string ReFormattedStringForQuantity = ReformatNumericString(tbQuantity.Text);
                if (ContainsPunctuation(ReFormattedStringForQuantity) == false)
                {
                    if (IsDigitOnly(ReFormattedStringForQuantity)) count++;
                    else MessageBox.Show("Только цифра для количества");
                }
                else MessageBox.Show("Без знаков препинания");
                if(count == 1)
                {
                   
                    float soldPrice = GetFloatValueFromFormattedText(tbQuantity.Text);
                    this.NewQuantity+=(int)soldPrice;
                    var dbResult = await _productRepository.AddQuantityToProduct(ProductId,NewQuantity);
                    if (dbResult == 1)
                    {
                        MessageBox.Show("Добавлен!");
                        RefreshPage?.Invoke();
                        this.Close();
                    }
                    else MessageBox.Show("Не добавлено!");
                }
            }
            else
            {
                MessageBox.Show("Проверьте количества. Должно быть минимум 1 символов и не более 16 символов ! ");
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

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }              
        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true; // Block the space key input
            }
        }

        private void scrollWindow(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
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
