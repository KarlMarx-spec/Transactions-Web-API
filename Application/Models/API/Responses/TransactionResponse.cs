using AutoMapper;
using Domain.Entities;

namespace Application.Models.API.Responses
{
	public class TransactionResponse
	{
		public required Guid Id { get; init; }
		public required DateTime TransactionDate { get; init; }
		public required decimal Amount { get; init; }
	}

	public class TransactionResponseProfile : Profile
	{
		public TransactionResponseProfile()
		{
			CreateMap<Transaction, TransactionResponse>();
		}
	}
}
