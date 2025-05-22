using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CW9_S30391.Models;

[Table("Perscription_Medicament")]
[PrimaryKey(nameof(IdMedicament), nameof(IdPerscription))]
public class PerscriptionMedicament
{
    public int IdMedicament { get; set; }
    
    public int IdPerscription { get; set; }
    
    public int? Dose { get; set; }
    
    [MaxLength(100)]
    public string Details { get; set; } = null!;
    
    [ForeignKey(nameof(IdMedicament))]
    public virtual Medicament Medicament { get; set; } = null!;
    
    [ForeignKey(nameof(IdPerscription))]
    public virtual Perscription Perscription { get; set; } = null!;
}