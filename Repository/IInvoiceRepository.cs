using BlazorInvoiceApp.Data;
using BlazorInvoiceApp.Dtos;
using System.Security.Claims;

namespace BlazorInvoiceApp.Repository;

public interface IInvoiceRepository : IGenericOwnedRepository<Invoice, InvoiceDto>
{
    Task<List<InvoiceDto>> GetAllMineFlat(ClaimsPrincipal user);
    Task DeleteWithLineItems(ClaimsPrincipal user, string invoiceId);
}
