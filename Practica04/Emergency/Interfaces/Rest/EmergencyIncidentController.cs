using Microsoft.AspNetCore.Mvc;
using Practica04.Emergency.Application.CommandServices;
using Practica04.Emergency.Interfaces.Rest.Resources;
using Practica04.Emergency.Interfaces.Rest.Transform;
using Practica04.Shared.Domain.Model.Errors;
namespace Practica04.Emergency.Interfaces.Rest;

/// <summary>Exposes emergency incident creation.</summary>
[ApiController]
[Route("api/v1/emergency-incidents")]
public class EmergencyIncidentController(IEmergencyIncidentCommandService commandService) : ControllerBase
{
    /// <summary>Creates an emergency incident with a unique identifier.</summary>
    /// <param name="resource">The primitive fields describing the emergency incident.</param>
    /// <param name="cancellationToken">The request cancellation token.</param>
    /// <returns>The generated identifier and public incident data, or Problem Details.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(EmergencyIncidentResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post([FromBody] CreateEmergencyIncidentResource resource, CancellationToken cancellationToken)
    {
        var result = await commandService.Handle(EmergencyIncidentResourceAssembler.ToCommand(resource), cancellationToken);
        if (result.IsFailure)
            return Problem(statusCode: result.DomainError.Type == EErrorType.Conflict ? 409 : 400,
                title: result.DomainError.Code, detail: result.DomainError.Message);
        var response = EmergencyIncidentResourceAssembler.ToResource(result.Value!);
        return Created($"{Request.Path}/{response.Id}", response);
    }
}


