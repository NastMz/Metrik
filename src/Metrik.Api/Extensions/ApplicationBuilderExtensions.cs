using Metrik.Api.Middleware;
using Metrik.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Metrik.Api.Extensions
{
    /// <summary>
    /// Extensions for the application builder.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Applies pending migrations to the database.
        /// </summary>
        /// <param name="app">The application builder.</param>
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            dbContext.Database.Migrate();
        }

        /// <summary>
        /// Configures the application to use a custom exception handler middleware.
        /// </summary>
        /// <param name="app">The application builder.</param>
        public static void UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
