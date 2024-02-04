using AutoMapper;
using BlazorInvoiceApp.Data;
using BlazorInvoiceApp.Dtos;
using System.Security.Claims;

namespace BlazorInvoiceApp.Repository
{
    public class InvoiceLineItemRepository : GenericOwnedRepository<InvoiceLineItem, InvoiceLineItemDto>, IInvoiceLineItemRepository
    {
        public InvoiceLineItemRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public async Task<List<InvoiceLineItemDto>> GetAllByInvoiceId(ClaimsPrincipal user, string invoiceId)
        {
            return await GenericQuery(user, l => l.InvoiceId == invoiceId, null);
        }
    }
}
