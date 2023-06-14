using Exercise8.Models;
using Exercise8.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Exercise8.Services;

public interface IPrescriptionService
{
    Task<GetPrescription?> GetPrescription(int id);
}

public class PrescriptionService : IPrescriptionService
{
    private readonly MyDbContext _dbContext;

    public PrescriptionService(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetPrescription?> GetPrescription(int id)
    {
        var prescription = await _dbContext.Prescriptions
            //Włączenie powiązanych tabel
            .Include(p => p.Doctor)
            .Include(p => p.Patient)
            .Include(p => p.PrescriptionMedicaments)
            .ThenInclude(pm => pm.Medicament)
            //Zwraca pierwszą receptę która spełnia wymagania
            .FirstOrDefaultAsync(p=>p.IdPrescription == id);

        //Jeżeli recepta nie isntnieje
        if (prescription == null)
        {
            return null;
        }
        
        //Wypełnienie danymi z DTOs
        var getPrescription = new GetPrescription
        {
            IdPrescription = prescription.IdPrescription,
            Date = prescription.Date,
            DueDate = prescription.DueDate,
            //Tworzenie nowego obiektu Doctor z danymi z prescription
            Doctor = new DoctorDto
            {
                IdDoctor = prescription.Doctor.IdDoctor,
                FirstName = prescription.Doctor.FirstName,
                LastName = prescription.Doctor.LastName,
                Email = prescription.Doctor.Email
            },
            //Tworzenie nowego obiektu Patient z danymi z prescription
            Patient = new PatientDto
            {
                IdPatient = prescription.Patient.IdPatient,
                FirstName = prescription.Patient.FirstName,
                LastName = prescription.Patient.LastName,
                BirthDate = prescription.Patient.BirthDate
            },
            //Tworzenie nowego PrescriptionMedicaments z użyciem LINQ
            PrescriptionMedicaments = prescription.PrescriptionMedicaments.Select(pm => new PrescriptionMedicamentDto
            {
                Dose = pm.Dose,
                Details = pm.Details,
                Medicament = new MedicamentDto
                {
                    IdMedicament = pm.Medicament.IdMedicament,
                    Name = pm.Medicament.Name,
                    Description = pm.Medicament.Description,
                    Type = pm.Medicament.Type
                }
            }).ToList()
        };
        return getPrescription;
    }
}