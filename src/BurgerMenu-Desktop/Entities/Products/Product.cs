using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMenu_Desktop.Entities.Products;

public class Product 
{
    public long Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public float StartingPrice { get; set; }
    public float SoldPrice{ get; set; }
    public long BarCode{ get; set; }
    public long SubcategoryId { get; set; }

}
