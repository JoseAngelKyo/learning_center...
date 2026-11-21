namespace Practica04.Emergency.Domain.Model.ValueObjects;

/// <summary>Defines the supported incident states; new incidents default to Pending.</summary>
public enum IncidentStatus { Pending = 0, Reported = 1, Attended = 2, Cancelled = 3 }

