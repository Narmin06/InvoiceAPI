using InvoicerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoicerAPI.Data;

public class InvoicerContext :DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<InvoiceRow> InvoiceRows => Set<InvoiceRow>();

    public InvoicerContext(DbContextOptions<InvoicerContext> options): base(options)
    { }
}
