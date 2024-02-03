
using AutoMapper;
using BlazorInvoiceApp.Data;
using Microsoft.EntityFrameworkCore;

namespace BlazorInvoiceApp.Repository;

public class RepositoryCollection : IRepositoryCollection
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public IInvoiceRepository Invoice { get; private set; }

    public ICustomerRepository Customer { get; private set; }

    public IInvoiceTermsRepository InvoiceTerms { get; private set; }

    public IInvoiceLineItemRepository InvoiceLineItem { get; private set; }

    public RepositoryCollection(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper)
    {
        _context = contextFactory.CreateDbContext();
        _mapper = mapper;

        Invoice = new InvoiceRepository(_context, _mapper);
        Customer = new CustomerRepository(_context, _mapper);
        InvoiceTerms = new InvoiceTermsRepository(_context, _mapper);
        InvoiceLineItem = new InvoiceLineItemRepository(_context, _mapper);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<int> Save()
    {
        try
        {
            return await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            foreach (var item in ex.Entries)
            {
                if (item.State == EntityState.Modified)
                {
                    item.CurrentValues.SetValues(item.OriginalValues);
                    item.State = EntityState.Unchanged;

                    throw new RepositoryUpdateException();
                }

                if(item.State == EntityState.Deleted)
                {
                    item.State = EntityState.Unchanged;

                    throw new RepositoryDeleteException();
                }

                if(item.State == EntityState.Added)
                {
                    throw new RepositoryAddException();
                }
            }
        }

        return 0;
    }
}
