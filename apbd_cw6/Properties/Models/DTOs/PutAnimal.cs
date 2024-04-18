using System.ComponentModel.DataAnnotations;

namespace apbd_cw6.Properties.Models.DTOs;

public class PutAnimal
{
    [Required]
    [MinLength(3)]
    [MaxLength(200)] 
    public string Name { get; set; }
    [MinLength(3)]
    [MaxLength(200)] 
    public string? Decsription { get; set; }
    [MinLength(3)]
    [MaxLength(200)] 
    public string? Category { get; set; }
    [MinLength(3)]
    [MaxLength(200)] 
    public string? Area { get; set; }
}