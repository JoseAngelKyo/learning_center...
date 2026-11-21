namespace Practica04.Shared.Domain.Model.ValueObjects;

/// <summary>Represents the report timestamp and its strictly later SLA deadline.</summary>
public sealed record DateIncident
{
    public DateTime ReportedAt { get; }
    public DateTime SlaDeadLine { get; }

    /// <summary>Creates the incident response window.</summary>
    /// <param name="reportedAt">The incident report timestamp.</param>
    /// <param name="slaDeadLine">The response deadline.</param>
    /// <exception cref="ArgumentException">The SLA deadline is not after the report timestamp.</exception>
    public DateIncident(DateTime reportedAt, DateTime slaDeadLine)
    {
        if (slaDeadLine <= reportedAt)
            throw new ArgumentException("The SLA deadline must be after the report timestamp.");
        ReportedAt = reportedAt;
        SlaDeadLine = slaDeadLine;
    }

    /// <summary>Determines whether the supplied timestamp exceeds the SLA deadline.</summary>
    /// <param name="currentTime">The timestamp to evaluate in the same time basis as the deadline.</param>
    /// <returns>True only if currentTime is strictly after SlaDeadLine.</returns>
    public bool IsSlaBreached(DateTime currentTime) => currentTime > SlaDeadLine;
}
