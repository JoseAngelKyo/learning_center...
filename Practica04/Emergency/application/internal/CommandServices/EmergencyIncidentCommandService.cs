using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Practica04.Emergency.Application.CommandServices;
using Practica04.Emergency.Domain.Model.Aggregates;
using Practica04.Emergency.Domain.Model.Commands;
using Practica04.Emergency.Domain.Model.Errors;
using Practica04.Emergency.Domain.Repositories;
using Practica04.Shared.Application.Model;
using Practica04.Shared.Domain.Repositories;
namespace Practica04.Emergency.Application.Internal.CommandServices;

/// <summary>Validates, checks uniqueness and persists emergency incidents.</summary>
public class EmergencyIncidentCommandService(IEmergencyIncidentRepository repository, IUnitOfWork unitOfWork)
    : IEmergencyIncidentCommandService
{
    public async Task<Result<EmergencyIncident>> Handle(CreateEmergencyIncidentCommand command, CancellationToken cancellationToken = default)
    {
        EmergencyIncident incident;
        try { incident = new EmergencyIncident(command); }
        catch (ArgumentException exception)
        {
            return Result<EmergencyIncident>.Failure(EmergencyErrors.Invalid(exception.Message.StartsWith("The SLA deadline must be after the report timestamp.") ? "slaDeadLine" : exception.ParamName ?? string.Empty));
        }
        if (await repository.ExistsByGuidCodeAsync(incident.GuidCode, cancellationToken))
            return Result<EmergencyIncident>.Failure(EmergencyErrors.DuplicateReference);
        try
        {
            await repository.AddAsync(incident, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
        }
        catch (DbUpdateException exception) when (exception.InnerException is MySqlException { Number: 1062 })
        {
            return Result<EmergencyIncident>.Failure(EmergencyErrors.DuplicateReference);
        }
        return Result<EmergencyIncident>.Success(incident);
    }
}




