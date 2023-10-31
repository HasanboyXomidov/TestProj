using BurgerMenu_Desktop.Entities.Products;
using BurgerMenu_Desktop.ViewModels.Products;
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
    /// Interaction logic for ProductTotalCollectUserControl_.xaml
    /// </summary>
    public partial class ProductTotalCollectUserControl : UserControl
    {
        public ProductTotalCollectUserControl()
        {
            InitializeComponent();
        }
        public string FormatPrice(string Price)
        {
            float number = float.Parse(Price);
            string formattedNumber = number.ToString("#,##0").Replace(",", " ");
            return formattedNumber;
        }
        public void setData(ProductWithTabDetails productWithTabDetails)
        {

            lblProductName.Content = productWithTabDetails.product_name;
            string FormattedSoldPrice = FormatPrice(productWithTabDetails.SoldPrice.ToString());
            lblProductCost.Content = FormattedSoldPrice;
            string FormattedQuantity = FormatPrice((productWithTabDetails.quantity).ToString());
            lblProductQuantity.Content= FormattedQuantity;
            float TotalSum = productWithTabDetails.SoldPrice * productWithTabDetails.quantity;
            string FormattedTotalPrice = FormatPrice((TotalSum).ToString());
            lblProductTotalPrice.Content = FormattedTotalPrice;


        }
    }
}
