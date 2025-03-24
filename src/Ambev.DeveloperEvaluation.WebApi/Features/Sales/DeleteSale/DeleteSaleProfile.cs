using Ambev.DeveloperEvaluation.Application.Sales.Commands.DeleteSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale
{
    public class DeleteSaleProfile : Profile
    {
        public DeleteSaleProfile() 
        {
            CreateMap<string, DeleteSaleCommand>()
                .ConstructUsing(id => new DeleteSaleCommand(id));
        }
    }
}
