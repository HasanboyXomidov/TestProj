using BurgerMenu_Desktop.Entities.CasheRegisters;
using BurgerMenu_Desktop.Entities.Categories;
using BurgerMenu_Desktop.Interfaces.CasheRegisters;
using BurgerMenu_Desktop.Repositories.CasheRegisters;
using BurgerMenu_Desktop.ViewModels.CasheRegisters;
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
    /// Interaction logic for KassaUpdateWindow.xaml
    /// </summary>
    public partial class KassaUpdateWindow : Window
    {
        public delegate void RefreshKassaPageDelegate();
        public RefreshKassaPageDelegate? RefreshPage { get ; set; }
        private Kassa? kassa{ get; set; }
        private readonly IKassaRepository _kassaRepository;
        public KassaUpdateWindow()
        {
            InitializeComponent();
            this._kassaRepository = new KassaRepository();
        }
        public void setData(KassaViewModel kassaViewModel)
        {
            this.kassa = new Kassa()
            {
                Id = kassaViewModel.Id,
                Name = kassaViewModel.Name,
                ShopId = kassaViewModel.ShopId,
            };
            tbCasheRegister.Text = kassaViewModel.Name;
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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;

            if (tbCasheRegister.Text.Length == 0)
            {
                MessageBox.Show("требуется название Kacca");
            }
            else
            {
                if (ContainsPunctuation(tbCasheRegister.Text) == false) count++;
                else MessageBox.Show("без знаков препинания");
                if (tbCasheRegister.Text.Length >= 4 && tbCasheRegister.Text.Length <= 50) count++;
                else MessageBox.Show("Проверьте имя Kacca. Должно быть минимум 4 символов и не более 50 символов !");
                if (count == 2)
                {
                    
                    if (MessageBox.Show("Вы хотите обновить?",
                       "Обновить Kacca",
                        MessageBoxButton.YesNo,
                       MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        this.kassa.Name = tbCasheRegister.Text;
                        var updateResult = await _kassaRepository.UpdateAsync(kassa.Id , kassa);
                        if (updateResult > 0)
                        {

                            MessageBox.Show("Обновлено!!");
                            RefreshPage?.Invoke();
                            this.Close();

                        }
                        else MessageBox.Show("Название Kacca должно быть уникальным!");
                    }
                    else MessageBox.Show("Hе Обновлено");
                }
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
