namespace _46Navbat.Models;

public class Service
{
    public int Id { get; set; }
    public string Name{ get; set; }
    public string Prefix { get; set; }
    public bool IsActive { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}

