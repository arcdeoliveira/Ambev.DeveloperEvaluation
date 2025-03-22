using Ambev.DeveloperEvaluation.Application.Products.Commands.DiscontinueProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.DiscontinueProduct
{
    public class DiscontinueProductProfile : Profile
    {
        public DiscontinueProductProfile() 
        {
            CreateMap<string, DiscontinueProductCommand>()
                .ConstructUsing(id => new DiscontinueProductCommand(id));
        }
    }
}
