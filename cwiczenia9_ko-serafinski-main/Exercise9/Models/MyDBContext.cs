using Microsoft.EntityFrameworkCore;

namespace Exercise8.Models;

public class MyDbContext : DbContext
{
	//Konstruktor bazowy
    public MyDbContext(DbContextOptions options) : base(options)
    {
    }
    
    // Rzutowanie tabelek na listy
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    // Dodatkowo dodaj nową migrację, która pozwoli na zapisanie użytkownika w bazie danych.
    public DbSet<User> Users { get; set; }


    //Opis za pomocą FluentAPI -> jest to bezpieczniejsze podejście - trudniej się walnąć pisząc to w taki sposób.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //DOCTOR
        modelBuilder.Entity<Doctor>(e =>
        {
            //Klucz główny
            e.HasKey(k => k.IdDoctor);
            //Ilość znaków
            e.Property(k => k.FirstName).HasMaxLength(100).IsRequired();
            e.Property(k => k.LastName).HasMaxLength(100).IsRequired();
            e.Property(k => k.Email).HasMaxLength(100).IsRequired();
            
            //Seed'owanie bazy danych przykładowymi danymi
            e.HasData(new List<Doctor>
            {
                new Doctor
                {
                    IdDoctor = 1,
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    Email = "jan@kowalski.com"
                },
                new Doctor
                {
                    IdDoctor = 2,
                    FirstName = "Adam",
                    LastName = "Nowak",
                    Email = "adam@nowak.com"
                },
                new Doctor
                {
                    IdDoctor = 3,
                    FirstName = "Tomasz",
                    LastName = "Serafinski",
                    Email = "tomasz@serafinski.com"
                }
            });
        });
        
        //PATIENT
        modelBuilder.Entity<Patient>(e =>
        {
            //Klucz główny
            e.HasKey(k => k.IdPatient);
            //Ilość znaków
            e.Property(k => k.FirstName).HasMaxLength(100).IsRequired();
            e.Property(k => k.LastName).HasMaxLength(100).IsRequired();
            //Nic nie ma wiec tylko że jest wymagana!
            e.Property(k => k.BirthDate).IsRequired();
            
            //Seed'owanie bazy danych przykładowymi danymi
            e.HasData(new List<Patient>
            {
                new Patient
                {
                    IdPatient = 1,
                    FirstName = "Janusz",
                    LastName = "Januszewski",
                    BirthDate = DateTime.Now
                },
                new Patient
                {
                    IdPatient = 2,
                    FirstName = "Alina",
                    LastName = "Alinowska",
                    BirthDate = DateTime.Now
                },
                new Patient
                {
                    IdPatient = 3,
                    FirstName = "Tomasz",
                    LastName = "Tomaszewski",
                    BirthDate = DateTime.Now
                }
            });
        });
        
        //MEDICAMENT
        modelBuilder.Entity<Medicament>(e =>
        {
            //Klucz główny
            e.HasKey(k => k.IdMedicament);
            //Ilość znaków
            e.Property(k => k.Name).HasMaxLength(100).IsRequired();
            e.Property(k => k.Description).HasMaxLength(100).IsRequired();
            e.Property(k => k.Type).HasMaxLength(100).IsRequired();
            
            //Seed'owanie bazy danych przykładowymi danymi
            e.HasData(new List<Medicament>
            {
                new Medicament
                {
                    IdMedicament = 1,
                    Name = "Ibuprofen",
                    Description = "Lek na ból",
                    Type = "Przeciwbólowy"
                }
            });
        });
        
        //PRESCRIPTION
        modelBuilder.Entity<Prescription>(e =>
        {
            //Klucz główny
            e.HasKey(k => k.IdPrescription);
            //Ilość znaków
            e.Property(k => k.Date).IsRequired();
            e.Property(k => k.DueDate).IsRequired();
            
            //Połączenia między tabelami -> tak robimy FK
            //Połączenie z DOCTOR
            e.HasOne(e => e.Doctor)
                .WithMany(e => e.Prescriptions)
                .HasForeignKey(e=> e.IdDoctor)
                //Co się ma stać jak usuniemy!
                .OnDelete(DeleteBehavior.ClientCascade);
            
            //Połączenie z PATIENT
            e.HasOne(e => e.Patient)
                .WithMany(e => e.Prescriptions)
                .HasForeignKey(e => e.IdPatient)
                .OnDelete(DeleteBehavior.ClientCascade);
            
            //Seed'owanie bazy danych przykładowymi danymi
            e.HasData(new List<Prescription>
            {
                new Prescription
                {
                    IdPrescription = 1,
                    Date = DateTime.Now,
                    DueDate = DateTime.Now,
                    IdDoctor = 1,
                    IdPatient = 1
                },
                new Prescription
                {
                    IdPrescription = 2,
                    Date = DateTime.Now,
                    DueDate = DateTime.Now,
                    IdDoctor = 2,
                    IdPatient = 2
                },
                new Prescription
                {
                    IdPrescription = 3,
                    Date = DateTime.Now,
                    DueDate = DateTime.Now,
                    IdDoctor = 3,
                    IdPatient = 3
                }
            });
        });
        
        //PRESCRIPTION_MEDICAMENT
        modelBuilder.Entity<PrescriptionMedicament>(e =>
        {
            // Klucz anonimowy -> tak robimy jak mamy FK i PK na jednym atrybucie?
            e.HasKey(k => new { k.IdMedicament, k.IdPrescription });
            
            //Połączenia między tabelami -> tak robimy FK
            //Połączenie z MEDICAMENT
            e.HasOne(e => e.Medicament)
                .WithMany(e => e.PrescriptionMedicaments)
                .HasForeignKey(e => e.IdMedicament)
                .OnDelete(DeleteBehavior.ClientCascade);;
            
            //Połączenie z PRESCRIPTION
            e.HasOne(e => e.Prescription)
                .WithMany(e => e.PrescriptionMedicaments)
                .HasForeignKey(e => e.IdPrescription)
                .OnDelete(DeleteBehavior.ClientCascade);
            
            //NIE DAJEMY TUTAJ POLA KTÓRE JEST NULLOWALNE
            e.Property(k => k.Details).IsRequired();
            
            //Seed'owanie bazy danych przykładowymi danymi
            e.HasData(new List<PrescriptionMedicament>
            {
                new PrescriptionMedicament
                {
                    IdMedicament = 1,
                    IdPrescription = 1,
                    Dose = 200,
                    Details = "2x dziennie"
                },
                new PrescriptionMedicament
                {
                    IdMedicament = 1,
                    IdPrescription = 2,
                    Details = "Brać doraźnie"
                },
                new PrescriptionMedicament
                {
                    IdMedicament = 1,
                    IdPrescription = 3,
                    Dose = 400,
                    Details = "Max. 4 razy dziennie!"
                }
            });
        });
        //Po każdej modyfikacji pliku trzeba zrobić migracje i update bazy!
        //Pamiętać o odświeżeniu bazy będą na dbo/@localhost
        
        // Tworzymy migracje za pomocą
        // dotnet ef migrations add Init
        // ALE musimy znajdować się w folderze niżej z naszą solucją - w tym wypadku Exercise 8 
        
        // Aktualizujemy bazę danych za pomocą
        // dotnet ef database update
        
        //Jak coś zrobimy z _EFMigrations History -> to najprostszym sposobem na ogarnięcie tego jest
        //usunięcie wszystkich tabelek i foldera migracji i odpalenie jeszcze raz tworzenia migracji
    }
}