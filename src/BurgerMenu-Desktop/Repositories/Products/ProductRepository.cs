﻿using BurgerMenu_Desktop.Entities.Products;
using BurgerMenu_Desktop.Entities.Users;
using BurgerMenu_Desktop.Interfaces.Products;
using BurgerMenu_Desktop.ViewModels.Products;
using BurgerMenu_Desktop.ViewModels.Shops;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace BurgerMenu_Desktop.Repositories.Products;

public class ProductRepository : BaseRepository, IProductInterface
{
    public async Task<int> AddQuantityToProduct(long ProductId , int ProductQuantity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"UPDATE `products` SET `quantity`=@productQuantity WHERE id = {ProductId}";
            var result = await _connection.ExecuteAsync(query,new { productQuantity = ProductQuantity });
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

    public async Task<int> CreateAsync(Product entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO `products`(`product_name`, `quantity`, `starting_price`, `sold_price`, `bar_code`, `subcategory_id`) VALUES (@ProductName,@Quantity,@StartingPrice,@SoldPrice,@BarCode,@SubcategoryId);";
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
            string query = "delete from products where id = @Id";
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

    public async Task<IList<ProductsViewModel>> GetAllAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM `products` order by id desc";
            var result = (await _connection.QueryAsync<ProductsViewModel>(query)).ToList();
            return result;

        }
        catch
        {
            return new List<ProductsViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<List<ProductsViewModel>> GetAllProductsBySubcategoryIdAsync(long SubCategoryId)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select * from products where subcategory_id = {SubCategoryId} order by id desc;";
            var result = (await _connection.QueryAsync<ProductsViewModel>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<ProductsViewModel>();
        }
        finally { await _connection.CloseAsync(); }
    }

    public async Task<List<ProductWithDetailsViewModel>> GetAllWithCategoryAndSubcategoryIdById()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT products.id, products.product_name,products.quantity, products.starting_price, products.sold_price, products.bar_code,categories.category_name AS 'Category', " +
                "subcategories.subcategory_name AS 'Subcategory' FROM  products JOIN  subcategories ON products.subcategory_id = subcategories.id JOIN  categories ON subcategories.category_id = categories.id order by products.id desc;";
            var result = (await _connection.QueryAsync<ProductWithDetailsViewModel>(query)).ToList();
            return result;

        }
        catch
        {
            return new List<ProductWithDetailsViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> UpdateAsync(long id, Product editObj)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"UPDATE `products` SET `product_name`=@ProductName,`quantity`=@Quantity,`starting_price`=@StartingPrice," +
                $"`sold_price`=@SoldPrice WHERE id = {id}";
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
