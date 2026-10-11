using System.ComponentModel.DataAnnotations;

public class RegisterDTO
{
    [Required]
    public string UserName { get; set; }

    [Required]
    [MinLength(8)]
    public string Password { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public string? Phone { get; set; }
}