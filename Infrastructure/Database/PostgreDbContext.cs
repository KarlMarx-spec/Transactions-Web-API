using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
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