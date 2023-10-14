using BurgerMenu_Desktop.Entities.Users;
using BurgerMenu_Desktop.Interfaces.Users;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace BurgerMenu_Desktop.Repositories.Users;

public class UserRepository : BaseRepository, IUserRepository
{
    public async Task<int> CreateAsync(User user)
    {
		try
		{
			await _connection.OpenAsync();			
			string query = "INSERT INTO users (username, password_hash, salt) VALUES (@UserName, @PasswordHash, @Salt);";
            var result = await _connection.ExecuteAsync(query, user);
			return result;

        }
        catch
		{
			return 0;
		}
		finally
		{
			await _connection.CloseAsync();
		}

	}

    public async Task<IList<User>> GetUserByUserName(string UserName)
    {
		try
		{
			await _connection.OpenAsync();
			string query = "SELECT * FROM users WHERE username = @userName;";
            var result = (await _connection.QueryAsync<User>(query , new { userName = UserName })).ToList();
            return result;
            
        }
        catch 
		{
            return new List<User>();
        }
        finally
		{
			await _connection.CloseAsync();
		}
    }
}
