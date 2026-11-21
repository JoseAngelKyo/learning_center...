using Practica04.Emergency.Domain.Model.Aggregates;
using Practica04.Emergency.Domain.Model.ValueObjects;
using Practica04.Shared.Domain.Repositories;
namespace Practica04.Emergency.Domain.Repositories;

/// <summary>Defines persistence operations for emergency incidents.</summary>
public interface IEmergencyIncidentRepository : IBaseRepository<EmergencyIncident>
{
    Task<bool> ExistsByGuidCodeAsync(IncidentCode reference, CancellationToken cancellationToken = default);
}



