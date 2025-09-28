using InvoicerAPI.DTOs.InvoiceDTO;
using InvoicerAPI.Enums;
using InvoicerAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace InvoicerAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvoicerController : ControllerBase
{
    private readonly IInvoiceService _service;

    public InvoicerController(IInvoiceService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateInvoiceDTO dto)
    {
        var invoice = await _service.CreateInvoice(dto);
        return Ok(invoice);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(Guid id, [FromBody] UpdateInvoiceDTO dto)
    {
        var invoice = await _service.EditInvoice(id, dto);
        return Ok(invoice);
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> ChangeStatus(Guid id, [FromQuery] InvoiceStatus status)
    {
        var invoice = await _service.ChangeInvoiceStatus(id, status);
        return Ok(invoice);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var invoice = await _service.DeleteInvoice(id);
        if( !invoice ) return BadRequest("Invoice cannot be deleted !!!");

        return Ok(invoice);
    }

    [HttpPatch("{id}/archive")]
    public async Task<IActionResult> ArchiveInvoice(Guid id)
    {
        var invoice = await _service.ArchiveInvoice(id); 
        if (!invoice) return BadRequest("Invoice cannot be deleted !!!");
        return Ok(invoice);
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var invoices = await _service.GetInvoicesList();
        return Ok(invoices);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var invoice = await _service.GetInvoiceById(id);
        return Ok(invoice);
    }

}
