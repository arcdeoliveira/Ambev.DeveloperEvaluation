using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductByIdQuery;

public class GetProductByIdQueryProfile : Profile
{
    public GetProductByIdQueryProfile()
    {
        CreateMap<Product, GetProductByIdQueryResult>();
    }
}
