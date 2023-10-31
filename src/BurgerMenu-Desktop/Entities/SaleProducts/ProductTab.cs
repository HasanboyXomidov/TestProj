﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMenu_Desktop.Entities.SaleProducts;

public class ProductTab
{
    public long id { get; set; }
    public string product_name { get; set; } = string.Empty;
    public int ProductQuantity { get; set; }
    public float StartingPrice { get; set; }
    public float SoldPrice { get; set; }
    public long BarCode { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Subcategory { get; set; } = string.Empty;
    public long tab_id { get; set; }
}
