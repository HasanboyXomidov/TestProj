using BurgerMenu_Desktop.Entities.Categories;
using BurgerMenu_Desktop.ViewModels.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMenu_Desktop.Interfaces.Categories;

public interface ICategoryRepository : IRepository<Category , CategoryViewModel>
{
    public Task<List<CategoryViewModel>> GetAllByIdAsync(long shopId);
}
