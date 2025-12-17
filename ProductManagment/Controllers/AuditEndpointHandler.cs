using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductManagment.Application.Audits.Queries;
using ProductManagment.Application.Common;
using Swashbuckle.AspNetCore.Annotations;

namespace ProductManagment.WebApi.Controllers
{
    public static class AuditEndpointHandler
    {
        public static RouteGroupBuilder MapAuditEndpoints(this RouteGroupBuilder group)
        {
            group.MapGet("", GetAudit)
                .AllowAnonymous()
                .WithName(nameof(GetAudit))
                .Produces(StatusCodes.Status200OK, responseType: typeof(ApiResponse), contentType: "application/json")
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithMetadata(new SwaggerOperationAttribute(
                    summary: "Retrieves audits",
                    description: "Retrieve audit logs with optional date filters"
                ));

            return group;
        }

        public static void MapAuditEndpoints(this WebApplication app)
        {
            app.MapGroup("/api/audit")
                .MapAuditEndpoints()
                .WithTags("Audit");
        }

        private static async Task<IResult> GetAudit(
            IMediator mediator,
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to)
        {
            var result = await mediator.Send(new GetAuditsQuery
            {
                From = from,
                To = to
            });

            return Results.Ok(new ApiResponse("Audit logs retrieved successfully", result));
        }
    }
}