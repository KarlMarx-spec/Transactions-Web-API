using Microsoft.EntityFrameworkCore;
using Transactions_Web_API.Entities;
using Transactions_Web_API.Interfaces;

namespace Transactions_Web_API.Services
{
	public class TransactionService
	{
		private readonly IApplicationDBContext _context;

		public TransactionService(IApplicationDBContext context)
		{
			_context = context;
		}

		public async Task<List<Transaction>> GetAllTransactionsAsync(CancellationToken ct)
		{
			return await _context.Transactions
				.AsNoTracking()
				.ToListAsync(ct);
		}
	}
}
