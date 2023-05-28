using System.Data;
using Exercise5.Models.DTOs;
using Microsoft.Data.SqlClient;

namespace Exercise5.Repository;

public interface IWarehouses2Repository
{
    Task<int> InsertProduct_Warehouse(AddProductWarehouse newProductWarehouse);
}

public class Warehouses2Repository : IWarehouses2Repository
{
    private readonly IConfiguration _configuration;

    public Warehouses2Repository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<int> InsertProduct_Warehouse(AddProductWarehouse newProductWarehouse)
    {
        int kluczGlowny;

        using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            await sqlConnection.OpenAsync();

            var transaction = await sqlConnection.BeginTransactionAsync();
            SqlCommand command = sqlConnection.CreateCommand();
            command.Connection = sqlConnection;
            command.Transaction = transaction as SqlTransaction;

            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "AddProductToWarehouse";

            command.Parameters.AddWithValue("@IdProduct", newProductWarehouse.IdProduct);
            command.Parameters.AddWithValue("@IdWarehouse", newProductWarehouse.IdWarehouse);
            command.Parameters.AddWithValue("@Amount", newProductWarehouse.Amount);
            command.Parameters.AddWithValue("@CreatedAt", newProductWarehouse.CreatedAt);

            kluczGlowny = Convert.ToInt32(await command.ExecuteScalarAsync());

            await transaction.CommitAsync();

            return kluczGlowny;
        }
    }
}