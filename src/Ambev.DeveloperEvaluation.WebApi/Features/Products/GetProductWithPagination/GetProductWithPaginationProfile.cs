using Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductWithPaginationQuery;
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
