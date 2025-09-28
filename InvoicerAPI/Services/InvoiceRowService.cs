using InvoicerAPI.Data;
using InvoicerAPI.DTOs.InvoiceDTO;
using InvoicerAPI.DTOs.InvoiceRowDTOs;
using InvoicerAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace InvoicerAPI.Services;

public class InvoiceRowService : IInvoiceRowService
{
    private readonly InvoicerContext _context;
    public InvoiceRowService(InvoicerContext context)
    {
        _context = context;
    }

    public async Task<InvoiceRowDTO> CreateAsync(CreateInvoiceRowDTO invoiceRowDTO)
    {
        var row = new InvoiceRow()
        {
            InvoiceId = invoiceRowDTO.InvoiceId,
            Service = invoiceRowDTO.Service,
            Quantity = invoiceRowDTO.Quantity,
            Amount = invoiceRowDTO.Amount,
            Sum = invoiceRowDTO.Quantity * invoiceRowDTO.Amount
        };

        _context.InvoiceRows.Add(row);

        var invoice = await _context.Invoices
            .Include(i => i.Rows)
            .FirstOrDefaultAsync(i => i.Id == invoiceRowDTO.InvoiceId);

        if (invoice != null)
        {
            invoice.TotalSum = invoice.Rows.Sum(r => r.Sum) + row.Sum;
        }
        
        await _context.SaveChangesAsync();
        return ChangeInvoiceRowDTO(row);
    }


    public async Task<InvoiceRowDTO> UpdateAsync(Guid id, UpdateInvoiceRowDTO dto)
    {
        var row = await _context.InvoiceRows.FirstOrDefaultAsync(r => r.Id == id);

        if (row == null)
        {
            throw new InvalidOperationException("InvoiceRow not found !!!");
        }

        row.Service = dto.Service;
        row.Quantity = dto.Quantity;
        row.Amount = dto.Amount;
        row.Sum = dto.Quantity * dto.Amount;

        var invoice = await _context.Invoices
            .Include(i => i.Rows)
            .FirstOrDefaultAsync(i => i.Id == row.InvoiceId);

        if (invoice != null)
        {
            invoice.TotalSum = invoice.Rows.Sum(r => r.Sum) + row.Sum;
        }

        await _context.SaveChangesAsync();
        return ChangeInvoiceRowDTO(row);

    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var row = await _context.InvoiceRows.FirstOrDefaultAsync(r => r.Id == id);

        if (row == null) return false;

        var invoice = await _context.Invoices
            .Include(i => i.Rows)
            .FirstOrDefaultAsync(i => i.Id == row.InvoiceId);

        if (invoice == null) return false;

        invoice.Rows.Remove(row);
        invoice.TotalSum = invoice.Rows.Sum(r => r.Sum);

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<InvoiceRowDTO> GetByIdAsync(Guid id)
    {
        var row = await _context.InvoiceRows.FirstOrDefaultAsync(r=>r.Id == id);
        if(row == null)
        {
            throw new InvalidOperationException("InvoiceRow not found !!!");
        }
        return ChangeInvoiceRowDTO(row);
    }

    public async Task<IEnumerable<InvoiceRowDTO>> GetByInvoiceIdAsync(Guid invoiceId)
    {
        var rows = await _context.InvoiceRows
            .Where(r => r.InvoiceId == invoiceId)
            .ToArrayAsync();

        return rows.Select(ChangeInvoiceRowDTO);
    }

    private static InvoiceRowDTO ChangeInvoiceRowDTO(InvoiceRow row)
    {
        InvoiceRowDTO invoiceRowDTO = new()
        {
            Id = row.Id,
            InvoiceId = row.InvoiceId,
            Service = row.Service,
            Quantity = row.Quantity,
            Amount = row.Amount,
            Sum = row.Sum,
        };
        return invoiceRowDTO;
    }
}
