using System.ComponentModel.DataAnnotations;

namespace CW9_S30391.DTOs;

public class PrescriptionCreateDto
{
    [Required]
    public PatientCreateDto Patient { get; set; } = null!;

    [Required]
    public List<MedicamentGetDto> Medicaments { get; set; } = null!;

    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public DateTime DueDate { get; set; }
    
    [Required]
    public int IdDoctor { get; set; }
    
}




