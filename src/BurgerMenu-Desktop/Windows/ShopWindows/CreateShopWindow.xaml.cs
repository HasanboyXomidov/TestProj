using BurgerMenu_Desktop.Entities.Shops;
using BurgerMenu_Desktop.Interfaces.Shops;
using BurgerMenu_Desktop.Repositories.Shops;
using Microsoft.Win32;
using System;
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
        if (openFileDialog.ShowDialog() == true )
        {
            string ImagePath = openFileDialog.FileName;
            ImgBImage.ImageSource = new BitmapImage(new Uri(ImagePath,UriKind.Relative));
        }
    }

    private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
    {

    }

    private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {

    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {

    }
    
    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        int count = 0;
        string imagePath = ImgBImage.ImageSource.ToString();
        if (string.IsNullOrEmpty(imagePath) == false) count++;
        else MessageBox.Show("Check Image");
        if (tbShopName.Text.Length >= 4 ) count++;
        else MessageBox.Show("Check ShopName Must be minimum 4 letters !");
        if(count == 2) 
        {

            Shop shop = new Shop();
            shop.Name = tbShopName.Text;
            shop.ImagePath = imagePath;
            var dbResult = await _shopRepository.CreateAsync(shop);
            if (dbResult > 0)
            {
                MessageBox.Show("Created Shop");
                await Refresh();
                this.Close();
            }
            else MessageBox.Show("Shop not Created Something wrong !");

        }
    }

}
