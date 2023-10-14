using BurgerMenu_Desktop.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMenu_Desktop.Interfaces.Users;

public interface IUserRepository 
{
    public Task<int> CreateAsync(User user);
    public Task<IList<User>> GetUserByUserName(string UserName);

}
