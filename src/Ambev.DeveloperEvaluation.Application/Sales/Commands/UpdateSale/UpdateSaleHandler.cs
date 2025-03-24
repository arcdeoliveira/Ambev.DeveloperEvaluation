using System.Text;
using Ambev.DeveloperEvaluation.Application.Notifications;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Services.NonRelational;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        private readonly IProductService _productService;
        private readonly IProductSaleService _productSaleService;
        private readonly ISaleService _saleService;

        public UpdateSaleHandler(IMapper mapper, IMediator mediator, IProductService productService, 
            IProductSaleService productSaleService, ISaleService saleService)
        {
            _mapper = mapper;
            _mediator = mediator;
            _productService = productService;
            _productSaleService = productSaleService;
            _saleService = saleService;
        }

        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleValidator();
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
                var domainNotification = DomainNotification.Create("NotUnauthorized", $"Sale is with status cancelled. Cannot be updated.");

                await _mediator.Publish(domainNotification, cancellationToken);
                return default!;
            }

            var maxQuantityAllowed = _productSaleService.GetMaxQuantityAllowedToSell();
            var filteredProducts = await _productService.GetEligibleProductsForSale(command.NewProductIds, maxQuantityAllowed, cancellationToken);

            var notFoundProductsNotification = _productService.GenerateNotFoundProductIdsMessage(filteredProducts.NotFoundProductIds); 
            var ineligibleProductsNotification = _productSaleService.GenerateIneligibleProductIdsMessage(filteredProducts.IneligibleProductIds, maxQuantityAllowed);

            if (filteredProducts.EligibleProducts.Count == 0)
            {
                await NotifyUpdateSaleErrors(notFoundProductsNotification, ineligibleProductsNotification, cancellationToken);
                return default!;
            }

            var productSales = await _productSaleService.ProcessProductSales(filteredProducts.EligibleProducts);
            await _saleService.UpdateSaleProdcutsAsync(sale, productSales, command.UserId, (short?)command.AffiliateId);

            var result = _mapper.Map<UpdateSaleResult>(sale);
            result.WarningIneligibleProductMessage = ineligibleProductsNotification;
            result.WarningNotFoundProductMessage = notFoundProductsNotification;

            return result;  
        }

        private Task NotifyUpdateSaleErrors(string notFoundProductsNotification, string ineligibleProductsNotification, CancellationToken cancellationToken)
        {
            var stringBuilder = new StringBuilder("Not possible to update sale. Details of the error: ");
            stringBuilder.AppendLine(string.Empty);

            if (!string.IsNullOrEmpty(notFoundProductsNotification))
                stringBuilder.AppendLine(notFoundProductsNotification);

            if (!string.IsNullOrEmpty(ineligibleProductsNotification))
                stringBuilder.AppendLine(ineligibleProductsNotification);
           
            var domainNotification = DomainNotification.Create("ErrorUpdate", stringBuilder.ToString());
            return _mediator.Publish(domainNotification, cancellationToken);
        }
    }

}
