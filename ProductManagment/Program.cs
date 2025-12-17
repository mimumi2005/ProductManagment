using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProductManagment.Domain.Entities;
using ProductManagment.Infrastructure;
using ProductManagment.Infrastructure.DbInitializer;
using ProductManagment.Infrastructure.Persistance;
using ProductManagment.WebApi.Controllers;
using ProductManagment.WebApi.Middleware;
using ProductManagment.WebAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddValidatorsFromAssemblyContaining<ProductManagment.Application.AssemblyMarker>();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblyContaining<ProductManagment.Application.AssemblyMarker>();
});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));

builder.Services.AddOpenApi();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DbInitializer.InitializeAsync(services);
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapProductEndpoints();
app.MapUserEndpoints();
app.MapAuditEndpoints();
app.Run();
