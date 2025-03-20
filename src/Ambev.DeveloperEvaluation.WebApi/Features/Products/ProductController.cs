using Ambev.DeveloperEvaluation.Application.Notifications;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.WebApi.Common;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : BaseController
    {
       
        private readonly DomainNotificationHandler _notifications;

        public ProductController(INotificationHandler<DomainNotification> notifications)
        {

            _notifications = (DomainNotificationHandler?)notifications ?? new DomainNotificationHandler();
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct([FromRoute] string id,  CancellationToken cancellationToken)
        {
            if (_notifications.HasNotifications())
                return BadRequest(_notifications.GetNotifications());

            return Ok();
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult CreateProduct()
        {
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateProduct()
        {
            return Ok();
        }

        [HttpPatch("{id}")]
        public IActionResult DiscontinueProduct([FromRoute] string id, CancellationToken cancellationToken)
        {
            if (_notifications.HasNotifications())
                return BadRequest(_notifications.GetNotifications());

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult InactiveProdut([FromRoute] string id, CancellationToken cancellationToken)
        {
            if (_notifications.HasNotifications())
                return BadRequest(_notifications.GetNotifications());

            return Ok();
        }
    }
}
