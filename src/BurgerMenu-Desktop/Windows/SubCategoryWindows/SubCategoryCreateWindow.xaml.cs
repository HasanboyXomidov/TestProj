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
    /// Interaction logic for SubCategoryCreateWindow.xaml
    /// </summary>
    public partial class SubCategoryCreateWindow : Window
    {
        public delegate void RefreshDelegateForSubCategoriesPage();
        public RefreshDelegateForSubCategoriesPage? RefreshPage { get; set; }
        private long CategoryId {  get; set; }
        private readonly ISubCategoryRepository _repository;
        public SubCategoryCreateWindow()
        {
            InitializeComponent();
            this._repository = new SubCategoryRepository();
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
        public void setData(long CategoryId)
        {
            this.CategoryId = CategoryId;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            if (tbCategoryName.Text.Length == 0)
            {
                MessageBox.Show("требуется название Подкатегория");
            }
            else
            {
                //string imagePath = ImgBImage.ImageSource.ToString();
                if (ContainsPunctuation(tbCategoryName.Text) == false) count++;
                else MessageBox.Show("без знаков препинания");
                if (tbCategoryName.Text.Length >= 4) count++;
                else MessageBox.Show("Проверьте имя Подкатегория. Должно быть минимум 4 буквы!");
                if (count == 2)
                {
                        SubCategory subCategory = new SubCategory();
                        subCategory.SubCategoryName = tbCategoryName.Text;
                        subCategory.CategoryId = this.CategoryId;
                        var dbResult = await _repository.CreateAsync(subCategory);
                    if (dbResult > 0)
                    {
                        MessageBox.Show("Создан Подкатегория");
                        RefreshPage?.Invoke();
                        this.Close();
                    }
                    else MessageBox.Show("Название Подкатегория должно быть уникальным!");

                }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
