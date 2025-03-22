using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Dtos.Products;
using Ambev.DeveloperEvaluation.Domain.Dtos.Shared;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductWithPaginationQuery;

public class GetProductWithPaginationQueryProfile : Profile
{
    public GetProductWithPaginationQueryProfile()
    {
        CreateMap<GetProductWithPaginationQuery, ProductPaginationFilter>();
        CreateMap<BaseDataPaginationDto<ProductPaginationDto>, GetProductWithPaginationQueryResponse>(); 
    }
}
