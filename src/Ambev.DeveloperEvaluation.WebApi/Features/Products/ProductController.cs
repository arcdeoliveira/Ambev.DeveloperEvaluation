using Ambev.DeveloperEvaluation.Application.Notifications;
using Ambev.DeveloperEvaluation.Application.Products.Commands.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.Commands.DeleteProduct;
using Ambev.DeveloperEvaluation.Application.Products.Commands.DiscontinueProduct;
using Ambev.DeveloperEvaluation.Application.Products.Commands.UpdateProduct;
using Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductByIdQuery;
using Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductWithPaginationQuery;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSaleByIdQuery;
using Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSaleWithPaginationQuery;
using Ambev.DeveloperEvaluation.Domain.Dtos.Products;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Common.Response;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.DiscontinueProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductById;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductWithPagination;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products
{
    [Authorize(Policy = "AdminOrManager")]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        private readonly DomainNotificationHandler _notifications;

        public ProductController(IMediator mediator, IMapper mapper, INotificationHandler<DomainNotification> notifications)
        {
            _mapper = mapper;
            _mediator = mediator;   
            _notifications = (DomainNotificationHandler)notifications;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] string id, CancellationToken cancellationToken)
        {
            var request = new GetProductByIdRequest { Id = id };
            var validator = new GetProductByIdRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var query = _mapper.Map<GetProductByIdQuery>(request.Id);
            var result = await _mediator.Send(query, cancellationToken);

            if (_notifications.HasNotifications())
                return BadRequest(_notifications.GetNotifications());

            var response = _mapper.Map<GetProductByIdResponse>(result);
            var message = "Product retrieved successfully";
            return Ok(response, true, message, []);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] GetProductWithPaginationRequest request, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<GetProductWithPaginationQuery>(request);
            var response = await _mediator.Send(query, cancellationToken);

            if (_notifications.HasNotifications())
                return BadRequest(_notifications.GetNotifications());

            var productsWereFound = response.Total > 0;
            return Created(string.Empty, new PaginatedResponse<ProductPaginationDto>
            {
                CurrentPage = request.PageNumber,
                Data = response.Data,
                Success = productsWereFound,
                Message = productsWereFound ? "Products retrieved successfully." : "Products not found.",
                TotalCount = response.Total,
                TotalPages = request.PageSize
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateProductRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateProductCommand>(request);
            command.UserId = GetCurrentUserId();

            var result = await _mediator.Send(command, cancellationToken);

            if(_notifications.HasNotifications())
                return BadRequest(_notifications.GetNotifications());

            var response = _mapper.Map<CreateProductResponse>(result);
            var message = $"Product {result.Name} created successfully";
            return Ok(response, true, message, []);
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
        {
            var validator = new UpdateProductRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<UpdateProductCommand>(request);
            var result = await _mediator.Send(command, cancellationToken);

            if (_notifications.HasNotifications())
                return BadRequest(_notifications.GetNotifications());

            var response = _mapper.Map<UpdateProductResponse>(result);
            var message = "Product updated with sucess.";
            return Ok(response, true, message, []);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> DiscontinueProduct([FromRoute] string id, CancellationToken cancellationToken)
        {
            var request = new DiscontinueProductRequest { Id = id };
            var validator = new DiscontinueProductRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<DiscontinueProductCommand>(request.Id);
            var result = await _mediator.Send(command, cancellationToken);

            if (_notifications.HasNotifications())
                return BadRequest(_notifications.GetNotifications());

            var messsage = result.Success ? "Product discontinued successfully" : "Error on discontinued product";           

            return Ok<object?>(null, result.Success, messsage, []);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> InactiveProdut([FromRoute] string id, CancellationToken cancellationToken)
        {
            var request = new DeleteProductRequest { Id = id };
            var validator = new DeleteProductRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<DeleteProductCommand>(request.Id);
            var result = await _mediator.Send(command, cancellationToken);

            if (_notifications.HasNotifications())
                return BadRequest(_notifications.GetNotifications());

            var messsage = result.Success ? "Product deleted successfully." : "Error on delete product.";
            return Ok<object?>(null, result.Success, messsage, []);
        }
    }
}
