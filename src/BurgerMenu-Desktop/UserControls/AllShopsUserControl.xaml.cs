using BurgerMenu_Desktop.Entities.Shops;
using BurgerMenu_Desktop.ViewModels.Shops;
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
    /// Interaction logic for AllShopsUserControl.xaml
    /// </summary>
    public partial class AllShopsUserControl : UserControl
    {
        public AllShopsUserControl()
        {
            InitializeComponent();
        }
        public void setData(ShopsViewModel shopsViewModels)
        {
            bgImgShops.ImageSource = new BitmapImage(new System.Uri(shopsViewModels.ImagePath,UriKind.Relative));
            lblShoName.Content = shopsViewModels.Name;
        }
    }
}
