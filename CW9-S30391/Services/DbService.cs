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
        List<PrescriptionMedicament> prescriptionMedicaments = [];
        List<Medicament> medicaments = [];
        var prescriptionId = (await data.Prescriptions.MaxAsync(p=>p.IdPrescription))+1;

        foreach (var medicament in prescriptionData.Medicaments)
        {
            var medicamentDb = await data.Medicaments.FirstOrDefaultAsync(m=>m.IdMedicament == medicament.IdMedicament);
            if (medicamentDb == null)
            {
                throw new NotFoundException($"Medicament of id {medicament.IdMedicament} not found");
            }
            medicaments.Add(medicamentDb);
            prescriptionMedicaments.Add(new PrescriptionMedicament
            {
                IdPrescription = prescriptionId,
                IdMedicament = medicament.IdMedicament,
                Dose = medicament.Dose,
                Details = medicament.Description
            });
        }

        if (prescriptionMedicaments.Count > 10)
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
                IdPatient = prescriptionData.Patient.IdPatient,
                FirstName = prescriptionData.Patient.FirstName,
                LastName = prescriptionData.Patient.LastName,
                Birthdate = prescriptionData.Patient.Birthdate
            };
            await data.Patients.AddAsync(patient);
        }

        var prescription = new Prescription
        {
            IdPrescription = prescriptionId,
            Date = prescriptionData.Date,
            DueDate = prescriptionData.DueDate,
            IdPatient = patient.IdPatient,
            IdDoctor = doctor.IdDoctor,
            Patient = patient,
            Doctor = doctor,
            PrescriptionMedicaments = prescriptionMedicaments

        };
        await data.Prescriptions.AddAsync(prescription);
        await data.SaveChangesAsync();
            
        return new PrescriptionGetDto
        {
            IdPrescription = prescriptionId,
            Date = prescription.Date,
            DueDate = prescription.DueDate,
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
            }).ToList(),
            Doctor = new DoctorGetDto
            {
                IdDoctor = doctor.IdDoctor,
                FirstName = doctor.FirstName
            }
        };
    }
}