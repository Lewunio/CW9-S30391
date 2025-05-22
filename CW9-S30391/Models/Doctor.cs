using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CW9_S30391.Models;

[Table("Doctor")]
public class Doctor
{
    [Key]
    public int IdDoctor { get; set; }

    [MaxLength(100)] 
    public string FirstName { get; set; } = null!;
    
    [MaxLength(100)] 
    public string LastName { get; set; } = null!;
    
    [MaxLength(100)] 
    public string Email { get; set; } = null!;
    
    public virtual ICollection<Perscription> Perscriptions { get; set; } = null!;
}