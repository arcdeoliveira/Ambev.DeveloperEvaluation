using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSaleItem
{
    public class CancelSaleItemProfile : Profile
    {
        public CancelSaleItemProfile() 
        {
            CreateMap<Sale, CancelSaleItemResult>();        
        }
    }
}
