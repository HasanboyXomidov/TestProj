using BurgerMenu_Desktop.ViewModels.Tabs;
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
    public class BooleanToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isSelected && isSelected)
            {
                return Brushes.Black;
            }
            return Brushes.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// Interaction logic for TabUserControl.xaml
    /// </summary>
    public partial class TabUserControl : UserControl
    {
        public static readonly DependencyProperty IsSelectedProperty =
       DependencyProperty.Register("IsSelected", typeof(bool), typeof(TabUserControl),
           new FrameworkPropertyMetadata(false));

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }
        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            IsSelected = true;
        }
        public delegate void UserControlClickedEventHandler(long id);
        public event UserControlClickedEventHandler UserControlClicked;

        public long TabId { get; set; }

        public TabUserControl()
        {
            InitializeComponent();
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
        }
        
    }
}
