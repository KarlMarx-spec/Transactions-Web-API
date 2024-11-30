using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Transactions_Web_API.Entities;
using Transactions_Web_API.Exceptions;
using Transactions_Web_API.Interfaces;
using Transactions_Web_API.Models.API.Requests;
using Transactions_Web_API.Models.API.Responses;

namespace Transactions_Web_API.Services
{
	public class TransactionService : ITransactionService
	{
		private readonly IApplicationDBContext _context;
		private readonly IMapper _mapper;
		private readonly IConfiguration _configuration;

		public TransactionService(IApplicationDBContext context,
			IMapper mapper,
			IConfiguration configuration)
		{
			_context = context;
			_mapper = mapper;
			_configuration = configuration;
		}

		public async Task<List<Transaction>> GetAllTransactionsAsync(CancellationToken ct)
		{
			return await _context.Transactions
				.AsNoTracking()
				.ToListAsync(ct);
		}

		public async Task<Transaction> GetTransactionAsync(Guid id, CancellationToken ct)
		{
			return await _context.Transactions
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id, ct)
				?? throw new EntityNotFoundException<Transaction>(id);
		}

		public async Task<AddTransactionResponse> AddTransactionAsync(
			AddTransactionRequest request,
			CancellationToken ct)
		{
			if (await _context.Transactions.CountAsync(ct) >=
					_configuration.GetValue<int>("MaxTransactionCount"))
			{
				throw new BusinessLogicException(
					title: "Достигнуто предельное количество транзакций",
					details: $"Одновременное количество хранимых транзакций в БД не должно " +
					$"превышать {_configuration.GetValue<int>("MaxTransactionsCount")}");
			}
			if (request.TransactionDate.ToUniversalTime() > DateTime.UtcNow)
			{
				throw new BusinessLogicException(
					title: "Некорректная входящая транзакция",
					details: "Дата и время транзакции не могут быть в будущем");
			}
			if (request.Amount <= 0)
			{
				throw new BusinessLogicException(
					title: "Некорректная входящая транзакция",
					details: "Cумма транзакции должна быть больше 0");
			}

			if (!await _context.Transactions.AnyAsync(x => x.Id == request.Id, ct))
			{
				var transaction = _mapper.Map<Transaction>(request);
				await _context.Transactions.AddAsync(transaction, ct);
				await _context.SaveChangesAsync(ct);
			}

			return new AddTransactionResponse(DateTime.UtcNow);
		}
	}
}
