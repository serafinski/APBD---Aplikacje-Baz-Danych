using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Exercise5.Models.DTOs;
using Exercise5.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Exercise5.Controllers
{
    [Route("api/warehouses2")]
    [ApiController]
    public class Warehouses2Controller : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWarehouses2Repository _warehouses2Repository;

        public Warehouses2Controller(IConfiguration configuration, IWarehouses2Repository warehouses2Repository)
        {
            _configuration = configuration;
            _warehouses2Repository = warehouses2Repository;
        }

        [HttpPost]
        public async Task<int> AddProductWarehouse(AddProductWarehouse newProductWarehouse)
        {
            return await _warehouses2Repository.InsertProduct_Warehouse(newProductWarehouse);
        }
    }
}
