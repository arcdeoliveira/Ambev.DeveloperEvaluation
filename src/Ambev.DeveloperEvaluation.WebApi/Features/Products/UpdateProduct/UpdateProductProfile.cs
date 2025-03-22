using Ambev.DeveloperEvaluation.Application.Products.Commands.UpdateProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct
{
    public class UpdateProductProfile : Profile
    {
        public UpdateProductProfile() 
        {
            CreateMap<UpdateProductRequest, UpdateProductCommand>();
            CreateMap<UpdateProductResult, UpdateProductResponse>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShortDateString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}
