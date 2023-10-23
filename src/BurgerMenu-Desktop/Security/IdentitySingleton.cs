using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMenu_Desktop.Security;

public class IdentitySingleton
{
    public static IdentitySingleton _identitySingleton;
    public string connectionString = "server=localhost;database=myshops_db;user=root;password=2315655i";
    private IdentitySingleton()
    {
        
    }
    public static IdentitySingleton GetInstance( )
    {
        if( _identitySingleton == null )
        {
            _identitySingleton = new IdentitySingleton();
        }
        return _identitySingleton;
    }
}
