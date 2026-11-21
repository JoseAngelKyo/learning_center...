namespace Practica04.Shared.Domain.Model.Entities;

/// <summary>Defines audit timestamps managed by the persistence layer.</summary>
public interface IAuditableEntity
{
    DateTimeOffset? CreatedAt { get; set; }
    
    DateTimeOffset? UpdatedAt { get; set; }
}


