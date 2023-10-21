using BurgerMenu_Desktop.Entities.Categories;
using BurgerMenu_Desktop.Entities.SubCategories;
using BurgerMenu_Desktop.Interfaces.SubCategories;
using BurgerMenu_Desktop.Repositories.SubCategories;
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

namespace BurgerMenu_Desktop.Windows.SubCategoryWindows
{
    /// <summary>
    /// Interaction logic for SubCategoryUpdateWindow.xaml
    /// </summary>
    public partial class SubCategoryUpdateWindow : Window
    {
        public delegate void RefreshDelegateForMyShopPage();
        public RefreshDelegateForMyShopPage? RefreshPage { get; set; }
        private readonly ISubCategoryRepository _repository;
        private long Id { get; set; } 
        private SubCategory subCategory { get; set; }
        public SubCategoryUpdateWindow()
        {
            InitializeComponent();
            this._repository = new SubCategoryRepository();
        }
        public void setData(long id,SubCategory subCategory)
        {
            tbSubCategoryName.Text = subCategory.SubCategoryName;
            this.Id = id;
            this.subCategory = subCategory;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;

            if (tbSubCategoryName.Text.Length == 0)
            {
                MessageBox.Show("требуется название Подкатегория");
            }
            else
            {
                if (ContainsPunctuation(tbSubCategoryName.Text) == false) count++;
                else MessageBox.Show("без знаков препинания");
                if (tbSubCategoryName.Text.Length >= 4) count++;
                else MessageBox.Show("Проверьте имя Подкатегория. Должно быть минимум 4 буквы!");
                if (count == 2)
                {
                   
                    this.subCategory.SubCategoryName = tbSubCategoryName.Text;
                    if (MessageBox.Show("Вы хотите обновить?",
                       "Обновить Подкатегория",
                        MessageBoxButton.YesNo,
                       MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        var updateResult = await _repository.UpdateAsync(this.Id, this.subCategory);
                        if (updateResult > 0)
                        {

                            MessageBox.Show("Обновлено!!");
                            RefreshPage?.Invoke();
                            this.Close();

                        }
                        else MessageBox.Show("Название Подкатегория должно быть уникальным!");
                    }
                    else MessageBox.Show("Hе Обновлено");
                }
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

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
