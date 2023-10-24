using BurgerMenu_Desktop.Entities.Shops;
using BurgerMenu_Desktop.Interfaces;
using BurgerMenu_Desktop.Interfaces.Shops;
using BurgerMenu_Desktop.ViewModels.Shops;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace BurgerMenu_Desktop.Repositories.Shops;

public class ShopRepository : BaseRepository, IShopRepository
{
    public async Task<int> CreateAsync(Shop entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO shops (name,user_id) VALUES( @Name , @UserId );";
            var result = await _connection.ExecuteAsync(query, entity);
            return result;
        }
        catch 
        {
            return 0;   
        }
        finally { await _connection.CloseAsync(); }
    }

    public async Task<int> DeleteAsync(long Id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "delete from shops where id = @id";
            var result = await _connection.ExecuteAsync(query, new { id = Id });
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

    public async Task<IList<ShopsViewModel>> GetAllAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "select * from shops order by id desc;";
            var result = (await _connection.QueryAsync<ShopsViewModel>(query)).ToList();
            return result;

        }
        catch
        {
            return new List<ShopsViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<IList<ShopsViewModel>> GetAllAsyncById(int userId)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select * from shops where user_id = {userId} order by id desc;";
            var result = (await _connection.QueryAsync<ShopsViewModel>(query)).ToList();
            return result;
        }
        catch 
        {
            return new List<ShopsViewModel>();
        }
        finally { await _connection.CloseAsync(); }
    }
  
    public async Task<int> UpdateAsync(long id, Shop entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"update shops set name=@Name , updated_at=now() where id={id}";
            var result = await _connection.ExecuteAsync(query, entity );
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
}
