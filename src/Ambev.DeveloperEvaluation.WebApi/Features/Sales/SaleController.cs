using Ambev.DeveloperEvaluation.Application.Notifications;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSaleItem;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSaleByIdQuery;
using Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSaleWithPaginationQuery;
using Ambev.DeveloperEvaluation.Domain.Dtos.Sales;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Common.Response;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSaleItem;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleWithPagination;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SaleController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        private readonly DomainNotificationHandler _notifications;

        public SaleController(IMediator mediator, IMapper mapper, INotificationHandler<DomainNotification> notifications)
        {
            _mapper = mapper;
            _mediator = mediator;   
            _notifications = (DomainNotificationHandler)notifications;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSale([FromRoute] string id, CancellationToken cancellationToken)
        {
            var request = new GetSaleByIdRequest { Id = id };
            var validator = new GetSaleByIdRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var query = _mapper.Map<GetSaleByIdQuery>(request.Id);
            var result = await _mediator.Send(query, cancellationToken);

            if (_notifications.HasNotifications())
                return BadRequest(_notifications.GetNotifications());

            var response = _mapper.Map<GetSaleByIdResponse>(result);
            var message = "Sale retrieved successfully";
            return Ok(response, true, message, []);
        }

        [HttpGet]
        public async Task<IActionResult> GetSales([FromQuery] GetSaleWithPaginationRequest request, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<GetSaleWithPaginationQuery>(request);
            var response = await _mediator.Send(query, cancellationToken);

            if (_notifications.HasNotifications())
                return BadRequest(_notifications.GetNotifications());

            var salesWereFound = response.Total > 0;
            return Created(string.Empty, new PaginatedResponse<SalePaginationDto>
            {
                CurrentPage = request.PageNumber,
                Data = response.Data,
                Success = salesWereFound,
                Message = salesWereFound ? "Sales retrieved successfully." : "Sales not found.",
                TotalCount = response.Total,
                TotalPages = request.PageSize
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateSaleCommand>(request);
            command.UserId = GetCurrentUserId();

            var result = await _mediator.Send(command, cancellationToken);

            if(_notifications.HasNotifications())
                return BadRequest(_notifications.GetNotifications());

            var response = _mapper.Map<CreateSaleResponse>(result);
            var additionalText = string.IsNullOrEmpty(response.NotFoundProductMessage) && string.IsNullOrEmpty(response.IneligibleProductMessage)
               ? string.Empty
               : "some";

            var message = string.IsNullOrEmpty(additionalText) 
                ? "Sale created with sucess."
                : $@"Sale created with {additionalText} sucess.  
                    {response.NotFoundProductMessage}
                    {response.IneligibleProductMessage}";

            return Ok(response, true, message, []);
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateSale([FromBody] UpdateSaleRequest request, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<UpdateSaleCommand>(request);
            command.UserId = GetCurrentUserId();    

            var result = await _mediator.Send(command, cancellationToken);

            if (_notifications.HasNotifications())
                return BadRequest(_notifications.GetNotifications());

            var response = _mapper.Map<UpdateSaleResponse>(result);
            var additionalText = string.IsNullOrEmpty(response.WarningNotFoundProductMessage) && string.IsNullOrEmpty(response.WarningIneligibleProductMessage)
                ? string.Empty
                : "some";

            var message = string.IsNullOrEmpty(additionalText) 
                ? "Sale updated with sucess."
                : $@"Sale updated with {additionalText} sucess.  
                            {response.WarningNotFoundProductMessage}
                            {response.WarningIneligibleProductMessage}";

            return Ok(response, true, message, []);
        }

        [HttpPatch("cancel/{id}")]
        public async Task<IActionResult> CancelSale([FromRoute] string id, CancellationToken cancellationToken)
        {
            var request = new CancelSaleRequest { Id = id };
            var validator = new CancelSaleRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CancelSaleCommand>(request.Id);
            command.UserId = GetCurrentUserId();

            var result = await _mediator.Send(command, cancellationToken);

            if (_notifications.HasNotifications())
                return BadRequest(_notifications.GetNotifications());

            var messsage = result.Success ? "Sale canceled successfully" : "Error on canceling sale";

            return Ok<object?>(null, result.Success, messsage, []);
        }

        [HttpPatch("cancel/{id}/itens")]   
        public async Task<IActionResult> CancelSaleItens([FromRoute] string id, [FromQuery] CancelSaleItemRequest request, CancellationToken cancellationToken)
        {
            request.Id = id;    

            var validator = new CancelSaleItemRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CancelSaleItemCommand>(request);
            command.UserId = GetCurrentUserId();

            var result = await _mediator.Send(command, cancellationToken);

            if (_notifications.HasNotifications())
                return BadRequest(_notifications.GetNotifications());

            var response = _mapper.Map<CancelSaleItemResponse>(result);
            var message = string.IsNullOrEmpty(response.NotFoundMessage) 
                ? "Sale updated with sucess. Itens were canceled"
                : $"Sale updated with some sucess. Not all itens were canceled. \n {response.NotFoundMessage}";

            return Ok(response, true, message, []);
        }
        

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale([FromRoute] string id, CancellationToken cancellationToken)
        {
            var request = new DeleteSaleRequest { Id = id };
            var validator = new DeleteSaleRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<DeleteSaleCommand>(request.Id);
            command.UserId = GetCurrentUserId();    

            var response = await _mediator.Send(command, cancellationToken);

            if (_notifications.HasNotifications())
                return BadRequest(_notifications.GetNotifications());

            var messsage = response.Success ? "Sale deleted successfully." : "Error deleting sale.";
            return Ok<object?>(null, response.Success, messsage, []);
        }
    }
}
