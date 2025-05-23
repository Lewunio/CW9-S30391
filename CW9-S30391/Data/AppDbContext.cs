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
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     base.OnModelCreating(modelBuilder);
    //     
    //     
    //     
    //     
    //     modelBuilder.Entity<PerscriptionMedicament>()
    // }
}