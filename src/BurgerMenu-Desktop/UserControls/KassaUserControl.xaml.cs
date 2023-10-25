using BurgerMenu_Desktop.Entities.CasheRegisters;
using BurgerMenu_Desktop.Interfaces.CasheRegisters;
using BurgerMenu_Desktop.Repositories.CasheRegisters;
using BurgerMenu_Desktop.ViewModels.CasheRegisters;
using BurgerMenu_Desktop.ViewModels.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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
    /// Interaction logic for KassaUserControl.xaml
    /// </summary>
    public partial class KassaUserControl : UserControl
    {
        public delegate void RefreshMyCasheRegisterPage();
        public RefreshMyCasheRegisterPage? RefreshPage {  get; set; }
        private long KassaId {  get; set; } 
        public KassaViewModel kassaViewModel { get; set; }
        private readonly IKassaRepository _kassaRepository;
        public KassaUserControl()
        {
            InitializeComponent();
            this._kassaRepository = new KassaRepository();
        }
        public void setData(KassaViewModel kassa)
        {
            this.KassaId =kassa.Id;
            kassaViewModel = kassa;
            lblCasheRegister.Text = kassa.Name;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы хотите удалить?",
                  "Удалить Кассa",
           MessageBoxButton.YesNo,
                  MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var dll = await _kassaRepository.DeleteAsync(KassaId);
                if (dll > 0)
                {
                    MessageBox.Show("Удален!!");
                    RefreshPage?.Invoke();
                }
                else MessageBox.Show("Не удалено!!");
            }
            else MessageBox.Show("Не удалено!!2");
        }

        private async void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            Kassa kassa = new Kassa()
            {
                Id = kassaViewModel.Id,
                Name = kassaViewModel.Name,
                ShopId = kassaViewModel.ShopId,
            };

            
        }
    }
}
