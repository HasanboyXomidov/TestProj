using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMenu_Desktop.Interfaces;

public interface IRepository<TEntity , TViewModel>
{
    public Task<int> CreateAsync(TEntity entity);
    public Task<int> UpdateAsync(long id, TEntity editObj);
    public Task<int> DeleteAsync(long id);
    public Task<IList<TViewModel>> GetAllAsync();


}
