using Ambev.DeveloperEvaluation.Application.Notifications;
using Ambev.DeveloperEvaluation.Domain.Dtos.Products;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Services.NonRelational;
using AutoMapper;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductWithPaginationQuery
{
    public class GetProductWithPaginationQueryHandler : IRequestHandler<GetProductWithPaginationQuery, GetProductWithPaginationQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IProductService _productService;

        public GetProductWithPaginationQueryHandler(IMapper mapper, IMediator mediator, IProductService productService)
        {
            _mapper = mapper;
            _mediator = mediator;
            _productService = productService;
        }

        public async Task<GetProductWithPaginationQueryResponse> Handle(GetProductWithPaginationQuery query, CancellationToken cancellationToken)
        {
            var validator = new GetProductWithPaginationValidator();
            var validationResult = await validator.ValidateAsync(query, cancellationToken);

            if (!validationResult.IsValid)
            {
                var domainNotification = validationResult.Errors
                    .Select(error => DomainNotification.Create(error.PropertyName, error.ErrorMessage))
                    .ToList();

                domainNotification.ForEach(async item => await _mediator.Publish(item, cancellationToken));
                
                return default!;
            }

            var paginationFilter = _mapper.Map<ProductPaginationFilter>(query);
            var productFilter = GetFilterToPagination(paginationFilter);
            var projectionDto = GetProjectionToPagination();

            var product = await _productService.GetAllProductsWithPagination(productFilter, projectionDto, query.PageNumber, query.PageSize, cancellationToken);

            return _mapper.Map<GetProductWithPaginationQueryResponse>(product);
        }

        private static FilterDefinition<Product> GetFilterToPagination(ProductPaginationFilter filter)
        {
            var builder = Builders<Product>.Filter;
            var filters = new List<FilterDefinition<Product>>();

            if (!string.IsNullOrEmpty(filter.Name))
            {
                filters.Add(builder.Regex(x => x.Name, new BsonRegularExpression(filter.Name, "i")));
            }

            if (filter.CreatedInitial.HasValue)
            {
                filters.Add(builder.Gte(x => x.CreatedAt, filter.CreatedInitial.Value.Date));
            }

            if (filter.Price.HasValue)
            {
                filters.Add(builder.Gte(x => x.Price, filter.Price.Value));
            }

            if (filter.Status.HasValue && filter.Status != ProductStatus.Unknown)
            {
                filters.Add(builder.Eq(x => x.Status, filter.Status.Value));
            }

            return filters.Count == 0 ? builder.Empty : builder.And(filters);
        }

        private static ProjectionDefinition<Product, ProductPaginationDto> GetProjectionToPagination() 
        {
            return Builders<Product>.Projection.Expression(produto => new ProductPaginationDto
            {
                Id = produto.Id,
                Name = produto.Name,
                Description = produto.Description,
                Price = produto.Price
            });
        }
    }
}
