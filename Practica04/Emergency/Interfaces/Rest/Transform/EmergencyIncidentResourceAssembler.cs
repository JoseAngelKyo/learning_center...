using System.Globalization;
using Practica04.Emergency.Domain.Model.Aggregates;
using Practica04.Emergency.Domain.Model.Commands;
using Practica04.Emergency.Interfaces.Rest.Resources;
namespace Practica04.Emergency.Interfaces.Rest.Transform;

/// <summary>Translates primitive request fields into commands and aggregates into public resources.</summary>
public static class EmergencyIncidentResourceAssembler
{
    public static CreateEmergencyIncidentCommand ToCommand(CreateEmergencyIncidentResource resource) =>
        new(resource.GuidCode!.Value, resource.ResponseUnitId!.Value,
            resource.ReportedAt!.Value, resource.SlaDeadLine!.Value,
            resource.DispatchOperationalCostValue!.Value, resource.DispatchOperationalCostCurrency, resource.Status, resource.Description);

    public static EmergencyIncidentResource ToResource(EmergencyIncident incident) =>
        new(incident.Id, incident.GuidCode.Value.ToString(), incident.ResponseUnitId.Value.ToString(),
            incident.ResponseWindow.ReportedAt.ToString("O", CultureInfo.InvariantCulture),
            incident.ResponseWindow.SlaDeadLine.ToString("O", CultureInfo.InvariantCulture),
            incident.DispatchOperationalCost.Value, incident.DispatchOperationalCost.Currency);
}




