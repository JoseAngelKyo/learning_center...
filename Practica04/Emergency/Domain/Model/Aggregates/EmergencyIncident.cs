using Practica04.Emergency.Domain.Model.Commands;
using Practica04.Emergency.Domain.Model.ValueObjects;
using Practica04.Shared.Domain.Model.Aggregates.Internal;
using Practica04.Shared.Domain.Model.Entities;
using Practica04.Shared.Domain.Model.ValueObjects;
namespace Practica04.Emergency.Domain.Model.Aggregates;

/// <summary>Represents an auditable emergency incident and its business invariants.</summary>
public class EmergencyIncident : AggregateRoot, IAuditableEntity
{
    public int Id { get; private set; }
    public IncidentCode GuidCode { get; private set; } = null!;
    public VehicleId ResponseUnitId { get; private set; } = null!;
    public DateIncident ResponseWindow { get; private set; } = null!;
    public MonetaryAmount DispatchOperationalCost { get; private set; } = null!;
    public IncidentStatus Status { get; private set; } = IncidentStatus.Pending;
    public string? Description { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    private EmergencyIncident() { }

    /// <summary>Creates a incident using validated value objects.</summary>
    /// <param name="command">The creation data.</param>
    /// <exception cref="ArgumentException">The status or a value object is invalid.</exception>
    public EmergencyIncident(CreateEmergencyIncidentCommand command)
    {
        if (!Enum.IsDefined(command.Status))
            throw new ArgumentException("Status must be Pending, Reported, Attended or Cancelled.", "status");
        GuidCode = new IncidentCode(command.GuidCode);
        ResponseUnitId = new VehicleId(command.ResponseUnitId);
        ResponseWindow = new DateIncident(command.ReportedAt, command.SlaDeadLine);
        DispatchOperationalCost = new MonetaryAmount(command.DispatchOperationalCostValue, command.DispatchOperationalCostCurrency);
        Status = command.Status;
        Description = command.Description;
    }
    /// <summary>Calculates the incident dispatch cost with a priority surcharge.</summary>
    /// <param name="factor">The multiplier applied to the dispatch cost.</param>
    /// <returns>A new monetary amount without modifying the stored base cost.</returns>
    public MonetaryAmount ApplyPrioritySurcharge(decimal factor) => DispatchOperationalCost.ApplyPrioritySurcharge(factor);
}




