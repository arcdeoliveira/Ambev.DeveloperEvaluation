using Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSaleByIdQuery;
using Ambev.DeveloperEvaluation.Domain.Dtos.ProductSales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleById
{
    public class GetaleByIdProfile : Profile
    {
        public GetaleByIdProfile() 
        { 
            CreateMap<string, GetSaleByIdQuery>().ConstructUsing(id => new GetSaleByIdQuery(id));
            CreateMap<GetSaleByIdQueryResult, GetSaleByIdResponse>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShortDateString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue 
                    ? src.UpdatedAt.Value.ToShortDateString() 
                    : string.Empty));

            CreateMap<ProductSale, ProductSaleGetByIdDto>()
                .ForMember(dest => dest.Canceled, opt => opt.MapFrom(src => src.Canceled ? "Yes" : "No"))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShortDateString()))
                .ForMember(dest => dest.DateCanceled, opt => opt.MapFrom(src => src.DateCanceled.HasValue 
                ? src.DateCanceled.Value.ToShortDateString()
                : string.Empty));
        }  
    }
}
