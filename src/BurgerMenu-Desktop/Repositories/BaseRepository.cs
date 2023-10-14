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
        _connection = new MySqlConnection();
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        this._connection = new MySqlConnection(
            "server=localhost;database=shop_db;user=root;password=2315655i;"
            );
    }
}
