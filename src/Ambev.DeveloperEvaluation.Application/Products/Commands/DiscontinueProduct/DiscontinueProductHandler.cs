using Ambev.DeveloperEvaluation.Application.Notifications;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Services.NonRelational;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Commands.DiscontinueProduct
{
    public class DiscontinueProductHandler : IRequestHandler<DiscontinueProductCommand, DiscontinueProductResponse>
    {
        private readonly IMediator _mediator;
        private readonly IProductService _productService;

        public DiscontinueProductHandler(IMediator mediator, IProductService productService)
        {
            _mediator = mediator;
            _productService = productService;
        }

        public async Task<DiscontinueProductResponse> Handle(DiscontinueProductCommand command, CancellationToken cancellationToken)
        {
            var validator = new DiscontinueProductValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                var domainNotification = validationResult.Errors
                    .Select(error => DomainNotification.Create(error.PropertyName, error.ErrorMessage))
                    .ToList();

                domainNotification.ForEach(async item => await _mediator.Publish(item, cancellationToken)); 

                return default!;
            }

            var product = await _productService.GetByIdAsync(command.Id);
            if (product is null)
            {
                var domainNotification = DomainNotification.Create("NotFound", $"Product with ID {command.Id} not found.");

                await _mediator.Publish(domainNotification, cancellationToken);
                return default!;
            }

            var isActiveProduct = new ActiveProductSpecification();
            if (!isActiveProduct.IsSatisfiedBy(product))
            {
                var domainNotification = DomainNotification.Create("Unauthorized", $"Product must be active to discontinue.");

                await _mediator.Publish(domainNotification, cancellationToken);
                return default!;
            }

            product.AlterStatus(ProductStatus.Discontinued);
            product.AlterDateUpdate(); 

            await _productService.UpdateAsync(command.Id, product);

            return new DiscontinueProductResponse { Success = true };
        }
    }

}
