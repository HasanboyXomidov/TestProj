using BurgerMenu_Desktop.Entities.SubCategories;
using BurgerMenu_Desktop.ViewModels.SubCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMenu_Desktop.Interfaces.SubCategories;

public  interface ISubCategoryRepository : IRepository<SubCategory , SubCategoryViewModel>
{

}
