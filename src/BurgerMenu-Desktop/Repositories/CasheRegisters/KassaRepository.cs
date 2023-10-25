using BurgerMenu_Desktop.Entities.CasheRegisters;
using BurgerMenu_Desktop.Entities.Categories;
using BurgerMenu_Desktop.Interfaces.CasheRegisters;
using BurgerMenu_Desktop.ViewModels.CasheRegisters;
using BurgerMenu_Desktop.ViewModels.SubCategories;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMenu_Desktop.Repositories.CasheRegisters;

public class KassaRepository : BaseRepository, IKassaRepository
{
    public async Task<int> CreateAsync(Kassa entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO `kassa`(`name`, `shop_id`) VALUES (@Name , @ShopId)";
            var result = await _connection.ExecuteAsync(query, entity);
            return result;
        }
        catch
        {
            return 0;
        }
        finally { await _connection.CloseAsync(); }
    }

    public async Task<int> DeleteAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "DELETE FROM `kassa` WHERE id = @Id";
            var result = await _connection.ExecuteAsync(query, new { Id = id });
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

    public Task<IList<KassaViewModel>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IList<KassaViewModel>> GetAllByIdAsync(long ShopId)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select * from kassa where shop_id = {ShopId} order by id desc;";
            var result = (await _connection.QueryAsync<KassaViewModel>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<KassaViewModel>();
        }
        finally { await _connection.CloseAsync(); }
    }

    public async Task<int> UpdateAsync(long id, Kassa editObj)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"UPDATE `kassa` SET `name`=@Name,`shop_id`=@ShopId WHERE id = {id}";
            var result = await _connection.ExecuteAsync(query, editObj);
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
