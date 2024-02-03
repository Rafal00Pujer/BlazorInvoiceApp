using BlazorInvoiceApp.Data;
using BlazorInvoiceApp.Dtos;

namespace BlazorInvoiceApp.Repository;

public interface IInvoiceLineItemRepository : IGenericOwnedRepository<InvoiceLineItem, InvoiceLineItemDto>
{
}
