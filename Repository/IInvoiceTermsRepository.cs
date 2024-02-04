using BlazorInvoiceApp.Data;
using BlazorInvoiceApp.Dtos;
using System.Security.Claims;

namespace BlazorInvoiceApp.Repository;

public interface IInvoiceTermsRepository : IGenericOwnedRepository<InvoiceTerms, InvoiceTermsDto>
{
    Task Seed(ClaimsPrincipal user);
}
