using Exercise8.Models;
using Exercise8.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Exercise8.Services;

public interface IDoctorServices
{
    Task<Doctor?> GetDoctor(int id);
    Task<Doctor> AddDoctor(Doctor doctor);
    Task<Doctor?> UpdateDoctor(int id, AddDoctor doctor);
    Task<bool> DeleteDoctor(int id);
}

public class DoctorServices : IDoctorServices
{
    private readonly MyDbContext _dbContext;

    public DoctorServices(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    //? -> dodane bo rzuca warning
    public async Task<Doctor?> GetDoctor(int id)
    { 
        //Znajdujemy doktora
        return await _dbContext.Doctors.FindAsync(id);
    }

    public async Task<Doctor> AddDoctor(Doctor doctor)
    {
        //Dodajemy doktora
        _dbContext.Doctors.Add(doctor);
        //Aktualizujemy bazę
        await _dbContext.SaveChangesAsync();

        return doctor;
    }

    public async Task<Doctor?> UpdateDoctor(int id, AddDoctor doctor)
    {
        //Patrzymy czy doktor istnieje
        var existingDoctor = await _dbContext.Doctors.FindAsync(id);
        
        //Jak nie istniej to nic nie zwracamy
        if (existingDoctor == null)
        {
            return null;
        }
        
        //Przypisujemy istniejącemu doktorowi nowe wartości
        existingDoctor.FirstName = doctor.FirstName;
        existingDoctor.LastName = doctor.LastName;
        existingDoctor.Email = doctor.Email;
        
        //Aktualizujemy wartości w bazie 
        _dbContext.Doctors.Update(existingDoctor);
        //Aktualizujemy bazę danych
        await _dbContext.SaveChangesAsync();

        return existingDoctor;
    }

    public async Task<bool> DeleteDoctor(int id)
    {
        //Sprawdzamy czy doktor istnieje
        var doctor = await _dbContext.Doctors.FindAsync(id);
        
        //Jak nie istnieje to zwracamy false
        if (doctor == null)
        {
            return false;
        }
        
        //Usuwamy doktora z bazy danych
        _dbContext.Remove(doctor);
        //Aktualizujemy bazę danych
        await _dbContext.SaveChangesAsync();
        
        return true;
    }
}