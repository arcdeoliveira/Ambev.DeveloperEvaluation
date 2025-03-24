using Ambev.DeveloperEvaluation.Application.Notifications;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Services.NonRelational;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, UpdateProductResult>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IProductService _productService;

        public UpdateProductHandler(IMapper mapper, IMediator mediator, IProductService productService)
        {
            _mapper = mapper;
            _mediator = mediator;
            _productService = productService;
        }

        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var validator = new UpdateProductValidator();
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

            var existingProduct = await _productService.CheckIfNameCanBeUpdate(command.Id, command.Name, cancellationToken);
            if (existingProduct)
            {
                var domainNotification = DomainNotification.Create("Duplicate", $"Product with name {command.Name} already exists");

                await _mediator.Publish(domainNotification, cancellationToken);
                return default!;
            }

            product.AlterStatus(command.Status);
            product.AlterPrice(command.Price);
            product.AlterName(command.Name);
            product.AlterDescription(command.Description);
            product.AlterDateUpdate(); 

            await _productService.UpdateAsync(command.Id, product);

            return _mapper.Map<UpdateProductResult>(product);
        }
    }

}
