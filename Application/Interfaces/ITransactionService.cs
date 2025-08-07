using Application.Models.API.Requests;
using Application.Models.API.Responses;

namespace Application.Interfaces
{
	public interface ITransactionService
	{
		Task<List<TransactionResponse>> GetAllTransactionsAsync(CancellationToken ct);

		Task<TransactionResponse> GetTransactionAsync(Guid id, CancellationToken ct);

		Task<AddTransactionResponse> AddTransactionAsync(AddTransactionRequest request,
			CancellationToken ct);
	}
}
