using BurgerMenu_Desktop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMenu_Desktop.ViewModels.Categories;

public class CategoryViewModel : Auditable
{
    public string CategoryName { get; set; } = string.Empty;
    public long ShopId { get; set; }
}
