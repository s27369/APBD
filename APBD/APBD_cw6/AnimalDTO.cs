using System.ComponentModel.DataAnnotations;

namespace APBD_cw6;

public class AnimalDTO
{
    public record GetAnimalsResponse(int id, string Name, string Description, string Category, string Area);
    public record GetOneAnimalResponse(int id, string Name, string Description, string Category, string Area);

    public record CreateAnimalRequest(
        [Required]
        [MaxLength(200, ErrorMessage = "Must contain less than 200 characters")]
        String Name,
        [MaxLength(200, ErrorMessage = "Must contain less than 200 characters")]
        string Desc,
        [Required]
        [MaxLength(200, ErrorMessage = "Must contain less than 200 characters")]
        string Cat,
        [Required]
        [MaxLength(200, ErrorMessage = "Must contain less than 200 characters")]
        string Area
    );
    
    public record EditAnimalRequest(
        [Required]
        [MaxLength(200, ErrorMessage = "Must contain less than 200 characters")]
        String Name,
        [MaxLength(200, ErrorMessage = "Must contain less than 200 characters")]
        string Desc,
        [Required]
        [MaxLength(200, ErrorMessage = "Must contain less than 200 characters")]
        string Cat,
        [Required]
        [MaxLength(200, ErrorMessage = "Must contain less than 200 characters")]
        string Area
    );
}