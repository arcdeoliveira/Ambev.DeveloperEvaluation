using Ambev.DeveloperEvaluation.Application.Notifications;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Services.NonRelational;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSaleItem
{
    public class CancelSaleItemHandler : IRequestHandler<CancelSaleItemCommand, CancelSaleItemResult>
    {
        private readonly IMapper _mapper;   
        private readonly IMediator _mediator;
        private readonly IProductSaleService _productSaleService;
        private readonly ISaleService _saleService;

        public CancelSaleItemHandler(IMapper mapper, IMediator mediator, IProductSaleService productSaleService, ISaleService saleService)
        {
            _mapper = mapper;
            _mediator = mediator;
            _productSaleService = productSaleService;   
            _saleService = saleService;

        }

        public async Task<CancelSaleItemResult> Handle(CancelSaleItemCommand command, CancellationToken cancellationToken)
        {
            var validator = new CancelSaleItemValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                var domainNotification = validationResult.Errors
                    .Select(error => DomainNotification.Create(error.PropertyName, error.ErrorMessage))
                    .ToList();

                domainNotification.ForEach(async item => await _mediator.Publish(item, cancellationToken));
                return default!;
            }

            var sale = await _saleService.GetByIdAsync(command.Id);
            if (sale is null)
            {
                var domainNotification = DomainNotification.Create("NotFound", $"Sale with ID {command.Id} not found.");

                await _mediator.Publish(domainNotification, cancellationToken);
                return default!;
            }

            if (sale.Status == SaleStatus.Cancelled)
            {
                var domainNotification = DomainNotification.Create("NotUnauthorized", $"Sale is with status cancelled. Cannot cancel item.");

                await _mediator.Publish(domainNotification, cancellationToken);
                return default!;
            }

            var canceledItemsMessage = _productSaleService.CancelProductsInSale(command.ProductIds, sale.ProductSales);

            sale.AlterCostumer(command.UserId);
            sale.AlterDateUpdate();
            sale.SetTotal();

            await _saleService.UpdateAsync(sale.Id, sale);

            var result = _mapper.Map<CancelSaleItemResult>(sale);
            result.NotFoundMessage = canceledItemsMessage;

            return result;
        }
    }
}
