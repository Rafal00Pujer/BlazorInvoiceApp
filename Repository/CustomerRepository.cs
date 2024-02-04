using AutoMapper;
using BlazorInvoiceApp.Data;
using BlazorInvoiceApp.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorInvoiceApp.Repository;

public class CustomerRepository : GenericOwnedRepository<Customer, CustomerDto>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task Seed(ClaimsPrincipal user)
    {
        var userId = GetMyUserId(user);

        if (userId is not null)
        {
            var count = await _context.Customers
                .Where(c => c.UserId == userId)
                .CountAsync();

            if (count == 0)
            {
                var defaultCustomer = new CustomerDto
                {
                    Name = "My Fisrt Customer"
                };

                await AddMine(user, defaultCustomer);
                await _context.SaveChangesAsync();
            }
        }
    }
}
