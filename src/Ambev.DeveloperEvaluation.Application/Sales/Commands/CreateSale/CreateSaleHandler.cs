using System.Text;
using Ambev.DeveloperEvaluation.Application.Notifications;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Services.NonRelational;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        private readonly IProductService _productService;   
        private readonly IProductSaleService _productSaleService;   
        private readonly ISaleService _saleService; 

        public CreateSaleHandler(IMapper mapper, IMediator mediator, IProductService productService, 
            IProductSaleService productSaleService, ISaleService saleService)
        {
            _mapper = mapper;   
            _mediator = mediator;
            _productService = productService;
            _productSaleService = productSaleService;
            _saleService = saleService; 
        }

        public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
           var validator = new CreateSaleValidator();
           var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                var domainNotification = validationResult.Errors
                    .Select(error => DomainNotification.Create(error.PropertyName, error.ErrorMessage))
                    .ToList();

                await _mediator.Publish(domainNotification, cancellationToken);
                return default!;
            }

            var productSaleDtos = command.Products.Distinct();
            var maxQuantityAllowed = _productSaleService.GetMaxQuantityAllowedToSell();
            var filteredProducts = await _productService.GetEligibleProductsForSale(productSaleDtos, maxQuantityAllowed, cancellationToken);

            var notFoundProductsNotification = _productService.GenerateNotFoundProductIdsMessage(filteredProducts.NotFoundProductIds);
            var ineligibleProductsNotification = _productSaleService.GenerateIneligibleProductIdsMessage(filteredProducts.IneligibleProductIds, maxQuantityAllowed);

            if (filteredProducts.EligibleProducts.Count == 0)
            {
                await NotifyUpdateSaleErrors(notFoundProductsNotification, ineligibleProductsNotification, cancellationToken);
                return default!;
            }

            var productSales = await _productSaleService.ProcessProductSales(filteredProducts.EligibleProducts);

            var sales = _mapper.Map<Sale>(command);
            sales.AlterItens(productSales);
            sales.SetTotal();

            await _saleService.CreateAsync(sales); 

            var result = _mapper.Map<CreateSaleResult>(sales);
            result.IneligibleProductMessage = ineligibleProductsNotification;
            result.NotFoundProductMessage = notFoundProductsNotification;

            return result;  
        }

        private Task NotifyUpdateSaleErrors(string notFoundProductsNotification, string ineligibleProductsNotification, CancellationToken cancellationToken)
        {
            var stringBuilder = new StringBuilder("Not possible to create sale. Details of the error: ");
            stringBuilder.AppendLine(string.Empty);

            if (!string.IsNullOrEmpty(notFoundProductsNotification))
                stringBuilder.AppendLine(notFoundProductsNotification);

            if (!string.IsNullOrEmpty(ineligibleProductsNotification))
                stringBuilder.AppendLine(ineligibleProductsNotification);

            var domainNotification = DomainNotification.Create("ErrorCreate", stringBuilder.ToString());
            return _mediator.Publish(domainNotification, cancellationToken);
        }
    }
}
