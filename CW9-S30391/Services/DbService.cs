using CW9_S30391.Data;
using CW9_S30391.DTOs;
using CW9_S30391.Exceptions;
using CW9_S30391.Models;
using Microsoft.EntityFrameworkCore;

namespace CW9_S30391.Services;

public interface IDbService
{
    public Task<PrescriptionGetDto> CreatePerscriptionAsync(PrescriptionCreateDto prescriptionData);
}

public class DbService(AppDbContext data) : IDbService
{
    public async Task<PrescriptionGetDto> CreatePerscriptionAsync(PrescriptionCreateDto prescriptionData)
    {
        

        if (prescriptionData.Medicaments.Count > 10)
        {
            throw new NotFoundException("Prescription can't have more than 10 medicaments");
        }

        if (prescriptionData.Date >= prescriptionData.DueDate)
        {
            throw new NotFoundException("Expired date");
        }

        var doctor = await data.Doctors.FirstOrDefaultAsync(d => d.IdDoctor == prescriptionData.IdDoctor);
        if (doctor == null)
        {
            throw new NotFoundException($"Doctor of id {prescriptionData.IdDoctor} not found");
        }
        
        var patient = await data.Patients.FirstOrDefaultAsync(p=>p.IdPatient == prescriptionData.Patient.IdPatient);
        if (patient == null)
        {
            patient = new Patient
            {
                FirstName = prescriptionData.Patient.FirstName,
                LastName = prescriptionData.Patient.LastName,
                Birthdate = prescriptionData.Patient.Birthdate
            };
            await data.Patients.AddAsync(patient);
            await data.SaveChangesAsync();
        }

        var medicamentIds = prescriptionData.Medicaments.Select(m => m.IdMedicament).ToList();
        var medicaments = await data.Medicaments
            .Where(m => medicamentIds.Contains(m.IdMedicament))
            .ToListAsync();

        if (medicaments.Count != prescriptionData.Medicaments.Count)
            throw new NotFoundException("One or more medicaments not found");
        
        var prescriptionMedicaments = prescriptionData.Medicaments.Select(m => new PrescriptionMedicament
        {
            IdMedicament = m.IdMedicament,
            Dose = m.Dose,
            Details = m.Description
        }).ToList();
        
        var prescription = new Prescription
        {
            Date = prescriptionData.Date,
            DueDate = prescriptionData.DueDate,
            IdPatient = patient.IdPatient,
            IdDoctor = doctor.IdDoctor,
            PrescriptionMedicaments = prescriptionMedicaments
        };
        await data.Prescriptions.AddAsync(prescription);
        await data.SaveChangesAsync();
            
        return new PrescriptionGetDto
        {
            IdPrescription = prescription.IdPrescription,
            Date = prescription.Date,
            DueDate = prescription.DueDate,
            Doctor = new DoctorGetDto
            {
                IdDoctor = doctor.IdDoctor,
                FirstName = doctor.FirstName
            },
            Medicaments = prescriptionData.Medicaments.Select(d=>
            {
                var name = medicaments.FirstOrDefault(m => m.IdMedicament == d.IdMedicament)?.Name;
                if (name != null)
                    return new MedicamentDetailsGetDto()
                    {
                        IdMedicament = d.IdMedicament,
                        Dose = d.Dose,
                        Description = d.Description,
                        Name = name
                    };
                throw new InvalidOperationException();
            }).ToList()
        };
    }
}