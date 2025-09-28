using InvoicerAPI.Entity;

namespace InvoicerAPI.Models;

public class InvoiceRow :BaseEntity
{
    public Guid InvoiceId { get; set; }
    public Invoice Invoice { get; set; }
    public string Service { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal Amount { get; set; }
    public decimal Sum { get; set; }
}
