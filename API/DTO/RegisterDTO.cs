using System.ComponentModel.DataAnnotations;

namespace Api.DTO;

public class RegisterDto{
    [Required]
    public string Name { get; set; } = null!;
    
    [Required]
    public string UserName { get; set; } = null!;
    
    [Required]
    public string Password { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;

}