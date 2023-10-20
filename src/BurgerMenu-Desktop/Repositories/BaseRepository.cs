using BurgerMenu_Desktop.Security;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMenu_Desktop.Repositories;

public abstract class BaseRepository
{
    protected readonly MySqlConnection _connection;
    public BaseRepository()
    {
        IdentitySingleton identity = IdentitySingleton.GetInstance();
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        this._connection = new MySqlConnection(           
           identity.connectionString
            );
    }
}
