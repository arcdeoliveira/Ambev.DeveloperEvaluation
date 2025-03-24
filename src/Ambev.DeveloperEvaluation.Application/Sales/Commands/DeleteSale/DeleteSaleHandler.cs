using Ambev.DeveloperEvaluation.Application.Notifications;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Services.NonRelational;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Services.Relational;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.DeleteSale
{
    public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResponse>
    {
        private readonly IMediator _mediator;
        private readonly ISaleService _saleService;
        private readonly IUserService _userService;

        public DeleteSaleHandler(IMediator mediator, ISaleService saleService, IUserService userService)
        {
            _mediator = mediator;
            _saleService = saleService; 
            _userService = userService;
        }

        public async Task<DeleteSaleResponse> Handle(DeleteSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new DeleteSaleValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                var domainNotification = validationResult.Errors
                    .Select(error => DomainNotification.Create(error.PropertyName, error.ErrorMessage))
                    .ToList();

                domainNotification.ForEach(async item => await _mediator.Publish(item, cancellationToken));
                return default!;
            }

            var userIsAdmin = await _userService.UserIsRoleAdminAsync(command.UserId, cancellationToken);
            if (!userIsAdmin)
            {
                var domainNotification = DomainNotification.Create("Unauthorized", "User is not authorized to delete a sale. Only admin can.");
                await _mediator.Publish(domainNotification, cancellationToken);
                return default!;
            }

            var saleExist = await _saleService.DocumentExist(command.Id, cancellationToken);
            if (saleExist)
            {
                var domainNotification = DomainNotification.Create("NotFound", $"Sale with ID {command.Id} not found.");

                await _mediator.Publish(domainNotification, cancellationToken);
                return default!;
            }

            await _saleService.DeleteAsync(command.Id);
            return new DeleteSaleResponse { Success = true };
        }
    }

}
