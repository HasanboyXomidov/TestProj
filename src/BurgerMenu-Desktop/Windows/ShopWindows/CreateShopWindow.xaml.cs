using BurgerMenu_Desktop.Entities.Shops;
using BurgerMenu_Desktop.Interfaces.Shops;
using BurgerMenu_Desktop.Repositories.Shops;
using Microsoft.Win32;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace BurgerMenu_Desktop.Windows.ShopWindows;

/// <summary>
/// Interaction logic for CreateShopWindow.xaml
/// </summary>
public partial class CreateShopWindow : Window
{
    private readonly IShopRepository _shopRepository;
    public Func<Task> Refresh { get; set; }

    public CreateShopWindow()
    {
        InitializeComponent();
        this._shopRepository = new ShopRepository();
    }

    private void btnClose_Click(object sender, RoutedEventArgs e)
    {
        this.Close();               
    }

    private void ImgCourseImage_MouseDown(object sender, MouseButtonEventArgs e)
    {
        var openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png, *.gif, *.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
        if (openFileDialog.ShowDialog() == true )
        {
            string selectedFilePath = openFileDialog.FileName;
            ImgBImage.ImageSource = new BitmapImage(new Uri(selectedFilePath, UriKind.Relative));
        }
    }

    private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
    {

    }
    public bool ContainsNonLatinCharacters(string input)
    {

        string latinAlphaNumericPattern = @"^[a-zA-Z0-9]+$";
        // Check if the input contains any Latin alphabets
        return Regex.IsMatch(input, latinAlphaNumericPattern);
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

    private void Window_Loaded(object sender, RoutedEventArgs e)
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

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        int count = 0;
        if (ImgBImage.ImageSource==null )
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
                shop.UserId = Convert.ToInt32(Properties.Settings.Default.UserId);
                shop.ImagePath = imagePath;
                var dbResult = await _shopRepository.CreateAsync(shop);
                if (dbResult > 0)
                {
                    MessageBox.Show("Создан Магазин");
                    await Refresh();
                    this.Close();
                }
                else MessageBox.Show("Shop not Created Something wrong !");

            }
        }
        
    }

}
