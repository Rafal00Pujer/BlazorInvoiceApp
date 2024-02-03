using AutoMapper;
using BlazorInvoiceApp.Data;
using BlazorInvoiceApp.Dtos;

namespace BlazorInvoiceApp.Repository;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Customer, CustomerDto>();
        CreateMap<CustomerDto, Customer>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<InvoiceTerms, InvoiceTermsDto>();
        CreateMap<InvoiceTermsDto, InvoiceTerms>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<InvoiceLineItem, InvoiceLineItemDto>();
        CreateMap<InvoiceLineItemDto, InvoiceLineItem>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<Invoice, InvoiceDto>()
            .ForMember(dest => dest.CustomerName,
                opt => opt.MapFrom(src => src.Customer!.Name))
            .ForMember(dest => dest.InvoiceTermsName,
                opt => opt.MapFrom(src => src.InvoiceTerms!.Name));

        CreateMap<InvoiceDto, Invoice>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}
