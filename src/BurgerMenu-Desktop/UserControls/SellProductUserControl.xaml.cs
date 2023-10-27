﻿using BurgerMenu_Desktop.ViewModels.Products;
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
    /// Interaction logic for SellProductUserControl.xaml
    /// </summary>
    public partial class SellProductUserControl : UserControl
    {
        public SellProductUserControl()
        {
            InitializeComponent();
        }
        public void setData(ProductWithTabDetails product)
        {
            lblProduct.Text = product.product_name;
        }
    }
}