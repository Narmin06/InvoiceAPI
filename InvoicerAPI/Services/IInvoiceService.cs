using InvoicerAPI.DTOs.InvoiceDTO;
using InvoicerAPI.Enums;
namespace InvoicerAPI.Services;

public interface IInvoiceService
{
    Task<InvoiceResponseDTO> CreateInvoice(CreateInvoiceDTO invoiceDto);
    Task<InvoiceResponseDTO> EditInvoice(Guid id, UpdateInvoiceDTO invoiceDto);
    Task<InvoiceResponseDTO> ChangeInvoiceStatus(Guid id, InvoiceStatus status);
    Task<bool> DeleteInvoice(Guid id);
    Task<bool> ArchiveInvoice(Guid id);            //soft
    Task<IEnumerable<InvoiceResponseDTO>> GetInvoicesList();
    Task<InvoiceResponseDTO> GetInvoiceById(Guid id);
}
