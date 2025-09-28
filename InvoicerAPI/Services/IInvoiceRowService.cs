using InvoicerAPI.DTOs.InvoiceRowDTOs;


namespace InvoicerAPI.Services;

public interface IInvoiceRowService
{
    Task<InvoiceRowDTO> CreateAsync(CreateInvoiceRowDTO dto);
    Task<InvoiceRowDTO> UpdateAsync(Guid id, UpdateInvoiceRowDTO dto);
    Task<bool> DeleteAsync(Guid id);
    Task<InvoiceRowDTO> GetByIdAsync(Guid id);
    Task<IEnumerable<InvoiceRowDTO>> GetByInvoiceIdAsync(Guid invoiceId);
}
