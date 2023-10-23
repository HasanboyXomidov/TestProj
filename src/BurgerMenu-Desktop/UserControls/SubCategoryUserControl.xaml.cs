using BurgerMenu_Desktop.Entities.SubCategories;
using BurgerMenu_Desktop.Interfaces.SubCategories;
using BurgerMenu_Desktop.Pages;
using BurgerMenu_Desktop.Pages.AuthPages;
using BurgerMenu_Desktop.Repositories.SubCategories;
using BurgerMenu_Desktop.ViewModels.Categories;
using BurgerMenu_Desktop.ViewModels.SubCategories;
using BurgerMenu_Desktop.Windows.SubCategoryWindows;
using System;
using System.Collections.Generic;
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

namespace BurgerMenu_Desktop.UserControls
{
    /// <summary>
    /// Interaction logic for SubCategoryUserControl.xaml
    /// </summary>
    public partial class SubCategoryUserControl : UserControl
    {
        public delegate void RefreshDelegateForMyShopPage();
        public RefreshDelegateForMyShopPage? RefreshPage { get; set; }
        private long SubCategoryId { get; set; }
        private SubCategory? subCategory {  get; set; }
        private readonly ISubCategoryRepository _repository;
        public SubCategoryUserControl()
        {
            InitializeComponent();
            this._repository = new SubCategoryRepository();
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Border border)
            {
                border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6A9C89"));
                Cursor = Cursors.Hand;
            }
        }
        public void setData(SubCategoryViewModel subcategoryViewModel)
        {
            lblSubCategoryName.Text = subcategoryViewModel.SubCategoryName;
            this.SubCategoryId = subcategoryViewModel.Id;
            SubCategory subCategory = new SubCategory();
            subCategory.Id = subcategoryViewModel.Id;
            subCategory.SubCategoryName = subcategoryViewModel.SubCategoryName;
            subCategory.CategoryId = subcategoryViewModel.Id;
            this.subCategory = subCategory;
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
                ProductsPage productsPage = new ProductsPage();
                mainWindow.PageNavigator.Navigate(productsPage);
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы хотите удалить?",
                 "Удалить Подкатегория",
          MessageBoxButton.YesNo,
                 MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var dll = await _repository.DeleteAsync(this.SubCategoryId);
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
            SubCategoryUpdateWindow subCategoryUpdateWindow = new SubCategoryUpdateWindow();
            subCategoryUpdateWindow.setData(SubCategoryId, subCategory: subCategory);
            subCategoryUpdateWindow.RefreshPage=refreshPage;
            subCategoryUpdateWindow.ShowDialog();
        }
        public void refreshPage()
        {
            RefreshPage?.Invoke();
        }
    }
}
