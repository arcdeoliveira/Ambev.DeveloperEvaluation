using Ambev.DeveloperEvaluation.Application.Notifications;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Services;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductByIdQuery
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdQueryResult>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IProductService _productService;

        public GetProductByIdQueryHandler(IMapper mapper, IMediator mediator, IProductService productService)
        {
            _mapper = mapper;
            _mediator = mediator;
            _productService = productService;
        }

        public async Task<GetProductByIdQueryResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var validator = new GetProductByIdQueryValidator();
            var validationResult = await validator.ValidateAsync(query, cancellationToken);

            if (!validationResult.IsValid)
            {
                var domainNotification = validationResult.Errors
                    .Select(error => DomainNotification.Create(error.PropertyName, error.ErrorMessage))
                    .ToList();

                domainNotification.ForEach(async item => await _mediator.Publish(item, cancellationToken)); 

                return default!;
            }

            var product = await _productService.GetByIdAsync(query.Id);
            if (product is null)
            {
                var domainNotification = DomainNotification.Create("NotFound", $"Product with Id {query.Id} not found.");

                await _mediator.Publish(domainNotification, cancellationToken);
                return default!;
            }

            return _mapper.Map<GetProductByIdQueryResult>(product);
        }
    }
}
