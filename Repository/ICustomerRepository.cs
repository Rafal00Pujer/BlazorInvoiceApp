using BlazorInvoiceApp.Data;
using BlazorInvoiceApp.Dtos;
using System.Security.Claims;

namespace BlazorInvoiceApp.Repository;

public interface ICustomerRepository : IGenericOwnedRepository<Customer, CustomerDto>
{
    public Task Seed(ClaimsPrincipal user);
}
