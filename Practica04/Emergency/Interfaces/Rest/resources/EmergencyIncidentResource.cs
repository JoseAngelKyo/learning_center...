namespace Practica04.Emergency.Interfaces.Rest.Resources;

/// <summary>Exposes only the response fields required by the emergency incident contract.</summary>
public record EmergencyIncidentResource(int Id, string GuidCode, string ResponseUnitId,
    string ReportedAtDate, string SlaDeadLineDate, decimal DispatchCostValue, string DispatchCostCurrency);





