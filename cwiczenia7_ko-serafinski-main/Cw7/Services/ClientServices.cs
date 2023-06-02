using Cw7.Models;
using Microsoft.EntityFrameworkCore;

namespace Cw7.Services;

public interface IClientServices
{
    Task<bool> HasAssignedTours(int idClient);
    Task<bool> DeleteClient(int idClient);
}


public class ClientServices : IClientServices
{
    private readonly MasterContext _context;

    public ClientServices(MasterContext context)
    {
        _context = context;
    }

    public async Task<bool> HasAssignedTours(int idClient)
    {
        //To chyba też może być?
        /*var test = await _context.ClientTrips.Where(e => e.IdClient == idClient).AnyAsync();*/
        return await _context.ClientTrips.AnyAsync(e => e.IdClient == idClient);
    }
    
    public async Task<bool> DeleteClient(int idClient)
    {
        //To chyba też może być?
        /*var client = await _context.Clients.FindAsync(idClient);*/
        var client = await _context.Clients.Where(e => e.IdClient == idClient).FirstOrDefaultAsync();
        
        
        if (client == null)
        {
            return false;
        }
        
        //Alternatywa na usuwanie
        //await _context.Clients.Where(e => e.IdClient == idClient).ExecuteDeleteAsync();
        
        //Zmiana stanu
        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
        return true;
    }
}