using AutoMapper;
using BlazorInvoiceApp.Data;
using BlazorInvoiceApp.Dtos;

namespace BlazorInvoiceApp.Repository;

public class InvoiceRepository : GenericOwnedRepository<Invoice, InvoiceDto>, IInvoiceRepository
{
    public InvoiceRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {

    }
}
