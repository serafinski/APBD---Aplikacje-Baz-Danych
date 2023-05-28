using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exercise5.Models.DTOs;
using Exercise5.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exercise5.Controllers
{
    [Route("api/warehouses")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly IWarehousesRepository _warehousesRepository;


        public WarehousesController(IConfiguration configuration, IWarehousesRepository warehousesRepository)
        {
            _configuration = configuration;
            _warehousesRepository = warehousesRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddProductWarehouse(AddProductWarehouse newProductWarehouse)
        {
            if (await _warehousesRepository.ProductNotExist(newProductWarehouse.IdProduct) 
                || await _warehousesRepository.WarehouseNotExist(newProductWarehouse.IdWarehouse))
            {
                return NotFound("Produkt lub hurtownia o danym ID nie istnieje!");
            }

            int idOrder = await _warehousesRepository.OrderNotExist(newProductWarehouse);
            if (idOrder == -1)
            {
                return NotFound("Nie ma odpowiedniego zlecenia produktu!");
            }

            GetProduct product = new GetProduct { IdProduct = newProductWarehouse.IdProduct};
            UpdateOrder order = new UpdateOrder
            {
                IdOrder = idOrder,
                IdProduct = newProductWarehouse.IdProduct,
                Amount = newProductWarehouse.Amount,
                CreatedAt = newProductWarehouse.CreatedAt
            };

            await _warehousesRepository.UpdateFulfilledAt(order);

            var kluczGlowny = await _warehousesRepository.InsertProduct_Warehouse(newProductWarehouse, order, product);
            
            return Ok(kluczGlowny);
        }
    }
    

}
