using Ambev.DeveloperEvaluation.Application.Notifications;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Services.NonRelational;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Commands.CreateProduct
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IProductService _productService;   

        public CreateProductHandler(IMapper mapper, IMediator mediator, IProductService productService)
        {
            _mapper = mapper;   
            _mediator = mediator;
            _productService = productService;
        }

        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
           var validator = new CreateProductValidator();
           var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                var domainNotification = validationResult.Errors
                    .Select(error => DomainNotification.Create(error.PropertyName, error.ErrorMessage))
                    .ToList();

                await _mediator.Publish(domainNotification, cancellationToken);
                return default!;
            }

            var existingProduct = await _productService.ProductNameAlreadyExist(command.Name, cancellationToken);    
            if(existingProduct)
            {
                var domainNotification = DomainNotification.Create("Duplicate", $"Product with name {command.Name} already exists");

                await _mediator.Publish(domainNotification, cancellationToken);
                return default!;
            }

            var product = _mapper.Map<Product>(command);

            await _productService.CreateAsync(product); 

            return _mapper.Map<CreateProductResult>(product);
        }
    }
}
