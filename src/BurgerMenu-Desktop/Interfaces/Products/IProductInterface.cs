using BurgerMenu_Desktop.Entities.Products;
using BurgerMenu_Desktop.ViewModels.Products;
using BurgerMenu_Desktop.ViewModels.Shops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMenu_Desktop.Interfaces.Products
{
    public interface IProductInterface : IRepository<Product , ProductsViewModel>
    {
        public Task<List<ProductsViewModel>> GetAllProductsBySubcategoryIdAsync(long SubCategoryId);
        public Task<List<ProductWithDetailsViewModel>> GetAllWithCategoryAndSubcategoryIdById();

    }
}
