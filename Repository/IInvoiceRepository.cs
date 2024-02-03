using BlazorInvoiceApp.Data;
using BlazorInvoiceApp.Dtos;

namespace BlazorInvoiceApp.Repository;

public interface IInvoiceRepository : IGenericOwnedRepository<Invoice, InvoiceDto>
{
}
