using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cw7.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cw7.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientServices _clientServices;

        public ClientsController(IClientServices clientServices)
        {
            _clientServices = clientServices;
        }

        [HttpDelete("{idClient}")]
        public async Task<IActionResult> DeleteClient(int idClient)
        {
            //Końcówka powinna najpierw sprawdzić czy klient nie posiada żadnych przypisanych wycieczek.
            bool hasAssignedTours = await _clientServices.HasAssignedTours(idClient);
            
            if (hasAssignedTours)
            {
                //Jeśli klient posiada co najmniej jedną przypisaną wycieczkę – zwracamy błąd i usunięcie nie dochodzi do skutku.
                return Conflict("Klient posiada co najmniej jedną przypisaną wycieczkę!");
            }

            bool delete = await _clientServices.DeleteClient(idClient);

            if (delete)
            {
                return Ok();
            }

            return NotFound();
        }
    }
}
