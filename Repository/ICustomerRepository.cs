using BlazorInvoiceApp.Data;
using BlazorInvoiceApp.Dtos;

namespace BlazorInvoiceApp.Repository;

public interface ICustomerRepository : IGenericOwnedRepository<Customer, CustomerDto>
{
}
