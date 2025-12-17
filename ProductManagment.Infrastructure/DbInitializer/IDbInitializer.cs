namespace ProductManagment.Infrastructure.DbInitializer;

/// <summary>
/// Defines a contract for database initialization logic,
/// including applying migrations and seeding required data.
/// </summary>
public interface IDbInitializer
{
    /// <summary>
    /// Initializes the database by applying migrations and seeding essential data.
    /// </summary>
    void InitializeAsync(IServiceProvider serviceProvider);
}
