using InvoicerAPI.DTOs.InvoiceRowDTOs;
using InvoicerAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace InvoicerAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvoiceRowController : ControllerBase
{
    private readonly IInvoiceRowService _service;

    public InvoiceRowController(IInvoiceRowService rowService)
    {
        _service = rowService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateInvoiceRowDTO dto)
    {
        var row = await _service.CreateAsync(dto);
        return Ok(row);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateInvoiceRowDTO dto)
    {
        var row = await _service.UpdateAsync(id, dto);
        return Ok(row);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _service.DeleteAsync(id);
        if (!result) return BadRequest("InvoiceRow cannot be deleted !!!");

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var row = await _service.GetByIdAsync(id);
        return Ok(row);
    }

    [HttpGet("{invoiceId}/invoice")]
    public async Task<IActionResult> GetByInvoiceId(Guid invoiceId)
    {
        var rows = await _service.GetByInvoiceIdAsync(invoiceId);
        return Ok(rows);
    }
}
