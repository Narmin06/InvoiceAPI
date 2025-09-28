using InvoicerAPI.Entity;
namespace InvoicerAPI.Models;

public class User : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; } 
    public string Email { get; set; } 
    public string Password { get; set; } 
    public string? PhoneNumber { get; set; }
    public ICollection<Invoice> Invoicers { get; set; } = new List<Invoice>();
}
