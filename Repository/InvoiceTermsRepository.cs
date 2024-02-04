using AutoMapper;
using BlazorInvoiceApp.Data;
using BlazorInvoiceApp.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorInvoiceApp.Repository;

public class InvoiceTermsRepository : GenericOwnedRepository<InvoiceTerms, InvoiceTermsDto>, IInvoiceTermsRepository
{
    public InvoiceTermsRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {

    }

    public async Task Seed(ClaimsPrincipal user)
    {
        var userId = GetMyUserId(user);

        if (userId is not null)
        {
            var count = await _context.InvoiceTerms
                .Where(c => c.UserId == userId)
                .CountAsync();

            if (count == 0)
            {
                await AddDefaultTerm("Net 30");
                await AddDefaultTerm("Net 60");
                await AddDefaultTerm("Net 90");

                await _context.SaveChangesAsync();
            }
        }

        async Task AddDefaultTerm(string termName)
        {
            var defaultTerm = new InvoiceTermsDto
            {
                Name = termName
            };

            await AddMine(user, defaultTerm);
        }
    }
}
