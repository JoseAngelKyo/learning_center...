using Practica04.Emergency.Domain.Model.Aggregates;
using Practica04.Emergency.Domain.Model.Commands;
using Practica04.Shared.Application.Model;
namespace Practica04.Emergency.Application.CommandServices;

/// <summary>Defines the emergency incident creation use case.</summary>
public interface IEmergencyIncidentCommandService
{
    Task<Result<EmergencyIncident>> Handle(CreateEmergencyIncidentCommand command, CancellationToken cancellationToken = default);
}



