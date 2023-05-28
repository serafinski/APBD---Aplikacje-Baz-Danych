using Exercise5.Models.DTOs;
using Microsoft.Data.SqlClient;

namespace Exercise5.Repository;

public interface IWarehousesRepository
{
    Task<bool> ProductNotExist(int id);
    Task<bool> WarehouseNotExist(int id);
    Task<int> OrderNotExist(AddProductWarehouse warehouse);
    Task UpdateFulfilledAt(UpdateOrder order);
    Task<int> InsertProduct_Warehouse(AddProductWarehouse productWarehouse, UpdateOrder order, GetProduct product);
}

public class WarehousesRepository : IWarehousesRepository
{
    private readonly IConfiguration _configuration;

    public WarehousesRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> ProductNotExist(int id)
    {
        await using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            var command = connection.CreateCommand();

            command.CommandText = "select * from Product where IdProduct = @1";

            command.Parameters.AddWithValue("@1", id);

            await connection.OpenAsync();

            if (await command.ExecuteScalarAsync() is not null)
            {
                return false;
            }

            return true;
        }
    }
    public async Task<bool> WarehouseNotExist(int id)
    {
        await using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            var command = connection.CreateCommand();

            command.CommandText = "select * from Warehouse where IdWarehouse = @1";

            command.Parameters.AddWithValue("@1", id);

            await connection.OpenAsync();

            if (await command.ExecuteScalarAsync() is not null)
            {
                return false;
            }

            return true;
        }
    }
    
    //Jako, że sprawdziliśmy czy Warehouse istnieje - to możemy zrobić to na obiekcie Warehouse
    public async Task<int> OrderNotExist(AddProductWarehouse productWarehouse)
    {
        await using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            var command = connection.CreateCommand();

            command.CommandText = "select IdOrder from [Order] where IdProduct = @1 " +
                                  "and Amount = @2 and FulfilledAt IS NULL " +
                                  "and CreatedAt<@3";

            command.Parameters.AddWithValue("@1", productWarehouse.IdProduct);
            command.Parameters.AddWithValue("@2", productWarehouse.Amount);
            command.Parameters.AddWithValue("@3", productWarehouse.CreatedAt);

            await connection.OpenAsync();

            var result = await command.ExecuteScalarAsync();
            if (result is not null)
            {
                return (int)result;
            }

            return -1;
        }
    }
    public async Task UpdateFulfilledAt(UpdateOrder order)
    {
        await using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            var command = connection.CreateCommand();

            command.CommandText = "update [Order] set FulfilledAt = @1 where IdOrder = @2";

            command.Parameters.AddWithValue("@1", DateTime.UtcNow);
            command.Parameters.AddWithValue("@2", order.IdOrder);
            
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }
    
    public async Task<decimal> GetPrice(GetProduct product)
    {
        await using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            var command = connection.CreateCommand();

            command.CommandText = "select Price from Product where IdProduct = @1";

            command.Parameters.AddWithValue("@1", product.IdProduct);

            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();
            if (result is not null)
            {
                return (decimal)result;
            }

            throw new Exception("Cena nie została znaleziona!");
        }
    }
    
    
    
    public async Task<int> InsertProduct_Warehouse(AddProductWarehouse productWarehouse, UpdateOrder order, GetProduct product)
    {
        await using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            var command = connection.CreateCommand();

            command.CommandText = "insert into Product_Warehouse(idwarehouse, idproduct, idorder, amount, price, createdat) " +
                                  "values (@1,@2,@3,@4,@5,@6);" +
                                  "SELECT SCOPE_IDENTITY();";

            decimal cena = await GetPrice(product);

            command.Parameters.AddWithValue("@1", productWarehouse.IdWarehouse);
            command.Parameters.AddWithValue("@2", productWarehouse.IdProduct);
            command.Parameters.AddWithValue("@3", order.IdOrder);
            command.Parameters.AddWithValue("@4", productWarehouse.Amount);
            command.Parameters.AddWithValue("@5", productWarehouse.Amount * cena);
            command.Parameters.AddWithValue("@6", DateTime.UtcNow);

            await connection.OpenAsync();
            int kluczGlowny = Convert.ToInt32(await command.ExecuteScalarAsync());

            return kluczGlowny;
        }
    }

}