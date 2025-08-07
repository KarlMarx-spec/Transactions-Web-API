using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Transactions_Web_API.src.Extensions
{
	public static class ApplicationBuilderExtensions
	{
		public static void ApplyMigrations(this IApplicationBuilder app)
		{
			using IServiceScope scope = app.ApplicationServices.CreateScope();
			using var dbContext = scope.ServiceProvider.GetRequiredService<PostgreDbContext>();

			dbContext.Database.Migrate();
		}
	}
}