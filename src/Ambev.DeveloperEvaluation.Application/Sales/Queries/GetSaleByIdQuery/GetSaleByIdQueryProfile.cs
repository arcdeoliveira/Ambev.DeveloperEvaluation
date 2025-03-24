using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSaleByIdQuery;

public class GetSaleByIdQueryProfile : Profile
{
    public GetSaleByIdQueryProfile()
    {
        CreateMap<Sale, GetSaleByIdQueryResult>();
    }
}
