using BlazorInvoiceApp.Data;
using BlazorInvoiceApp.Dtos;

namespace BlazorInvoiceApp.Repository;

public interface IInvoiceTermsRepository : IGenericOwnedRepository<InvoiceTerms, InvoiceTermsDto>
{
}
