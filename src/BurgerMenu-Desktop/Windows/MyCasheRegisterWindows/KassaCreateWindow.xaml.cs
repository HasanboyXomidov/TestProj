using BurgerMenu_Desktop.Entities.CasheRegisters;
using BurgerMenu_Desktop.Entities.SubCategories;
using BurgerMenu_Desktop.Interfaces;
using BurgerMenu_Desktop.Interfaces.CasheRegisters;
using BurgerMenu_Desktop.Repositories.CasheRegisters;
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

namespace BurgerMenu_Desktop.Windows.MyCasheRegisterWindows
{
    /// <summary>
    /// Interaction logic for KassaCreateWindow.xaml
    /// </summary>
    public partial class KassaCreateWindow : Window
    {
        public delegate void RefreshCasheRegisterPage();
        public RefreshCasheRegisterPage? RefreshPage { get; set; }
        private long ShopId {  get; set; }
        private readonly IKassaRepository _kassaRepository;
        public KassaCreateWindow()
        {
            InitializeComponent();
            this._kassaRepository = new KassaRepository();
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
        public void setData(long shopId)
        {
            this.ShopId = shopId;
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            if (tbMyCasheRegister.Text.Length == 0)
            {
                MessageBox.Show("требуется название Касса");
            }
            else
            {               
                if (ContainsPunctuation(tbMyCasheRegister.Text) == false) count++;
                else MessageBox.Show("без знаков препинания");
                if (tbMyCasheRegister.Text.Length >= 4 && tbMyCasheRegister.Text.Length <= 50) count++;
                else MessageBox.Show("Проверьте имя Касса. Должно быть минимум 4 символов и не более 50 символов !!");
                if (count == 2)
                {
                    Kassa kassa = new Kassa();
                    kassa.Name =tbMyCasheRegister.Text;
                    kassa.ShopId = this.ShopId;
                    var dbResult = await _kassaRepository.CreateAsync(kassa);
                    if (dbResult > 0)
                    {
                        MessageBox.Show("Создан Касса");
                        RefreshPage?.Invoke();
                        this.Close();
                    }
                    else MessageBox.Show("Название Касса должно быть уникальным!");

                }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
