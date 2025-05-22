using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CW9_S30391.Models;

[Table("Patient")]
public class Patient
{
    [Key]
    public int IdPatient { get; set; }

    [MaxLength(100)] 
    public string FirstName { get; set; } = null!;
    
    [MaxLength(100)] 
    public string LastName { get; set; } = null!;
    
    public DateTime Birthdate { get; set; }
    
    public virtual ICollection<Perscription> Perscriptions { get; set; } = null!;
}