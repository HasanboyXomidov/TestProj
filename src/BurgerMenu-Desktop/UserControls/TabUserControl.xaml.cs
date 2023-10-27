using BurgerMenu_Desktop.ViewModels.Tabs;
using BurgerMenu_Desktop.Windows.MyCasheRegisterWindows;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for TabUserControl.xaml
    /// </summary>
    public partial class TabUserControl : UserControl
    {

        

        public delegate void UserControlClickedEventHandler(long id);
        public event UserControlClickedEventHandler UserControlClicked;

        PaymentWindow ParentWindow;

        public long TabId { get; set; }

        public TabUserControl(PaymentWindow paymentWindow)
        {
            InitializeComponent();
            this.ParentWindow = paymentWindow;
        }
      
        private void UserControl_Click(object sender, RoutedEventArgs e)
        {
            
        }
        public void setData(TabViewModel tabViewModel)
        {
            lblTabName.Content = tabViewModel.name;
            this.TabId = tabViewModel.id;
        }
        
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Raise the UserControlClicked event and pass the ID
            UserControlClicked?.Invoke(TabId);

            ParentWindow.ClearUserControlBorder();

            BorderContainer.BorderBrush = Brushes.Red;


        }
        
    }
}
