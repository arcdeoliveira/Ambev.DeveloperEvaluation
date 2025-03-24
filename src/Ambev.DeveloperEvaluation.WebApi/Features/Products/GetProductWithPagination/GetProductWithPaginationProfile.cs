using Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductWithPaginationQuery;
using Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSaleWithPaginationQuery;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductWithPagination
{
    public class GetProductWithPaginationProfile : Profile
    {
        public GetProductWithPaginationProfile() 
        {
            CreateMap<GetProductWithPaginationRequest, GetProductWithPaginationQuery>();
        }
    }
}
