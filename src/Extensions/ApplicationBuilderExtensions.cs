using Microsoft.EntityFrameworkCore;
using Transactions_Web_API.Database;

namespace Transactions_Web_API.Extensions
{
	public static class ApplicationBuilderExtensions
	{
		public static void ApplyMigrations(this IApplicationBuilder app)
		{
			using IServiceScope scope = app.ApplicationServices.CreateScope();
			using var dbContext =
				scope.ServiceProvider.GetRequiredService<PostgreDbContext>();

			dbContext.Database.Migrate();
		}
	}
}