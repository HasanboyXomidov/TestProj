using BurgerMenu_Desktop.Entities.Categories;
using BurgerMenu_Desktop.Interfaces.Categories;
using BurgerMenu_Desktop.ViewModels.Categories;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMenu_Desktop.Repositories.Categories;

public class CategoryRepository : BaseRepository, ICategoryRepository
{
    public async Task<int> CreateAsync(Category entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO `categories`(`category_name`, `shop_id`) VALUES ( @CategoryName , @ShopId );";
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
            string query = "DELETE FROM `categories` WHERE @Id";
            var result = await _connection.ExecuteAsync(query , new { Id = id });
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
    //Megabite9863322
    public async Task<IList<CategoryViewModel>> GetAllAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM `categories`;";
            var result = ( await _connection.QueryAsync<CategoryViewModel>(query) ).ToList();
            return result;
        }
        catch 
        {
            return new List<CategoryViewModel>();
        }
        finally { await _connection.CloseAsync(); }
    }

    public async Task<int> UpdateAsync(long id, Category entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"UPDATE `categories` SET `category_name`=@CategoryName,`shop_id`=@ShopId,`updated_at`= now() WHERE id = {id}";
            var result = await _connection.ExecuteAsync(query, entity);
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
