using InvoicerAPI.DTOs.InvoiceRowDTOs;
using InvoicerAPI.Enums;
using InvoicerAPI.Models;

namespace InvoicerAPI.DTOs.InvoiceDTO;

//Response
public class InvoiceResponseDTO
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal TotalSum { get; set; }
    public string? Comment { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public List<InvoiceRowDTO> Rows { get; set; } = new();
}
