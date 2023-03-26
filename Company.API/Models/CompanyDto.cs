using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Company.API.Models;

public class CompanyDto : CompanyScheduleDto
{
    [Required] [JsonPropertyName("name")] public string? Name { get; set; }

    [Required]
    [Range(1000000000, 9999999999)]
    [JsonPropertyName("number")]
    public long Number { get; set; }

    [Required] [JsonPropertyName("type")] public string? Type { get; set; }

    [Required]
    [JsonPropertyName("market")]
    public string? Market { get; set; }
}