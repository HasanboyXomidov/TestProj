using BurgerMenu_Desktop.Entities.Categories;
using BurgerMenu_Desktop.Entities.Tabs;
using BurgerMenu_Desktop.Interfaces.Tabs;
using BurgerMenu_Desktop.Repositories.Tabs;
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

namespace BurgerMenu_Desktop.Windows.TabWindows
{
    /// <summary>
    /// Interaction logic for TabUpdateWindow.xaml
    /// </summary>
    public partial class TabUpdateWindow : Window
    {
        public delegate void RefreshPaymentWindowAboveWrapPanel();
        public RefreshPaymentWindowAboveWrapPanel? RefreshPage {  get; set; }
        public RefreshPaymentWindowAboveWrapPanel? RefreshPage2 { get; set; }
        private readonly ITabsrepository _tabsRepository;
        public long TabId { get; set; }
        public Tab tab;
        public TabUpdateWindow()
        {
            InitializeComponent();
            this._tabsRepository = new TabRepository();
        }
        private bool ContainsPunctuation(string text)
        {
            // Define the regular expression pattern to match punctuation characters
            string pattern = @"[\p{P}]";
            Regex regex = new Regex(pattern);

            // Check if the text contains any punctuation characters
            return regex.IsMatch(text);
        }
        public void setData(long TabId)
        {
            this.TabId = TabId;
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

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.TabId != 0)
            {
                var dbResult = await _tabsRepository.GetTabByTabId(this.TabId);
                if (dbResult != null)
                {
                    tbTabName.Text = dbResult.name;
                    this.tab = new Tab()
                    {
                        id =dbResult.id,
                        name = dbResult.name,
                        kassa_id = dbResult.kassa_id,
                    };
                }
                else MessageBox.Show("Не могу получить идентификатор вкладки");
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;

            if (tbTabName.Text.Length == 0)
            {
                MessageBox.Show("требуется название вкладки");
            }
            else
            {
                if (ContainsPunctuation(tbTabName.Text) == false) count++;
                else MessageBox.Show("без знаков препинания");
                if (tbTabName.Text.Length >= 4 && tbTabName.Text.Length <= 50) count++;
                else MessageBox.Show("Проверьте имя вкладки. Должно быть минимум 4 символов и не более 50 символов !");
                if (count == 2)
                {
                    tab.name = tbTabName.Text;
                    if (MessageBox.Show("Вы хотите обновить?",
                       "Обновить вкладки",
                        MessageBoxButton.YesNo,
                       MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        var updateResult = await _tabsRepository.UpdateAsync(TabId,this.tab);
                        if (updateResult > 0)
                        {

                            MessageBox.Show("Обновлено!!");
                            RefreshPage?.Invoke();
                            RefreshPage2?.Invoke();
                            this.Close();

                        }
                        else MessageBox.Show("Название вкладки должно быть уникальным!");
                    }
                    else MessageBox.Show("Hе Обновлено");
                }
            }
        }

    }
}
