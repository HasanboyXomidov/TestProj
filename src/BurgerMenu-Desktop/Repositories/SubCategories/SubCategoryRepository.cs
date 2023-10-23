using BurgerMenu_Desktop.Entities.SubCategories;
using BurgerMenu_Desktop.Entities.Users;
using BurgerMenu_Desktop.Interfaces.SubCategories;
using BurgerMenu_Desktop.ViewModels.Categories;
using BurgerMenu_Desktop.ViewModels.Products;
using BurgerMenu_Desktop.ViewModels.Shops;
using BurgerMenu_Desktop.ViewModels.SubCategories;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace BurgerMenu_Desktop.Repositories.SubCategories;

public class SubCategoryRepository : BaseRepository, ISubCategoryRepository
{
    public async Task<int> CreateAsync(SubCategory entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO `subcategories`(`subcategory_name`, `category_id`) VALUES (@SubCategoryName , @CategoryId)";
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
            string query = "DELETE FROM `subcategories` WHERE id = @Id";
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

    public async Task<IList<SubCategoryViewModel>> GetAllAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM `subcategories` order by id desc;";
            var result = (await _connection.QueryAsync<SubCategoryViewModel>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<SubCategoryViewModel>();
        }
        finally { await _connection.CloseAsync(); }
    }

    public async Task<List<SubCategoryViewModel>> GetAllSubCategoriesByCategoryIdAsync(long categoryId)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select * from subcategories where category_id = {categoryId} order by id desc;";
            var result = (await _connection.QueryAsync<SubCategoryViewModel>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<SubCategoryViewModel>();
        }
        finally { await _connection.CloseAsync(); }
    }
   

    public async Task<int> UpdateAsync(long id, SubCategory editObj)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"UPDATE `subcategories` SET `subcategory_name`=@SubCategoryName WHERE id = {id}";
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
