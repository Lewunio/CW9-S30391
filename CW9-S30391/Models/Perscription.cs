using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CW9_S30391.Models;

[Table("Perscription")]
public class Perscription
{
    [Key] public int IdPerscription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public int IdPatient { get; set; }
    public int IdDoctor { get; set; }

    [ForeignKey(nameof(IdPatient))] 
    public virtual Patient Patient { get; set; } = null!;

    [ForeignKey(nameof(IdDoctor))] 
    public virtual Doctor Doctor { get; set; } = null!;
    
    public virtual ICollection<PerscriptionMedicament> PerscriptionMedicaments { get; set; } = null!;
}