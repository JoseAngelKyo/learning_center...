using Practica04.Emergency.Domain.Model.ValueObjects;
namespace Practica04.Emergency.Domain.Model.Commands;

/// <summary>Contains the primitive values required to create a emergency incident.</summary>
public record CreateEmergencyIncidentCommand(Guid GuidCode, Guid ResponseUnitId,
    DateTime ReportedAt, DateTime SlaDeadLine, decimal DispatchOperationalCostValue,
    string DispatchOperationalCostCurrency, IncidentStatus Status = IncidentStatus.Pending, string? Description = null);



