using BurgerMenu_Desktop.Security;
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
using static Dapper.SqlMapper;

namespace BurgerMenu_Desktop.UserControls
{
    /// <summary>
    /// Interaction logic for SellProductUserControl.xaml
    /// </summary>
    public partial class SellProductUserControl : UserControl
    {
        public delegate void RefreshPaymendWindowThirdWrapPanel();
        public RefreshPaymendWindowThirdWrapPanel? RefreshThirdWrapPanel { get; set; }
        public ProductWithTabDetails? productWithTabDetails;
        public SellProductUserControl()
        {
            InitializeComponent();
        }
        public void setData(ProductWithTabDetails product)
        {
            lblProduct.Text = product.product_name;
            this.productWithTabDetails = product;
        }

        private void brButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var identity = IdentitySingleton.GetInstance();
            var sellProductList = identity.AddToCartList;
            int checkName = 0;
            for(int i = 0; i < sellProductList.Count; i++)
            {
                if (sellProductList[i].product_name == productWithTabDetails?.product_name)
                {
                    //sellProductList[i].SoldPrice += productWithTabDetails.SoldPrice;
                    checkName = i;
                    break;
                }               
            }
            if( checkName > 0 && productWithTabDetails?.SoldPrice != null) 
            {
                sellProductList[checkName].SoldPrice += productWithTabDetails.SoldPrice;
                sellProductList[checkName].quantity++;
                MessageBox.Show("Добавлен +");
                RefreshThirdWrapPanel?.Invoke();
            }
            else
            {
                if (productWithTabDetails!=null)
                {
                    sellProductList.Add(productWithTabDetails);
                    sellProductList[checkName].quantity++;
                    MessageBox.Show("Добавлен новый +");
                    RefreshThirdWrapPanel?.Invoke();
                }
            }


        }
    }
}
