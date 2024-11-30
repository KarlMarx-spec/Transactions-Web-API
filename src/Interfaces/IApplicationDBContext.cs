using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Transactions_Web_API.Entities;

namespace Transactions_Web_API.Interfaces
{
	public interface IApplicationDBContext
	{
		public DatabaseFacade Database { get; }

		public DbSet<Transaction> Transactions { get; set; }

		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
