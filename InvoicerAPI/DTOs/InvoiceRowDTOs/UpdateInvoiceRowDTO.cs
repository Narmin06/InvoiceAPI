namespace InvoicerAPI.DTOs.InvoiceRowDTOs;

public class UpdateInvoiceRowDTO
{
    public string Service { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal Amount { get; set; }

}
