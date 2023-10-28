using BurgerMenu_Desktop.Entities.SaleProducts;
using BurgerMenu_Desktop.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMenu_Desktop.Interfaces.SaleProducts
{
    public interface ISaleProductsRepository
    {
        public Task<IList<ProductWithTabDetails>> GetAllProductWithTabsWithTabId(long tabId);
        public Task<int> CreateSaleProduct(ProductTab productTab);
    }
}
