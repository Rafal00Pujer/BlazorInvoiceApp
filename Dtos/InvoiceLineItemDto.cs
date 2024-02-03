namespace BlazorInvoiceApp.Dtos;

public class InvoiceLineItemDto : IDto, IOwnedDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string UserId { get; set; } = string.Empty;

    public string InvoiceId { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public double UnitPrice { get; set; }

    public double Quantity { get; set; }
}
