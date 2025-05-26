using CW9_S30391.Models;
using Microsoft.EntityFrameworkCore;

namespace CW9_S30391.Data;

public class AppDbContext : DbContext
{
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    //dodawianie danych recznie
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Patient>().HasData(
            new Patient { IdPatient = 1, FirstName = "Anna", LastName = "Kowalska", Birthdate = new DateTime(1990, 1, 1) },
            new Patient { IdPatient = 2, FirstName = "Jan", LastName = "Nowak", Birthdate = new DateTime(1985, 5, 15) }
        );

        modelBuilder.Entity<Doctor>().HasData(
            new Doctor { IdDoctor = 1, FirstName = "Marek", LastName = "Zieliński", Email = "m.zielinski@clinic.com" },
            new Doctor { IdDoctor = 2, FirstName = "Ewa", LastName = "Bąk", Email = "e.bak@clinic.com" }
        );

        modelBuilder.Entity<Medicament>().HasData(
            new Medicament { IdMedicament = 1, Name = "Ibuprofen", Description = "Pain reliever", Type = "Tablet" },
            new Medicament { IdMedicament = 2, Name = "Amoxicillin", Description = "Antibiotic", Type = "Capsule" }
        );

        modelBuilder.Entity<Prescription>().HasData(
            new Prescription { IdPrescription = 1, Date = new DateTime(2024, 5, 1), DueDate = new DateTime(2024, 6, 1), IdDoctor = 1, IdPatient = 1 },
            new Prescription { IdPrescription = 2, Date = new DateTime(2024, 5, 5), DueDate = new DateTime(2024, 6, 5), IdDoctor = 2, IdPatient = 2 }
        );

        modelBuilder.Entity<PrescriptionMedicament>().HasData(
            new PrescriptionMedicament { IdMedicament = 1, IdPrescription = 1, Dose = 200, Details = "Twice a day" },
            new PrescriptionMedicament { IdMedicament = 2, IdPrescription = 2, Dose = 500, Details = "Three times a day" }
        );
    }

}