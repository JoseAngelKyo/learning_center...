using Practica04.Shared.Domain.Model.Errors;
namespace Practica04.Emergency.Domain.Model.Errors;

/// <summary>Defines errors for emergency incident creation.</summary>
public static class EmergencyErrors
{
    public static Error DuplicateReference => Error.Conflict("EmergencyIncident.DuplicateReference", "An emergency incident with this identifier already exists.");
    public static Error Invalid(string parameter) => Error.Validation("EmergencyIncident.Invalid", parameter switch
    {
        "slaDeadLine" => "The SLA deadline must be after the report timestamp.",
        "value" => "Dispatch cost value must be greater than or equal to zero.",
        "currency" => "Currency must not be null, empty or whitespace.",
        "status" => "Status must be Pending (0), Reported (1), Attended (2) or Cancelled (3).",
        _ => "The request contains invalid or missing fields."
    });
}



