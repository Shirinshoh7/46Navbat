namespace _46Navbat.Models;
using System;

public class Ticket
{
    public int Id { get; set; }
    public string FullNumber { get; set; } 
    public string PhoneNumber { get; set; }
    public DateTime CreatedAT { get; set; } = DateTime.UtcNow;
    public string Status { get; set; }
    public int ServiceId { get; set; }
    public Service? Service { get; set; }
}

