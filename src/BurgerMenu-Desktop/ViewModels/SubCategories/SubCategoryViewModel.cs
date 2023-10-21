using BurgerMenu_Desktop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMenu_Desktop.ViewModels.SubCategories;

public class SubCategoryViewModel : Auditable
{
    public string SubCategoryName { get; set; } = string.Empty;
    public long CategoryId { get; set; }

}
