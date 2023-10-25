using BurgerMenu_Desktop.Entities.CasheRegisters;
using BurgerMenu_Desktop.ViewModels.CasheRegisters;
using Notifications.Wpf.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMenu_Desktop.Interfaces.CasheRegisters;

public interface IKassaRepository : IRepository<Kassa, KassaViewModel>
{
    public Task<IList<KassaViewModel>> GetAllByIdAsync(long ShopId);

}
