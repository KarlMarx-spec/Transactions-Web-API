using System.Transactions;

namespace Transactions_Web_API.Interfaces
{
	public interface ITransactionService
	{
		Task<List<Transaction>> GetAllTransactionsAsync(CancellationToken ct);
	}
}
