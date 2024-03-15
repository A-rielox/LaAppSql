using System.ComponentModel.DataAnnotations;

namespace App.DTOs;

public class RegisterDto
{
    [Required] 
    public string UserName { get; set; }

    [Required] 
    public string KnownAs { get; set; }

    [Required] 
    public string City { get; set; }

    [Required] 
    public string Country { get; set; }


    [Required]
    [StringLength(12, MinimumLength = 4)]
    public string Password { get; set; }
}
  