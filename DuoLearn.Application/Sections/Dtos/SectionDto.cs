using System.ComponentModel.DataAnnotations;

namespace DuoLearn.Application;

public class SectionDto
{
    [Required]
    public string Name { get; set; } = null!;

    public string? FlagUrl { get; set; } = null!;

}
