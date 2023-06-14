using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Kolokwium.Models;
using Kolokwium.Models.DTOs;
using Kolokwium.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddOrderController : ControllerBase
    {
        private readonly IDbService _dbService;
        private readonly MyDbContext _myDbContext;

        public AddOrderController(IDbService dbService, MyDbContext myDbContext)
        {
            _dbService = dbService;
            _myDbContext = myDbContext;
        }
        
        [HttpPost("{clientId}/orders")]
        public async Task<IActionResult> AddOrder(int clientId, AddClientOrder data)
        {
            if (!await _dbService.DoesClientExist(clientId))
                return NotFound();

            if (!await _dbService.DoesStatusExist())
                return NotFound();

            var products = new List<ProductOrder> { };
            foreach (var product in data.GetProductsForOrders)
            {

                products.Add(new ProductOrder
                {
                    ProductId = product.Id,
                    Amount = product.Amount
                });
            }

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var order = await _dbService.AddOrder(new Order
                {
                    CreatedAt = data.CreatedAt,
                    FulfilledAt = null,
                    ClientId = clientId,
                    StatusId = 1
                });

                products.ForEach(p => p.Order = order);

                await _dbService.AddProductOrders(products);


                scope.Complete();
            }

            return Created("", "");
        }
    }
}
