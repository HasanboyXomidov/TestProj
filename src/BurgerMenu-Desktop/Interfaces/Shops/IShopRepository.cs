using BurgerMenu_Desktop.Entities.Shops;
using BurgerMenu_Desktop.Repositories;
using BurgerMenu_Desktop.ViewModels.Shops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMenu_Desktop.Interfaces.Shops;

public interface IShopRepository : IRepository<Shop, ShopsViewModel>
{
    public Task<IList<ShopsViewModel>> GetAllAsyncById(int userId);
    public Task<ShopsViewModel?> GetShopById(long ShopId);
}
