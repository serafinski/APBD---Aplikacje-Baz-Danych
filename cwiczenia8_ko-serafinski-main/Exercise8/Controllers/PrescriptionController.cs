using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exercise8.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exercise8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;

        public PrescriptionController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrescription(int id)
        {
            //Pobieramy receptę po id
            var prescription = await _prescriptionService.GetPrescription(id);
            
            //Jeżeli recepta nie istnieje
            if (prescription == null)
            {
                return NotFound();
            }
            
            //Jak recepta istnieje 
            return Ok(prescription);
        }
    }
}
