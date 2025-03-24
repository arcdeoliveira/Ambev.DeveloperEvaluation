using Ambev.DeveloperEvaluation.Application.Notifications;
using Ambev.DeveloperEvaluation.Domain.Dtos.ProductSales;
using Ambev.DeveloperEvaluation.Domain.Dtos.Sales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Services.NonRelational;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Services.Relational;
using AutoMapper;
using MediatR;
using MongoDB.Driver;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSaleWithPaginationQuery
{
    public class GetSaleWithPaginationQueryHandler : IRequestHandler<GetSaleWithPaginationQuery, GetSaleWithPaginationQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        private readonly IAffiliateService _afiliateService;
        private readonly IProductService _productService;
        private readonly ISaleService _saleService;
        private readonly IUserService _userService; 

        public GetSaleWithPaginationQueryHandler(IMapper mapper, IMediator mediator, IAffiliateService afiliateService, IProductService productService, 
            ISaleService saleService, IUserService userService)
        {
            _mapper = mapper;
            _mediator = mediator;
            _afiliateService = afiliateService;
            _productService = productService;
            _saleService = saleService;
            _userService = userService;
        }

        public async Task<GetSaleWithPaginationQueryResponse> Handle(GetSaleWithPaginationQuery query, CancellationToken cancellationToken)
        {
            var validator = new GetSaleWithPaginationValidator();
            var validationResult = await validator.ValidateAsync(query, cancellationToken);

            if (!validationResult.IsValid)
            {
                var domainNotification = validationResult.Errors
                    .Select(error => DomainNotification.Create(error.PropertyName, error.ErrorMessage))
                    .ToList();

                domainNotification.ForEach(async item => await _mediator.Publish(item, cancellationToken));                
                return default!;
            }

            var paginationFilter = _mapper.Map<SalePaginationFilter>(query);
            var saleFilter = GetFilterToPagination(paginationFilter);
            var projectionDto = GetProjectionToPagination();

            var saleList = await _saleService.GetPaginatedSaleList(saleFilter, projectionDto, query.PageNumber, query.PageSize, cancellationToken);

            await GetSalesAditionalInformation(saleList.Data, cancellationToken);    
            return _mapper.Map<GetSaleWithPaginationQueryResponse>(saleList);
        }

        private async Task GetSalesAditionalInformation(IEnumerable<SalePaginationDto> data, CancellationToken cancellationToken)
        {
            if (data == null)
                return;

            foreach (var item in data)
            {
                item.AfilliateName = await _afiliateService.GetAfiliateNameAsync((short)item.AfiliateId, cancellationToken) ?? string.Empty;
                item.UserName = await _userService.GetUserNameAsync(item.UserId, cancellationToken) ?? string.Empty;
                
                foreach(var productSale in item.ProductSales)
                {
                    var product = await _productService.GetByIdAsync(productSale.ProductId);
                    productSale.ProductName = product.Name;
                };
            }
        }

        private static FilterDefinition<Sale> GetFilterToPagination(SalePaginationFilter filter)
        {
            var builder = Builders<Sale>.Filter;
            var filters = new List<FilterDefinition<Sale>>();

            if (filter.AfiliateId.HasValue)            
                filters.Add(builder.Eq(x => x.AfiliateId, filter.AfiliateId.Value));            

            if (filter.InitialDate.HasValue)            
                filters.Add(builder.Gte(x => x.CreatedAt, filter.InitialDate.Value.Date));            

            if (filter.FinalDate.HasValue)            
                filters.Add(builder.Lte(x => x.CreatedAt, filter.FinalDate.Value.Date));            

            if (filter.Total.HasValue)            
                filters.Add(builder.Gte(x => x.Total, filter.Total.Value));            

            if (filter.Status.HasValue && filter.Status != SaleStatus.None)            
                filters.Add(builder.Eq(x => x.Status, filter.Status.Value));            

            if (filter.UserId.HasValue)            
                filters.Add(builder.Eq(x => x.UserId, filter.UserId.Value));            

            return filters.Count == 0 ? builder.Empty : builder.And(filters);
        }

        private ProjectionDefinition<Sale, SalePaginationDto> GetProjectionToPagination() 
        {
            return Builders<Sale>.Projection.Expression(sale => new SalePaginationDto
            {
                Id = sale.Id,
                CreatedAt = sale.CreatedAt,
                UpdatedAt = sale.UpdatedAt,
                Status = sale.Status,
                Total = sale.Total,
                AfiliateId = sale.AfiliateId,
                UserId = sale.UserId,
                ProductSales = sale.ProductSales.Select(productSale => new ProductSalePaginationDto
                {
                    ProductId = productSale.ProductId,
                    CreatedAt = productSale.CreatedAt,
                    Canceled = productSale.Canceled,
                    DateCanceled = productSale.DateCanceled,
                    UnitPrice = productSale.UnitPrice,
                    UnitDiscount = productSale.UnitDiscount,
                    Quantity = productSale.Quantity,
                    Total = productSale.Total
                })
            });
        }
    }
}
