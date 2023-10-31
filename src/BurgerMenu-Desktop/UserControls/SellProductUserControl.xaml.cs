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
        private long KassaId { get; set; }
        public SellProductUserControl()
        {
            InitializeComponent();
        }
        public void setData(ProductWithTabDetails product,long KassaId)
        {
            lblProduct.Text = product.product_name;
            this.productWithTabDetails = product;
            productWithTabDetails.shopId = KassaId;
            this.KassaId = KassaId;
        }

        private void brButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var identity = IdentitySingleton.GetInstance();
            var sellProductList = identity.AddToCartList;
            int checkName = 0;
            int countIteration = 0;
            bool isOldProduct = false;
            for (int i = 0; i < sellProductList.Count; i++)
            {
                countIteration++;
                if (sellProductList[i].product_name == productWithTabDetails?.product_name && sellProductList[i].shopId == KassaId)
                {
                    checkName = i;
                    isOldProduct  = true;
                    break;
                }                
            }
            if( isOldProduct == true  && productWithTabDetails?.SoldPrice != null) 
            {
                if (sellProductList[checkName].productquantity > 0) /*> sellProductList[checkName].quantity)*/
                {
                    sellProductList[checkName].quantity++;
                    sellProductList[checkName].productquantity--;
                    RefreshThirdWrapPanel?.Invoke();
                }
                else MessageBox.Show("Товара не осталось");
            }
            else
            {
                if (isOldProduct == false && productWithTabDetails!=null)
                {
                    if(productWithTabDetails.productquantity > 0)
                    {
                        sellProductList.Add(productWithTabDetails);
                        sellProductList[countIteration].quantity++;
                        sellProductList[countIteration].productquantity--;
                        RefreshThirdWrapPanel?.Invoke();
                    }
                    else MessageBox.Show("Товара не осталось");

                }
            }


        }
    }
}
