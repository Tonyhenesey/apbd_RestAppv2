using System.ComponentModel.DataAnnotations;

namespace apbd_cw6.Properties.Models.DTOs;

public class DeleteAnimal
{
    [Required]
    [MinLength(1)]
    public int IdAnimal { get; set; }
}