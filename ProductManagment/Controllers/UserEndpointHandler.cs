using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ProductManagment.Domain.Entities;
using ProductManagment.Application.Common;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ProductManagment.WebApi.Controllers
{
    public static class UserEndpointHandler
    {
        public static RouteGroupBuilder MapUserEndpoints(this RouteGroupBuilder group)
        {
            group.MapPost("/register", RegisterUser)
                .AllowAnonymous()
                .WithName(nameof(RegisterUser))
                .Produces(StatusCodes.Status200OK, responseType: typeof(ApiResponse), contentType: "application/json")
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithMetadata(new SwaggerOperationAttribute(
                    summary: "Register a new user",
                    description: "Creates a new user account"
                ));

            group.MapPost("/login", LoginUser)
                .AllowAnonymous()
                .WithName(nameof(LoginUser))
                .Produces(StatusCodes.Status200OK, responseType: typeof(ApiResponse), contentType: "application/json")
                .ProducesProblem(StatusCodes.Status401Unauthorized)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithMetadata(new SwaggerOperationAttribute(
                    summary: "Login user",
                    description: "Authenticates a user and returns a JWT token"
                ));

            group.MapPost("/logout", LogoutUser)
                .WithName(nameof(LogoutUser))
                .Produces(StatusCodes.Status200OK, responseType: typeof(ApiResponse), contentType: "application/json")
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithMetadata(new SwaggerOperationAttribute(
                    summary: "Logout user",
                    description: "Logs out the currently authenticated user"
                ));

            group.MapGet("/roles", GetRoles)
                .WithName(nameof(GetRoles))
                .Produces(StatusCodes.Status200OK, responseType: typeof(ApiResponse), contentType: "application/json")
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithMetadata(new SwaggerOperationAttribute(
                    summary: "Get roles",
                    description: "Retrieves all roles"
                ));

            return group;
        }

        public static void MapUserEndpoints(this WebApplication app)
        {
            app.MapGroup("/api/user")
                .MapUserEndpoints()
                .WithTags("User");
        }

        private static async Task<IResult> RegisterUser(
            UserManager<ApplicationUser> userManager,
            RegisterDto dto)
        {
            var user = new ApplicationUser { UserName = dto.Email, Email = dto.Email };
            var result = await userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                return Results.BadRequest(new ApiResponse("Failed to register user", result.Errors.Select(e => e.Description)));

            return Results.Ok(new ApiResponse("User registered successfully"));
        }

        private static async Task<IResult> LoginUser(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IConfiguration config,
            LoginDto dto)
        {
            var result = await signInManager.PasswordSignInAsync(dto.Email, dto.Password, false, false);
            if (!result.Succeeded) return Results.Unauthorized();

            var user = await userManager.FindByEmailAsync(dto.Email);
            var roles = await userManager.GetRolesAsync(user);

            var jwt = GenerateJwtToken(user, roles, config);

            return Results.Ok(new ApiResponse("Login successful", jwt));
        }

        private static async Task<IResult> LogoutUser(
            SignInManager<ApplicationUser> signInManager)
        {
            await signInManager.SignOutAsync();
            return Results.Ok(new ApiResponse("Logged out successfully"));
        }

        private static async Task<IResult> GetRoles(
            RoleManager<IdentityRole> roleManager)
        {
            var roles = roleManager.Roles.Select(r => r.Name).ToList();
            return Results.Ok(new ApiResponse("Roles retrieved successfully", roles));
        }

        private static string GenerateJwtToken(ApplicationUser user, IList<string> roles, IConfiguration config)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(config["Jwt:ExpiresInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public record RegisterDto(string Email, string Password);
    public record LoginDto(string Email, string Password);
}
