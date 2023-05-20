using Kolokwium1.Models;
using Microsoft.Data.SqlClient;

namespace Kolokwium1.Repository;

public interface IClientsRepository
{
    Task<IEnumerable<Order>> GetClientOrders(int clientId);
    Task<int?> AddClientOrder(int clientId, OrderRequest orderRequest);
}


public class ClientsRepository : IClientsRepository
{
    private readonly IConfiguration _configuration;


    public ClientsRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    
    public async Task<IEnumerable<Order>> GetClientOrders(int clientId)
    {
        var orders = new List<Order>();

        await using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            var command = connection.CreateCommand();

            command.CommandText = "SELECT c.ID as ClientID, o.ID as OrderID, o.CREATEDAT as CreatedAt, s.NAME as Status, p.NAME as ProductName, po.AMOUNT as Amount " +
                                  "FROM [Order] o " +
                                  "JOIN STATUS s ON o.STATUS_ID = s.ID " +
                                  "JOIN PRODUCT_ORDER po ON po.ORDER_ID = o.ID " +
                                  "JOIN PRODUCT p ON p.ID = po.PRODUCT_ID " +
                                  "JOIN CLIENT c ON c.ID = o.CLIENT_ID " +
                                  "WHERE o.CLIENT_ID = @ClientId";

            command.Parameters.AddWithValue("@ClientId", clientId);

            await connection.OpenAsync();
            using (var reader = await command.ExecuteReaderAsync())
            {
                if (!reader.HasRows)
                {
                    return null;
                }

                while (await reader.ReadAsync())
                {
                    int orderId = reader.GetInt32(1);
                    DateTime createdAt = reader.GetDateTime(2);
                    string status = reader.GetString(3);
                    string productName = reader.GetString(4);
                    int amount = reader.GetInt32(5);

                    var order = orders.FirstOrDefault(o => o.OrderID == orderId);

                    if (order == null)
                    {
                        order = new Order
                        {
                            OrderID = orderId,
                            CreatedAt = createdAt,
                            Status = status,
                            Products = new List<Product>()
                        };
                        orders.Add(order);
                    }

                    order.Products.Add(new Product { Name = productName, Amount = amount });
                }
            }
        }

        return orders;
    }
    
    public async Task<int?> AddClientOrder(int clientId, OrderRequest orderRequest)
    {
        int? newOrderId = null;

        await using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            await connection.OpenAsync();
            
            var clientCheckCommand = connection.CreateCommand();
            clientCheckCommand.CommandText = "SELECT COUNT(*) FROM CLIENT WHERE ID = @ClientId";
            clientCheckCommand.Parameters.AddWithValue("@ClientId", clientId);
            int clientCount = Convert.ToInt32(await clientCheckCommand.ExecuteScalarAsync());

            if (clientCount == 0)
            {
                return null;
            }

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var command = connection.CreateCommand();
                    command.Transaction = transaction;

                    command.CommandText = "INSERT INTO [Order] (CLIENT_ID, CREATEDAT, STATUS_ID) " +
                                          "VALUES (@ClientId, @CreatedAt, @StatusId); " +
                                          "SELECT SCOPE_IDENTITY();";

                    command.Parameters.AddWithValue("@ClientId", clientId);
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.UtcNow);
                    command.Parameters.AddWithValue("@StatusId", 1); // Assuming 'Created' status has ID 1

                    newOrderId = Convert.ToInt32(await command.ExecuteScalarAsync());

                    foreach (var product in orderRequest.Products)
                    {
                        command = connection.CreateCommand();
                        command.Transaction = transaction;

                        command.CommandText = "INSERT INTO PRODUCT_ORDER (ORDER_ID, PRODUCT_ID, AMOUNT) " +
                                              "VALUES (@OrderId, @ProductId, @Amount);";

                        command.Parameters.AddWithValue("@OrderId", newOrderId);
                        command.Parameters.AddWithValue("@ProductId", product.Id);
                        command.Parameters.AddWithValue("@Amount", product.Amount);

                        await command.ExecuteNonQueryAsync();
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        return newOrderId;
    }

}