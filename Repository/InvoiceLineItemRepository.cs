using AutoMapper;
using BlazorInvoiceApp.Data;
using BlazorInvoiceApp.Dtos;

namespace BlazorInvoiceApp.Repository
{
    public class InvoiceLineItemRepository : GenericOwnedRepository<InvoiceLineItem, InvoiceLineItemDto>, IInvoiceLineItemRepository
    {
        public InvoiceLineItemRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {

        }
    }
}
