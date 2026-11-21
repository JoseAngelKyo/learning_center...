using Cortex.Mediator.Notifications;

namespace Practica04.Shared.Domain.Model.Events;

/// <summary>Marks a domain event that can be published through the mediator.</summary>
public interface IEvent : INotification
{
}


