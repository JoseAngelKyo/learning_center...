using Microsoft.EntityFrameworkCore;
using Practica04.Emergency.Domain.Model.Aggregates;
using Practica04.Emergency.Domain.Model.ValueObjects;
using Practica04.Emergency.Domain.Repositories;
using Practica04.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Practica04.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
namespace Practica04.Emergency.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>Implements emergency incident persistence and reference lookups.</summary>
public class EmergencyIncidentRepository(AppDbContext context) : BaseRepository<EmergencyIncident>(context), IEmergencyIncidentRepository
{
    public Task<bool> ExistsByGuidCodeAsync(IncidentCode reference, CancellationToken cancellationToken = default) =>
        Context.Set<EmergencyIncident>().AnyAsync(incident => incident.GuidCode == reference, cancellationToken);
}



