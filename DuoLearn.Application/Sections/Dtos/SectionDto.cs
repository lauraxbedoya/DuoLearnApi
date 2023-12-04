using System.ComponentModel.DataAnnotations;

namespace DuoLearn.Application;

public class CreateSectionDto
{
    public int LanguageId { get; set; }
    public required string Description { get; set; }
    public required string Color { get; set; }
    public required int Order { get; set; }
    public required bool Enabled { get; set; }
}  


public class UpdateSectionDto
{
    public string? Description { get; set; }
    public string? Color { get; set; }
    public int? Order { get; set; }
    public bool? Enabled { get; set; }
}
