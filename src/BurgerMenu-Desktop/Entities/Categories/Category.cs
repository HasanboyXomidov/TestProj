using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMenu_Desktop.Entities.Categories;

public class Category : Auditable
{
    public string CategoryName { get; set; }=string.Empty;
    public long ShopId { get; set; }
}
