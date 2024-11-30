using Microsoft.EntityFrameworkCore;
using Transactions_Web_API.Entities;
using Transactions_Web_API.Interfaces;

namespace Transactions_Web_API
{
	public class PostgreDbContext : DbContext,
		IApplicationDBContext
	{
		public PostgreDbContext(DbContextOptions options) : base(options) { }

		public DbSet<Transaction> Transactions { get; set; }
	}
}