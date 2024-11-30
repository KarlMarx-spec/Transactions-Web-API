using Microsoft.EntityFrameworkCore;
using Transactions_Web_API.Entities;
using Transactions_Web_API.Interfaces;

namespace Transactions_Web_API.Database
{
	public class PostgreDbContext : DbContext,
		IApplicationDBContext
	{
		public PostgreDbContext(DbContextOptions<PostgreDbContext> options)
			: base(options)
		{
		}

		public DbSet<Transaction> Transactions { get; set; }
	}
}