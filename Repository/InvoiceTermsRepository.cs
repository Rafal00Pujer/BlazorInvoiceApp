using AutoMapper;
using BlazorInvoiceApp.Data;
using BlazorInvoiceApp.Dtos;

namespace BlazorInvoiceApp.Repository;

public class InvoiceTermsRepository : GenericOwnedRepository<InvoiceTerms, InvoiceTermsDto>, IInvoiceTermsRepository
{
    public InvoiceTermsRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {

    }
}
