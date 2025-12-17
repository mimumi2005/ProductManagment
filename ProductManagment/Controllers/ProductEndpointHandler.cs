using MediatR;
using ProductManagment.Application.Common;
using ProductManagment.Application.Products.Commands.CreateProduct;
using ProductManagment.Application.Products.Commands.DeleteProduct;
using ProductManagment.Application.Products.Commands.UpdateProduct;
using ProductManagment.Application.Products.Dtos;
using ProductManagment.Application.Products.Queries.GetProduct;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace ProductManagment.WebApi.Controllers
{
    public static class ProductEndpointHandler
    {
        public static RouteGroupBuilder MapProductEndpoints(this RouteGroupBuilder group)
        {
            group.MapGet("", GetProducts)
                .AllowAnonymous()
                .WithName(nameof(GetProducts))
                .Produces(StatusCodes.Status200OK, responseType: typeof(ApiResponse), contentType: "application/json")
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithMetadata(new SwaggerOperationAttribute(
                    summary: "Retrieve products",
                    description: "Fetches all existing products."
                ));

            group.MapGet("/{id}", GetProduct)
                .AllowAnonymous()
                .WithName(nameof(GetProduct))
                .Produces(StatusCodes.Status200OK, responseType: typeof(ApiResponse), contentType: "application/json")
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithMetadata(new SwaggerOperationAttribute(
                    summary: "Retrieve product",
                    description: "Fetches a single product by its unique id."
                ));

            group.MapPost("", CreateProduct)
                .RequireAuthorization("AdminOnly")
                .WithName(nameof(CreateProduct))
                .Produces(StatusCodes.Status200OK, responseType: typeof(ApiResponse), contentType: "application/json")
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithMetadata(new SwaggerOperationAttribute(
                    summary: "Create a product",
                    description: "Adds a product to the database (Admin only)."
                ));

            group.MapPut("", UpdateProduct)
                .RequireAuthorization("AdminOnly")
                .WithName(nameof(UpdateProduct))
                .Produces(StatusCodes.Status200OK, responseType: typeof(ApiResponse), contentType: "application/json")
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithMetadata(new SwaggerOperationAttribute(
                    summary: "Update product",
                    description: "Update an existing product in the database (Admin only)."
                ));

            group.MapDelete("{Id}", DeleteProduct)
                .RequireAuthorization("AdminOnly")
                .WithName(nameof(DeleteProduct))
                .Produces(StatusCodes.Status200OK, responseType: typeof(ApiResponse), contentType: "application/json")
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithMetadata(new SwaggerOperationAttribute(
                    summary: "Delete product",
                    description: "Permanently delete a product from the database (Admin only)."
                ));

            return group;
        }

        public static void MapProductEndpoints(this WebApplication app)
        {
            app.MapGroup("/api/products")
                .MapProductEndpoints()
                .WithTags("Product");
        }

        private static async Task<IResult> GetProducts(
            IMediator mediator)
        {
            var product = await mediator.Send(new GetProductsQuery());
            return Results.Ok(new ApiResponse("Products retrieved successfully", product));
        }

        private static async Task<IResult> GetProduct(
            int Id,
            IMediator mediator)
        {
            var product = await mediator.Send(new GetProductQuery(Id));
            return Results.Ok(new ApiResponse("Product by Id found", product));
        }

        private static async Task<IResult> CreateProduct(
            ProductDto product,
            IMediator mediator,
            ClaimsPrincipal user)
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Results.Unauthorized();

            var createdProduct = await mediator.Send(new CreateProductCommand(product, userId));
            return Results.Ok(new ApiResponse("Product created successfully", createdProduct));
        }

        private static async Task<IResult> UpdateProduct(
            ProductDto product,
            IMediator mediator,
            ClaimsPrincipal user)
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Results.Unauthorized();

            var updatedProduct = await mediator.Send(new UpdateProductCommand(product, userId));
            return Results.Ok(new ApiResponse("Product updated successfully", updatedProduct));
        }

        private static async Task<IResult> DeleteProduct (
            int Id,
            IMediator mediator,
            ClaimsPrincipal user)
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Results.Unauthorized();

            await mediator.Send(new DeleteProductCommand(Id, userId));
            return Results.Ok(new ApiResponse("Product deleted successfully"));
        }
    }
}
