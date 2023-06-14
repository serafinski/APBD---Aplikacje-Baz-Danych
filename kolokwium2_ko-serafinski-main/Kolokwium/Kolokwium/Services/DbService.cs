using Kolokwium.Models;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium.Services
{
    public interface IDbService
    {
        public Task<bool> DoesClientExist(int clientId);
        public Task<bool> DoesStatusExist();
        public Task<ICollection<Order>> GetOrdersData(int clientId);
        public Task<Product?> GetProduct(int productId);
        public Task<Order> AddOrder(Order newOrder);
        public Task AddProductOrders(ICollection<ProductOrder> products);
    }

    public class DbService : IDbService
    {
        private readonly MyDbContext _myDbContext;

        public DbService(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public async Task<bool> DoesClientExist(int clientId)
        {
            return await _myDbContext.Clients.AnyAsync(e => e.Id == clientId);
        }
        public async Task<bool> DoesStatusExist()
        {
            return await _myDbContext.Status.AnyAsync(e => e.Name == "Utworzone");
        }

        public async Task<ICollection<Order>> GetOrdersData(int clientId)
        {
            return await _myDbContext.Orders
                .Include(e => e.Client)
                .Include(e => e.Status)
                .Include(e => e.ProductOrders).ThenInclude(e => e.Product)
                .Where(e => e.Client.Id == clientId)
                .ToListAsync();
        }

        public async Task<Product?> GetProduct(int productId)
        {
            return await _myDbContext.Products.FirstOrDefaultAsync(e => e.Id == productId);
        }

        public async Task<Order> AddOrder(Order newOrder)
        {
            await _myDbContext.Orders.AddAsync(newOrder);
            await _myDbContext.SaveChangesAsync();
            return newOrder;
        }
        public async Task AddProductOrders(ICollection<ProductOrder> products)
        {
            await _myDbContext.ProductOrder.AddRangeAsync(products);
            await _myDbContext.SaveChangesAsync();
        }

    }

}
