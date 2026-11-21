using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Practica04.Emergency.Domain.Model.Aggregates;
using Practica04.Emergency.Domain.Model.ValueObjects;
using Practica04.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.ValueConverters;
namespace Practica04.Emergency.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

/// <summary>Maps the incident aggregate and its value objects using Fluent API.</summary>
public class EmergencyIncidentConfiguration : IEntityTypeConfiguration<EmergencyIncident>
{
    public void Configure(EntityTypeBuilder<EmergencyIncident> builder)
    {
        builder.ToTable("emergency_incidents");
        builder.HasKey(incident => incident.Id);
        builder.Property(incident => incident.Id).ValueGeneratedOnAdd();
        builder.Property(incident => incident.GuidCode)
            .HasConversion(new StronglyTypedIdConverter<IncidentCode, Guid>(value => new IncidentCode(value)))
            .IsRequired();
        builder.HasIndex(incident => incident.GuidCode).IsUnique();
        builder.Property(incident => incident.ResponseUnitId)
            .HasConversion(new StronglyTypedIdConverter<VehicleId, Guid>(value => new VehicleId(value)))
            .IsRequired();
        builder.Property(incident => incident.Status).HasConversion<int>().IsRequired();
        builder.Property(incident => incident.Description).IsRequired(false);
        builder.Ignore(incident => incident.DomainEvents);
        builder.OwnsOne(incident => incident.ResponseWindow, window =>
        {
            window.WithOwner().HasForeignKey("Id");
            window.Property<int>("Id").HasColumnName("id");
            window.Property(value => value.ReportedAt).HasColumnName("reported_at_date").IsRequired();
            window.Property(value => value.SlaDeadLine).HasColumnName("sla_dead_line_date").IsRequired();
        });
        builder.Navigation(incident => incident.ResponseWindow).IsRequired();
        builder.OwnsOne(incident => incident.DispatchOperationalCost, amount =>
        {
            amount.WithOwner().HasForeignKey("Id");
            amount.Property<int>("Id").HasColumnName("id");
            amount.Property(value => value.Value).HasColumnName("dispatch_cost_value").HasPrecision(65, 30).IsRequired();
            amount.Property(value => value.Currency).HasColumnName("dispatch_cost_currency").IsRequired();
        });
        builder.Navigation(incident => incident.DispatchOperationalCost).IsRequired();
    }
}




