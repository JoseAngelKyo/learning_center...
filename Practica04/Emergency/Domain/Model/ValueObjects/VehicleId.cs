using Practica04.Shared.Domain.Model.ValueObjects;
namespace Practica04.Emergency.Domain.Model.ValueObjects;

/// <summary>Identifies a merchant account originating in another context.</summary>
public sealed record VehicleId(Guid Value) : IEntityId<Guid>;



