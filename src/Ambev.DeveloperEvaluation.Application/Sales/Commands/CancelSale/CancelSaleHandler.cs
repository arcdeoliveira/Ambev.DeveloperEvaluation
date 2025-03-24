using Ambev.DeveloperEvaluation.Application.Notifications;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Services.NonRelational;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSale
{
    public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, CancelSaleResponse>
    {
        private readonly IMediator _mediator;
        private readonly ISaleService _saleService;

        public CancelSaleHandler(IMediator mediator, ISaleService saleService)
        {
            _mediator = mediator;
            _saleService = saleService;
        }

        public async Task<CancelSaleResponse> Handle(CancelSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new CancelSaleValidator();
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
                var domainNotification = DomainNotification.Create("NotUnauthorized", $"Sale is already with status cancelled.");

                await _mediator.Publish(domainNotification, cancellationToken);
                return default!;
            }

            sale.AlterStatus(SaleStatus.Cancelled);
            sale.AlterDateUpdate(); 

            await _saleService.UpdateAsync(command.Id, sale);
            return new CancelSaleResponse { Success = true };
        }
    }

}
