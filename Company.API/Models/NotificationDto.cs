using System.ComponentModel.DataAnnotations;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Company.API.Models;

public class NotificationDto
{
    [Key] public string? Id { get; set; }

    public DateTime DateTime { get; set; }
}