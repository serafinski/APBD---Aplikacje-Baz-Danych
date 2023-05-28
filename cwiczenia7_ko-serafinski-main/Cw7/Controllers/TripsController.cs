using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cw7.Models;
using Cw7.Models.DTOs;
using Cw7.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cw7.Controllers
{
    [Route("api/trips")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripsServices _tripsService;

        public TripsController(ITripsServices tripsServices)
        {
            _tripsService = tripsServices;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var trips = await _tripsService.GetAllTripsAsync();
            return Ok(trips);
        }

        [HttpPost("{idTrip}/clients")]
        public async Task<IActionResult> AddClientToTrip(int idTrip, AddClientToTrip clientToTrip)
        {
            if (idTrip != clientToTrip.TripID)
            {
                return BadRequest("Wycieczka o podanym ID nie istnieje!");
            }

            var addClient = await _tripsService.AssignClientToTrip(clientToTrip);

            if (addClient)
            {
                return Ok();
            }

            return BadRequest("Klient jest już zapisany na podaną wycieczkę!");
        }
        
    }
}
