using Practica04.Emergency.Application.CommandServices;
using Practica04.Emergency.Domain.Model.Commands;
using Practica04.Emergency.Interfaces.Acl;
using Practica04.Shared.Application.Model;
namespace Practica04.Emergency.Application.Acl;

/// <summary>Translates primitive cross-context calls without exposing the emergency aggregate.</summary>
public class EmergencyContextFacade(IEmergencyIncidentCommandService commandService) : IEmergencyContextFacade
{
    public async Task<Result<int>> CreateEmergencyIncidentAsync(Guid reference, Guid merchantId, DateTime reportedAt,
        DateTime slaDeadLine, decimal value, string currency, CancellationToken cancellationToken = default)
    {
        var result = await commandService.Handle(new CreateEmergencyIncidentCommand(reference, merchantId,
            reportedAt, slaDeadLine, value, currency), cancellationToken);
        return result.IsSuccess ? Result<int>.Success(result.Value!.Id) : Result<int>.Failure(result.DomainError);
    }
}



