using Microsoft.OpenApi;
using System.Reflection;
using System.Text;

public static class OpenApiExtensions
{
    public static IServiceCollection AddOpenApi(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductManagment API", Version = "v1" });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

            c.EnableAnnotations();
        });
        return services;
    }

    public static WebApplication MapOpenApi(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductManagment API v1");
            c.RoutePrefix = "swagger";
        });

        app.MapGet("/", (IEnumerable<EndpointDataSource> sources) =>
        {
            var endpoints = sources
                .SelectMany(s => s.Endpoints)
                .OfType<RouteEndpoint>()
                .Select(e =>
                {
                    var methods = e.Metadata.OfType<HttpMethodMetadata>().FirstOrDefault()?.HttpMethods ?? new List<string> { "ANY" };
                    var route = e.RoutePattern.RawText ?? "/";
                    return new { Methods = string.Join(", ", methods), Route = route, Name = e.DisplayName ?? string.Empty };
                })
                .OrderBy(e => e.Route)
                .ToList();

            var sb = new StringBuilder();
            sb.Append("<!doctype html><html><head><meta charset='utf-8'><title>Endpoints</title>");
            sb.Append("<style>body{font-family:Segoe UI,Roboto,Arial;margin:20px}table{border-collapse:collapse;width:100%}th,td{border:1px solid #ccc;padding:8px;text-align:left}th{background:#f4f4f4}</style>");
            sb.Append("</head><body>");
            sb.Append("<h1>ProductManagment API - Active Endpoints</h1>");
            sb.Append("<p><a href='/swagger'>Open Swagger UI</a></p>");
            sb.Append("<table><thead><tr><th>Methods</th><th>Route</th><th>Name</th></tr></thead><tbody>");
            foreach (var ep in endpoints)
            {
                sb.Append($"<tr><td>{System.Net.WebUtility.HtmlEncode(ep.Methods)}</td><td>{System.Net.WebUtility.HtmlEncode(ep.Route)}</td><td>{System.Net.WebUtility.HtmlEncode(ep.Name)}</td></tr>");
            }
            sb.Append("</tbody></table></body></html>");

            return Results.Content(sb.ToString(), "text/html");
        }).WithName("RootEndpoints")
          .ExcludeFromDescription();

        return app;
    }
}