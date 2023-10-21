using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMenu_Desktop.Entities.SubCategories;

public class SubCategory : Auditable
{
    public string SubCategoryName { get; set; } = string.Empty;
    public long CategoryId { get; set; }

}
