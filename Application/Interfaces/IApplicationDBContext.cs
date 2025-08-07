using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Interfaces
{
	public interface IApplicationDBContext
	{
		public DatabaseFacade Database { get; }

		public DbSet<Transaction> Transactions { get; set; }

		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
