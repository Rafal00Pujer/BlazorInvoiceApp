using AutoMapper;
using BlazorInvoiceApp.Data;
using BlazorInvoiceApp.Dtos;

namespace BlazorInvoiceApp.Repository;

public class CustomerRepository : GenericOwnedRepository<Customer, CustomerDto>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}
