using AutoMapper;
using BlazorInvoiceApp.Data;
using BlazorInvoiceApp.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorInvoiceApp.Repository;

public class InvoiceRepository : GenericOwnedRepository<Invoice, InvoiceDto>, IInvoiceRepository
{
    public InvoiceRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {

    }

    public async Task DeleteWithLineItems(ClaimsPrincipal user, string invoiceId)
    {
        var userid = GetMyUserId(user);

        var lineitems = await _context.InvoiceLineItems
            .Where(i => i.InvoiceId == invoiceId
                && i.UserId == userid)
            .ToListAsync();

        foreach (InvoiceLineItem lineitem in lineitems)
        {
            _context.InvoiceLineItems.Remove(lineitem);
        }

        Invoice? invoice = await _context.Invoices
            .Where(i => i.Id == invoiceId
                && i.UserId == userid)
            .FirstOrDefaultAsync();

        if (invoice is not null)
        {
            _context.Invoices.Remove(invoice);
        }
    }

    public async Task<List<InvoiceDto>> GetAllMineFlat(ClaimsPrincipal user)
    {
        var userid = GetMyUserId(user);

        var q = from i in _context.Invoices
                .Where(i => i.UserId == userid)
                .Include(i => i.InvoiceLineItems)
                .Include(i => i.InvoiceTerms)
                .Include(i => i.Customer)
                select new InvoiceDto
                {
                    Id = i.Id,
                    CreateDate = i.CreateDate,
                    InvoiceNumber = i.InvoiceNumber,
                    Description = i.Description,
                    CustomerId = i.CustomerId,
                    CustomerName = i.Customer!.Name,
                    InvoiceTermsId = i.InvoiceTermsId,
                    InvoiceTermsName = i.InvoiceTerms!.Name,
                    Paid = i.Paid,
                    Credit = i.Credit,
                    TaxRate = i.TaxRate,
                    UserId = i.UserId,
                    InvoiceTotal = i.InvoiceLineItems.Sum(a => a.TotalPrice)
                };


        var results = await q.ToListAsync();
        return results;
    }
}
