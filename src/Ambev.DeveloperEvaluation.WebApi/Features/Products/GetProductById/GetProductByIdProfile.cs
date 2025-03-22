using Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductByIdQuery;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductById
{
    public class GetProductByIdProfile : Profile
    {
        public GetProductByIdProfile() 
        { 
            CreateMap<string, GetProductByIdQuery>().ConstructUsing(id => new GetProductByIdQuery(id));
            CreateMap<GetProductByIdQueryResult, GetProductByIdResponse>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShortDateString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }  
    }
}
