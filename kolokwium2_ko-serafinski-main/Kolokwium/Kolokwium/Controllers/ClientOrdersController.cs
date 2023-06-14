using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kolokwium.Models;
using Kolokwium.Models.DTOs;
using Kolokwium.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientOrdersController : ControllerBase
    {
        private readonly IDbService _dbService;
        private readonly MyDbContext _myDbContext;

        public ClientOrdersController(IDbService dbService, MyDbContext myDbContext)
        {
            _dbService = dbService;
            _myDbContext = myDbContext;
        }
        
        [HttpGet("{clientId}/orders")]
        public async Task<IActionResult> GetClientsOrders(int clientId)
        {
            if (!await _dbService.DoesClientExist(clientId)){ 
                return NotFound();
            }

            var orders = await _dbService.GetOrdersData(clientId);

            return Ok(orders.Select(e => new GetClientOrder
            {
                OrderId = e.Id,
                ClientsLastName = e.Client.LastName,
                CreatedAt = e.CreatedAt,
                FulfilledAt = e.FulfilledAt,
                Status = e.Status.Name,
                Products = e.ProductOrders.Select(p => new GetProducts
                {
                    Name = p.Product.Name,
                    Price = p.Product.Price,
                    Amount = p.Amount
                }).ToList()
         
            }));
        }
    }
}
