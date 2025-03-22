using Ambev.DeveloperEvaluation.Application.Products.Commands.DeleteProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct
{
    public class DeleteProductProfile : Profile
    {
        public DeleteProductProfile() 
        {
            CreateMap<string, DeleteProductCommand>()
                .ConstructUsing(id => new DeleteProductCommand(id));
        }
    }
}
