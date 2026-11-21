using Cortex.Mediator.Commands;
using Cortex.Mediator.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Practica04.Emergency.Application.Acl;
using Practica04.Emergency.Application.CommandServices;
using Practica04.Emergency.Application.Internal.CommandServices;
using Practica04.Emergency.Domain.Repositories;
using Practica04.Emergency.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Practica04.Emergency.Interfaces.Acl;
using Practica04.Shared.Domain.Repositories;
using Practica04.Shared.Infrastructure.Interfaces.AspNetCore.Configuration;
using Practica04.Shared.Infrastructure.Mediator.Cortex.Configuration;
using Practica04.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Practica04.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;
using Practica04.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Practica04.Shared.Infrastructure.Pipeline.Middleware.Components;
using Scalar.AspNetCore;
using SharedProblemDetailsFactory = Practica04.Shared.Interfaces.Rest.ProblemDetails.ProblemDetailsFactory;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));
builder.Services.AddOpenApi();
builder.Services.AddLocalization();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddScoped<SharedProblemDetailsFactory>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<DispatchDomainEventsInterceptor>();
builder.Services.AddDbContext<AppDbContext>((services, options) =>
{
    var connection = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrWhiteSpace(connection)) throw new InvalidOperationException("Database connection is not configured.");
    options.UseMySQL(connection).AddInterceptors(services.GetRequiredService<DispatchDomainEventsInterceptor>());
});
builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(LoggingCommandBehavior<>));
builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(ExceptionHandlingCommandBehavior<>));
builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(TransactionCommandBehavior<>));
builder.Services.AddCortexMediator([typeof(Program)]);
builder.Services.AddScoped<IEmergencyIncidentRepository, EmergencyIncidentRepository>();
builder.Services.AddScoped<IEmergencyIncidentCommandService, EmergencyIncidentCommandService>();
builder.Services.AddScoped<IEmergencyContextFacade, EmergencyContextFacade>();
var app = builder.Build();
app.UseExceptionHandler();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
else app.UseHttpsRedirection();
app.MapControllers();
app.Run();

