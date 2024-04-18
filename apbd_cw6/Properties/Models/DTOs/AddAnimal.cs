using System.ComponentModel.DataAnnotations;

namespace apbd_cw6.Properties.Models.DTOs;

public class AddAnimal
{
    //walidacja danych ktore przychodza dzieki adnotacja []
    [Required]
    [MinLength(3)]
    [MaxLength(200)]
    public string Name { get; set; }
    [MinLength(3)]
    [MaxLength(200)]
    public string? Decsription { get; set; }
}