using Ambev.DeveloperEvaluation.Application.Notifications;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Services.NonRelational;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSaleByIdQuery
{
    public class GetSaleByIdQueryHandler : IRequestHandler<GetSaleByIdQuery, GetSaleByIdQueryResult>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ISaleService _saleService;

        public GetSaleByIdQueryHandler(IMapper mapper, IMediator mediator, ISaleService saleService)
        {
            _mapper = mapper;
            _mediator = mediator;
            _saleService = saleService;
        }

        public async Task<GetSaleByIdQueryResult> Handle(GetSaleByIdQuery query, CancellationToken cancellationToken)
        {
            var validator = new GetSaleByIdQueryValidator();
            var validationResult = await validator.ValidateAsync(query, cancellationToken);

            if (!validationResult.IsValid)
            {
                var domainNotification = validationResult.Errors
                    .Select(error => DomainNotification.Create(error.PropertyName, error.ErrorMessage))
                    .ToList();

                domainNotification.ForEach(async item => await _mediator.Publish(item, cancellationToken)); 

                return default!;
            }

            var sale = await _saleService.GetByIdAsync(query.Id);
            if (sale is null)
            {
                var domainNotification = DomainNotification.Create("NotFound", $"Sale with ID {query.Id} not found.");

                await _mediator.Publish(domainNotification, cancellationToken);
                return default!;
            }

            return _mapper.Map<GetSaleByIdQueryResult>(sale);
        }
    }
}
