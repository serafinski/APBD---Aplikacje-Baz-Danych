using Cw7.Models;
using Cw7.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Cw7.Services;

public interface ITripsServices
{
   Task<List<TripGetAllResponse>> GetAllTripsAsync();
   Task<bool> AssignClientToTrip(AddClientToTrip clientToTrip);
}

public class TripsServices : ITripsServices
{
   private readonly MasterContext _context;

   public TripsServices(MasterContext context)
   {
      _context = context;
   }

   public async Task<List<TripGetAllResponse>> GetAllTripsAsync()
   {
      return await _context.Trips
         // ALTERNATYWA NA ŁĄCZENIE WIELU TABELEK - mówimy programowi co ma sobie zjoin'ować
         //.Include(e => e.ClientTrips)
         // ThenInclude - przejście po tabelkach
         //.ThenInclude(e => e.IdClientNavigation)
         //.Include(e => e.IdCountries)
         .Select(e => new TripGetAllResponse
         {
            Name = e.Name,
            Description = e.Description,
            DateFrom = e.DateFrom,
            DateTo = e.DateTo,
            MaxPeople = e.MaxPeople,
            Countries = e.IdCountries
               .Select(e => new CountryGetAllRequests
               {
                  Name = e.Name
               })
               .ToList(),
            Clients = e.ClientTrips
               .Select(e => new ClientResponse
               {
                  FirstName = e.IdClientNavigation.FirstName,
                  LastName = e.IdClientNavigation.LastName
               })
               .ToList()
         }).ToListAsync();
   }

   public async Task<bool> AssignClientToTrip(AddClientToTrip clientToTrip)
   {
      //Czy klient o danym numerze PESEL istnieje.
      var client = await _context.Clients.FirstOrDefaultAsync(e => e.Pesel == clientToTrip.PESEL);
      
      //Jeśli nie, dodajemy go do bazy danych.
      if (client == null)
      {
         client = new Client
         {
            FirstName = clientToTrip.FirstName,
            LastName = clientToTrip.LastName,
            Email = clientToTrip.Email,
            Telephone = clientToTrip.Telephone,
            Pesel = clientToTrip.PESEL
         };
         _context.Clients.Add(client);
         await _context.SaveChangesAsync();
      }
      
      //Sprawdzamy czy wycieczka istnieje
      var trip = await _context.Trips.FindAsync(clientToTrip.TripID);
      
      //Jak nie istnieje
      if (trip == null)
      {
         return false;
      }
      
      //Czy klient nie jest już zapisaną na wspomnianą wycieczkę 
      var existingTrip = await _context.ClientTrips.SingleOrDefaultAsync(e => e.IdClient == client.IdClient 
                                                                              && e.IdTrip == trip.IdTrip);
      //W takim wypadku zwracamy błąd
      if (existingTrip != null)
      {
         return false;
      }
      
      //Jak wszytko jest OK to dodajemy
      var clientTrip = new ClientTrip
      {
         IdClient = client.IdClient,
         IdTrip = trip.IdTrip,
         PaymentDate = clientToTrip.PaymentDate,
         RegisteredAt = DateTime.UtcNow
      };

      _context.ClientTrips.Add(clientTrip);
      await _context.SaveChangesAsync();

      return true;
   }
}