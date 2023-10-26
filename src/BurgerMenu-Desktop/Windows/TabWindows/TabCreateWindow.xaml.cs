using BurgerMenu_Desktop.Entities.Categories;
using BurgerMenu_Desktop.Entities.Tabs;
using BurgerMenu_Desktop.Interfaces.Tabs;
using BurgerMenu_Desktop.Repositories.Tabs;
using BurgerMenu_Desktop.ViewModels.Tabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BurgerMenu_Desktop.Windows.TabWindows
{
    /// <summary>
    /// Interaction logic for TabCreateWindow.xaml
    /// </summary>
    public partial class TabCreateWindow : Window
    {
        public delegate void RefreshPaymentWindow();
        public RefreshPaymentWindow? RefreshPage { get; set; }
        public Tab tab { get ; set; }   
        private readonly ITabsrepository _tabsRepository;
        public TabCreateWindow()
        {
            InitializeComponent();
            this._tabsRepository = new TabRepository();
        }
        public void setData(long KassaId)
        {
            this.tab = new Tab()
            {                          
                kassa_id = KassaId
            };
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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            if (tbTabs.Text.Length == 0)
            {
                MessageBox.Show("требуется название Вкладка");
            }
            else
            {
                //string imagePath = ImgBImage.ImageSource.ToString();
                if (ContainsPunctuation(tbTabs.Text) == false)
                {
                    if (tbTabs.Text.Length > 50) MessageBox.Show("Вкладка с очень длинным названием");
                    else count++;
                }
                else MessageBox.Show("без знаков препинания");
                if (tbTabs.Text.Length >= 4) count++;
                else MessageBox.Show("Проверьте имя Вкладка. Должно быть минимум 4 буквы!");
                if (count == 2)
                {
                  
                    this.tab.name = tbTabs.Text;
                    var dbResult = await _tabsRepository.CreateAsync(this.tab);
                    if (dbResult > 0)
                    {
                        MessageBox.Show("Создан Вкладка");
                        RefreshPage?.Invoke();
                        this.Close();
                    }
                    else MessageBox.Show("Название Вкладка должно быть уникальным!");

                }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
