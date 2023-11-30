using System.ComponentModel.DataAnnotations;

namespace DuoLearn.Application;

public class CreateLanguageDto
{
    [Required]
    public required string Name { get; set; } = null!;

    public string? FlagUrl { get; set; } = null!;

}

public class UpdateLanguageDto
{
    public string Name { get; set; } = null!;

    public string? FlagUrl { get; set; } = null!;
}
