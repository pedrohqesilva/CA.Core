using Api.Exceptions;
using Domain.Services.Events;
using Domain.Services.Interfaces.MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ProducesResponseType(typeof(ExeceptionResponse), StatusCodes.Status500InternalServerError)]
    public abstract class BaseController : ControllerBase
    {
        protected readonly IDomainNotificationHandler _notifications;
        protected readonly IMediatorHandler _mediator;
        protected readonly IServiceProvider _serviceProvider;

        protected BaseController(
            IDomainNotificationHandler notifications,
            IMediatorHandler mediator,
            IServiceProvider serviceProvider
        )
        {
            _notifications = notifications;
            _mediator = mediator;
            _serviceProvider = serviceProvider;
        }

        protected IList<DomainNotification> Notifications => _notifications.GetNotifications();
    }
}