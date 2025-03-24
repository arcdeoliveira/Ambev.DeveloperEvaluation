using Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSaleItem;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSaleItem
{
    public class CancelSaleItemProfile : Profile
    {
        public CancelSaleItemProfile() 
        {
            CreateMap<CancelSaleItemRequest, CancelSaleItemCommand>();
            CreateMap<CancelSaleItemResult, CancelSaleItemResponse>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShortDateString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue
                    ? src.UpdatedAt.Value.ToShortDateString()
                    : string.Empty));

        }
    }
}
