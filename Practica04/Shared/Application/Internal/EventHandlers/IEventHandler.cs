using Cortex.Mediator.Notifications;
using Practica04.Shared.Domain.Model.Events;

namespace Practica04.Shared.Application.Internal.EventHandlers;

/// <summary>Defines a handler for domain events published through the mediator.</summary>
/// <typeparam name="TEvent">The domain event handled by this contract.</typeparam>
public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
}


