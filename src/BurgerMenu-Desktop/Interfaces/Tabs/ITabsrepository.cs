using BurgerMenu_Desktop.Entities.Tabs;
using BurgerMenu_Desktop.ViewModels.Tabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMenu_Desktop.Interfaces.Tabs;

public interface ITabsrepository : IRepository<Tab, TabViewModel>
{
    public Task<IList<TabViewModel>> GetTabsByKassaIdAsync(long KassaId);
    public Task<TabViewModel> GetTabByTabId(long TabId);
}
