using BurgerMenu_Desktop.Entities.Categories;
using BurgerMenu_Desktop.Entities.Shops;
using BurgerMenu_Desktop.Interfaces.Categories;
using BurgerMenu_Desktop.Pages;
using BurgerMenu_Desktop.Pages.AuthPages;
using BurgerMenu_Desktop.Repositories.Categories;
using BurgerMenu_Desktop.ViewModels.Categories;
using BurgerMenu_Desktop.ViewModels.Shops;
using BurgerMenu_Desktop.Windows.CategoryWindows;
using BurgerMenu_Desktop.Windows.ShopWindows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static BurgerMenu_Desktop.Pages.CategoriesPage;

namespace BurgerMenu_Desktop.UserControls
{
    /// <summary>
    /// Interaction logic for CategoryUserControl.xaml
    /// </summary>
    
    public partial class CategoryUserControl : UserControl
    {
        public delegate void RefreshDelegateForMyShopPage();
        public RefreshDelegateForMyShopPage RefreshPage { get; set; }
        private string ShopName { get; set; }

        private readonly ICategoryRepository _categoryRepository;
        public CategoryViewModel categoryViewModel { get; set; }
        public CategoryUserControl()
        {
            InitializeComponent();
            this._categoryRepository = new CategoryRepository();
        }

        public void setData(CategoryViewModel category,string ShopName)
        {
            categoryViewModel = category;
            lblShoName.Text = categoryViewModel.CategoryName;
            this.ShopName = ShopName;
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Border border)
            {
                border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6A9C89"));
                Cursor = Cursors.Hand;
            }
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Border border)
            {
                border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Transparent"));
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {            
            if (Window.GetWindow(this) is MainWindow mainWindow)
            {                
                SubCategoryPage subCategoryPage = new SubCategoryPage();
                subCategoryPage.setCategoryid(categoryViewModel.Id,categoryViewModel.CategoryName,ShopName);
                mainWindow.PageNavigator.Navigate(subCategoryPage);
                Button? btnBackToCategory = mainWindow.FindName("btnBacktoCategory") as Button;
                if (btnBackToCategory !=null) btnBackToCategory.Visibility = Visibility.Visible;
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы хотите удалить?",
                  "Удалить Категория",
           MessageBoxButton.YesNo,
                  MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var dll = await _categoryRepository.DeleteAsync(categoryViewModel.Id);
                if (dll > 0)
                {
                    MessageBox.Show("Удален!!");
                    RefreshPage?.Invoke();
                }
                else MessageBox.Show("Не удалено!!");
            }
            else MessageBox.Show("Не удалено!!2");
        }

        private void updateBtn(object sender, RoutedEventArgs e)
        {
            Category category = new Category
            {
                Id = categoryViewModel.Id,
                CategoryName = categoryViewModel.CategoryName,
                ShopId = categoryViewModel.ShopId
            };            
            CategoryUpdateWindow categoryUpdateWindow = new CategoryUpdateWindow();
            categoryUpdateWindow.setData(category);
            categoryUpdateWindow.RefreshPage = refreshCategoryPage;
            categoryUpdateWindow.ShowDialog();
          
        }
        private void refreshCategoryPage()
        {
            RefreshPage?.Invoke();
        }
    }
}
