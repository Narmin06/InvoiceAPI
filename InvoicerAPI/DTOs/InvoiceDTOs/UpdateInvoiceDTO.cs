namespace InvoicerAPI.DTOs.InvoiceDTO;

public class UpdateInvoiceDTO
{
    public Guid UserId { get; set; }
    public string? Comment { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
}
