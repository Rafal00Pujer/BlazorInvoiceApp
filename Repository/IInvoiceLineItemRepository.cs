using BlazorInvoiceApp.Data;
using BlazorInvoiceApp.Dtos;
using System.Security.Claims;

namespace BlazorInvoiceApp.Repository;

public interface IInvoiceLineItemRepository : IGenericOwnedRepository<InvoiceLineItem, InvoiceLineItemDto>
{
    Task<List<InvoiceLineItemDto>> GetAllByInvoiceId(ClaimsPrincipal user, string invoiceId);
}
