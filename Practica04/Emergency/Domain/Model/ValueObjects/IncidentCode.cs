using Practica04.Shared.Domain.Model.ValueObjects;
namespace Practica04.Emergency.Domain.Model.ValueObjects;

/// <summary>Identifies a incident reference originating in another context.</summary>
public sealed record IncidentCode(Guid Value) : IEntityId<Guid>;



