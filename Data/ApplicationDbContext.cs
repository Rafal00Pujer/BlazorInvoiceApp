using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;

namespace BlazorInvoiceApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<InvoiceTerms> InvoiceTerms { get; set; }

        public DbSet<InvoiceLineItem> InvoiceLineItems { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            RemoveFixups(builder, typeof(Invoice));
            RemoveFixups(builder, typeof(InvoiceTerms));
            RemoveFixups(builder, typeof(InvoiceLineItem));
            RemoveFixups(builder, typeof(Customer));

            builder.Entity<Invoice>()
                .Property(u => u.InvoiceNumber)
                .Metadata
                .SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            builder.Entity<InvoiceLineItem>()
                .Property(u => u.TotalPrice)
                .HasComputedColumnSql("[UnitPrice] * [Quantity]");

            base.OnModelCreating(builder);
        }

        protected void RemoveFixups(ModelBuilder modelBuilder, Type type)
        {
            foreach (var relationship in modelBuilder.Model.FindEntityType(type)!.GetForeignKeys())
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientNoAction;
            }
        }
    }
}
