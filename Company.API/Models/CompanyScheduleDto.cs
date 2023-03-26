using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Company.API.Models;

public class CompanyScheduleDto
{
    [Key] [JsonPropertyName("id")] public string Id { get; internal set; } = Guid.NewGuid().ToString();

    [JsonIgnore] public List<NotificationDto> Notifications { get; set; } = new();

    [NotMapped]
    [JsonPropertyName("notifications")]
    public List<string> NotificationsList => Notifications.Select(x => x.DateTime.ToString("dd/MM/yyyy")).ToList();
}