using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kolokwium1.Models;
using Kolokwium1.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium1.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class OrderControler : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IClientsRepository _clientsRepository;

        public OrderControler(IConfiguration configuration, IClientsRepository clientsRepository)
        {
            _configuration = configuration;
            _clientsRepository = clientsRepository;
        }
        
        [HttpGet("{clientId}/orders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetClientOrders(int clientId)
        {
            var orders = await _clientsRepository.GetClientOrders(clientId);
            
            if (orders == null)
            {
                return NotFound();
            }

            return Ok(orders);
        }
        
        [HttpPost("{clientId}/orders")]
        public async Task<IActionResult> AddClientOrder(int clientId, OrderRequest orderRequest)
        {
            var orderId = await _clientsRepository.AddClientOrder(clientId, orderRequest);

            if (orderId == null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetClientOrders), new { clientId = clientId }, orderId);
        }

    }
    
}
