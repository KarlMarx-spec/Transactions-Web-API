using AutoMapper;
using Transactions_Web_API.Entities;

namespace Transactions_Web_API.Models.API.Requests
{
	public class AddTransactionRequest
	{
		public required Guid Id { get; init; }
		public required DateTime TransactionDate { get; init; }
		public required decimal Amount { get; init; }
	}

	public class AddTransactionRequestProfile : Profile
	{
		public AddTransactionRequestProfile()
		{
			CreateMap<AddTransactionRequest, Transaction>();
		}
	}
}
