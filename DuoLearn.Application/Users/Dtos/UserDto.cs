using System.ComponentModel.DataAnnotations;

namespace DuoLearn.Application;

public class UserDto
{
    [Required]
    [StringLength(40)]
    public string Name { get; set; } = null!;

    [EmailAddress]
    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;

    public string? ProfileImage { get; set; } = null!;

    [Required]
    public DateOnly? DateOfBirth { get; set; } = null!;
}
