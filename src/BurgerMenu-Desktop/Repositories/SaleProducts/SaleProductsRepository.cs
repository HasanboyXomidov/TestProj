using BurgerMenu_Desktop.Entities.SaleProducts;
using BurgerMenu_Desktop.Entities.SubCategories;
using BurgerMenu_Desktop.Interfaces.SaleProducts;
using BurgerMenu_Desktop.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace BurgerMenu_Desktop.Repositories.SaleProducts;

public class SaleProductsRepository : BaseRepository , ISaleProductsRepository
{
    public async Task<int> CreateSaleProduct(ProductTab productTab)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO `sale_products`( `product_name`, `product_quantity`, `StartingPrice`, `SoldPrice`, `BarCode`, `Category`, `Subcategory`, `tab_id`) " +
                "VALUES (@product_name ,@ProductQuantity, @StartingPrice, @SoldPrice ,@BarCode ,  @Category  , @Subcategory  , @tab_id)";
            var result = await _connection.ExecuteAsync(query, productTab);
            return result;
        }
        catch
        {
            return 0;
        }
        finally { await _connection.CloseAsync(); }
    }

    public async Task<IList<ProductWithTabDetails>> GetAllProductWithTabsWithTabId(long tabId)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select * from sale_products where tab_id = {tabId} order by id desc;";
            var result = (await _connection.QueryAsync<ProductWithTabDetails>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<ProductWithTabDetails>();
        }
        finally { await _connection.CloseAsync(); }
    }
}
