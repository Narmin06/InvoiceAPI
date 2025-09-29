using InvoicerAPI.Data;
using InvoicerAPI.DTOs.InvoiceDTO;
using InvoicerAPI.DTOs.InvoiceRowDTOs;
using InvoicerAPI.Enums;
using InvoicerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoicerAPI.Services;

public class InvoiceService : IInvoiceService
{
    private readonly InvoicerContext _context;

    public InvoiceService(InvoicerContext context)
    {
        _context = context;
    }

    public async Task<InvoiceResponseDTO> CreateInvoice(CreateInvoiceDTO invoiceDto)
    {
        var new_invoice = new Invoice()
        {
            UserId = invoiceDto.UserId,
            Comment = invoiceDto.Comment,
            StartDate = invoiceDto.StartDate,
            EndDate = invoiceDto.EndDate,
            Status = InvoiceStatus.Created,
        };
        await _context.Invoices.AddAsync(new_invoice);
        await _context.SaveChangesAsync();

        return ChangeInvoiceDTO(new_invoice);
    }

    public async Task<InvoiceResponseDTO> EditInvoice(Guid id, UpdateInvoiceDTO invoiceDto)
    {
        var editInvoice = await _context.Invoices
            .Include(i => i.Rows)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (editInvoice == null ) 
        {
            throw new InvalidOperationException("Invoice not found !!!");
        }
        if (editInvoice.Status != InvoiceStatus.Created)
            throw new InvalidOperationException("Only invoices with status 'Created' can be deleted.");


        editInvoice.Comment = invoiceDto.Comment;
        editInvoice.StartDate = invoiceDto.StartDate;
        editInvoice.EndDate = invoiceDto.EndDate;
        editInvoice.UpdatedAt = DateTimeOffset.UtcNow;

        await _context.SaveChangesAsync();
        return ChangeInvoiceDTO(editInvoice);
    }

    public async Task<InvoiceResponseDTO> ChangeInvoiceStatus(Guid id, InvoiceStatus status)
    {
        var changeStatus = await _context.Invoices
          .Include(i => i.Rows)
          .FirstOrDefaultAsync(i => i.Id == id);

        if (changeStatus == null)
        {
            throw new InvalidOperationException("Invoice not found !!!");
        }


        if (changeStatus.Status != InvoiceStatus.Created)
            throw new InvalidOperationException("Only invoices with status 'Created' can be deleted.");


        changeStatus.Status = status;
        changeStatus.UpdatedAt = DateTimeOffset.UtcNow;
        await _context.SaveChangesAsync();
        return ChangeInvoiceDTO(changeStatus);
    }

    public async Task<bool> DeleteInvoice(Guid id)
    {
        var deleteInvoice = await _context.Invoices
                    .FirstOrDefaultAsync(i => i.Id == id);

        if (deleteInvoice == null)
        {
            throw new InvalidOperationException("Invoice not found !!!");
        }

        if (deleteInvoice.Status != InvoiceStatus.Created)
            throw new InvalidOperationException("Only invoices with status 'Created' can be deleted.");

        _context.Invoices.Remove(deleteInvoice);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ArchiveInvoice(Guid id)
    {
        var archiveInvoice = await _context.Invoices
                 .FirstOrDefaultAsync(i => i.Id == id);

        if (archiveInvoice == null)
        {
            return false;
        }

        archiveInvoice.DeletedAt = DateTimeOffset.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<InvoiceResponseDTO>> GetInvoicesList()
    {
        var invoices = await _context.Invoices
            .Include(i => i.Rows)
            .Where(i => i.DeletedAt == null)
            .ToListAsync();

        return invoices.Select(ChangeInvoiceDTO);
    }

    public async Task<InvoiceResponseDTO> GetInvoiceById(Guid id)
    {
        var invoice = await _context.Invoices
            .Include(i => i.Rows)
            .FirstOrDefaultAsync(i => i.Id == id && i.DeletedAt == null);

        if (invoice == null)
            throw new InvalidOperationException("Invoice not found.");

        return ChangeInvoiceDTO(invoice);
    }


    private static InvoiceResponseDTO ChangeInvoiceDTO(Invoice invoice)
    {
        var totalSum = invoice.Rows.Sum( r => r.Sum);
        InvoiceResponseDTO invoiceResponseDTO = new() 
        { 
            Id = invoice.Id,
            UserId = invoice.UserId,
            TotalSum = totalSum,
            Comment = invoice.Comment,
            StartDate = invoice.StartDate,
            EndDate = invoice.EndDate,
            Status = invoice.Status,
            Rows = invoice.Rows.Select(r => new InvoiceRowDTO
            {
                Id = r.Id,
                InvoiceId = r.InvoiceId,
                Service = r.Service,
                Quantity = r.Quantity,
                Amount = r.Amount,
                Sum = r.Sum
            }).ToList()
        };
        return invoiceResponseDTO;
    }
}

