using BurgerMenu_Desktop.Entities.Shops;
using BurgerMenu_Desktop.Entities.Tabs;
using BurgerMenu_Desktop.Interfaces.Tabs;
using BurgerMenu_Desktop.ViewModels.CasheRegisters;
using BurgerMenu_Desktop.ViewModels.Tabs;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMenu_Desktop.Repositories.Tabs;

class TabRepository : BaseRepository, ITabsrepository
{
    public async Task<int> CreateAsync(Tab entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO `tabs`(`name`, `kassa_id`) VALUES ( @name , @kassa_id)";
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
            string query = $"DELETE FROM `tabs` WHERE id = @Id";
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

    public Task<IList<TabViewModel>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<TabViewModel> GetTabByTabId(long TabId)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM `tabs` WHERE id = @tabId";
            var result = await _connection.QuerySingleAsync<TabViewModel>(query, new { tabId = TabId });
            return result;
        }
        catch 
        {
            return new TabViewModel();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<IList<TabViewModel>> GetTabsByKassaIdAsync(long KassaId)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select * from tabs where kassa_id = {KassaId} order by id desc;";
            var result = (await _connection.QueryAsync<TabViewModel>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<TabViewModel>();
        }
        finally { await _connection.CloseAsync(); }
    }

    public async Task<int> UpdateAsync(long id, Tab editObj)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"UPDATE `tabs` SET `name`=@name WHERE id = {id}";
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
