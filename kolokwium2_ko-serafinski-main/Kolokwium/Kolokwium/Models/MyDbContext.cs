using Microsoft.EntityFrameworkCore;

namespace Kolokwium.Models;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions options) : base(options)
    {
    }
    
    //Rzutowanie tabelek
    public DbSet<Client> Clients { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductOrder> ProductOrder { get; set; }
    public DbSet<Status> Status { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        var clients = new List<Client>
        {
            new Client
            {
                Id = 1,
                FirstName = "Jan",
                LastName = "Kowalski",
            }
        };
        
        var orders = new List<Order>
        {
            new Order
            {
                Id = 1,
                CreatedAt = DateTime.Now,
                FulfilledAt = DateTime.Now,
                ClientId = 1,
                StatusId = 1
            },
            new Order
            {
                Id=2,
                CreatedAt = DateTime.Now,
                ClientId = 1,
                StatusId = 2
            }
        };

        var products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "Gniazdko Poznanskie",
                Price = 11.2
            },
            new Product
            {
                Id = 2,
                Name = "Paczek",
                Price = 10.2
            }
        };

        var productOrders = new List<ProductOrder>
        {
            new ProductOrder
            {
                OrderId = 1,
                ProductId = 2,
                Amount = 3
            },
            new ProductOrder
            {
                OrderId = 2,
                ProductId = 1,
                Amount = 5
            }
        };
        
        var statuses = new List<Status>
        {
            new Status
            {
                Id = 1,
                Name = "Created"
            },
            new Status
            {
                Id = 2,
                Name = "Finished"
            }
        };
        
        modelBuilder.Entity<Client>().HasData(clients);
        modelBuilder.Entity<Order>().HasData(orders);
        modelBuilder.Entity<Product>().HasData(products);
        modelBuilder.Entity<ProductOrder>().HasData(productOrders);
        modelBuilder.Entity<Status>().HasData(statuses);
        
    }
}