using Practica04.Shared.Application.Model;
namespace Practica04.Emergency.Interfaces.Acl;

/// <summary>Defines a primitive contract for other contexts that need to create a incident.</summary>
public interface IEmergencyContextFacade
{
    Task<Result<int>> CreateEmergencyIncidentAsync(Guid reference, Guid merchantId, DateTime reportedAt,
        DateTime slaDeadLine, decimal value, string currency, CancellationToken cancellationToken = default);
}



