using InvoicerAPI.Entity;
using InvoicerAPI.Enums;
namespace InvoicerAPI.Models;

public class Invoice :BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public decimal TotalSum { get; set; }
    public string? Comment { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public InvoiceStatus Status { get; set; } = InvoiceStatus.Created;
    public ICollection<InvoiceRow> Rows { get; set; } = new List<InvoiceRow>();
} 
  