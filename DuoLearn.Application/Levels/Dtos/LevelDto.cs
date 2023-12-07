using System.ComponentModel.DataAnnotations;

namespace DuoLearn.Application;

public class CreateLevelDto
{
    public int SectionId { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public string? ImageUrl { get; set; }
    public required string Type { get; set; }
    public required int Order { get; set; }
    public required bool Enabled { get; set; }
}  


public class UpdateLevelDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public string? Type { get; set; }
    public int? Order { get; set; }
    public bool? Enabled { get; set; }
}
