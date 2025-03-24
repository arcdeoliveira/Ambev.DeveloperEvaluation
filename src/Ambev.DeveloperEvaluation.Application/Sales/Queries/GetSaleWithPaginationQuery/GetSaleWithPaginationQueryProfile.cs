using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Dtos.Shared;
using Ambev.DeveloperEvaluation.Domain.Dtos.Sales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Dtos.ProductSales;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSaleWithPaginationQuery;

public class GetSaleWithPaginationQueryProfile : Profile
{
    public GetSaleWithPaginationQueryProfile()
    {
        CreateMap<GetSaleWithPaginationQuery, SalePaginationFilter>();
        CreateMap<BaseDataPaginationDto<SalePaginationDto>, GetSaleWithPaginationQueryResponse>(); 

        CreateMap<Sale, SalePaginationDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShortDateString()))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
            .ForMember(dest => dest.ProductSales, opt => opt.MapFrom(src => src.ProductSales));

        CreateMap<ProductSale, ProductSalePaginationDto>()
            .ForMember(dest => dest.Canceled, opt => opt.MapFrom(src => src.Canceled ? "Yes" : "No"))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShortDateString()))
            .ForMember(dest => dest.DateCanceled, opt => opt.MapFrom(src => src.DateCanceled.HasValue 
                ? src.DateCanceled.Value.ToShortDateString() 
                : string.Empty));
    }
}
