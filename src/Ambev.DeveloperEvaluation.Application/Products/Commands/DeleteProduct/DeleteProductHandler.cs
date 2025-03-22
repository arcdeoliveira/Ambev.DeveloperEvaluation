using Ambev.DeveloperEvaluation.Application.Notifications;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Services;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, DeleteProductResponse>
    {
        private readonly IMediator _mediator;
        private readonly IProductService _productService;

        public DeleteProductHandler(IMediator mediator, IProductService productService)
        {
            _mediator = mediator;
            _productService = productService;
        }

        public async Task<DeleteProductResponse> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var validator = new DeleteProductValidator();
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

            product.AlterStatus(ProductStatus.Inactive);
            product.AlterDateUpdate();

            await _productService.UpdateAsync(command.Id, product);

            return new DeleteProductResponse { Success = true };
        }
    }

}
