using Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSaleWithPaginationQuery;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleWithPagination
{
    public class GetSaleWithPaginationProfile : Profile
    {
        public GetSaleWithPaginationProfile() 
        {
            CreateMap<GetSaleWithPaginationRequest, GetSaleWithPaginationQuery>();
        }
    }
}
