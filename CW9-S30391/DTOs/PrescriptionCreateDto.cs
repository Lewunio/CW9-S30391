using System.ComponentModel.DataAnnotations;

namespace CW9_S30391.DTOs;

public class PrescriptionCreateDto
{
    [Required]
    public PatientCreateDto Patient { get; set; }
    
    [Required]
    public List<MedicamentGetDto> Medicament { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public DateTime DueDate { get; set; }
    
}




