﻿namespace BlazorInvoiceApp.Dtos;

public class CustomerDto : IDto, IOwnedDto
{
    public string Id { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
}
