using Transactions_Web_API.Entities;
using Transactions_Web_API.Models.API.Requests;
using Transactions_Web_API.Models.API.Responses;

namespace Transactions_Web_API.Interfaces
{
	public interface ITransactionService
	{
		Task<List<Transaction>> GetAllTransactionsAsync(CancellationToken ct);

		Task<Transaction> GetTransactionAsync(Guid id, CancellationToken ct);

		Task<AddTransactionResponse> AddTransactionAsync(AddTransactionRequest request,
			CancellationToken ct);
	}
}
