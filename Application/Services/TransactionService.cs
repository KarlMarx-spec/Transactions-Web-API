using Application.Interfaces;
using Application.Models.API.Requests;
using Application.Models.API.Responses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Application.Services
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

		public async Task<List<TransactionResponse>> GetAllTransactionsAsync(CancellationToken ct)
		{
			return await _context.Transactions
				.AsNoTracking()
				.ProjectTo<TransactionResponse>(_mapper.ConfigurationProvider)
				.ToListAsync(ct);
		}

		public async Task<TransactionResponse> GetTransactionAsync(Guid id, CancellationToken ct)
		{
			return await _context.Transactions
				.AsNoTracking()
				.ProjectTo<TransactionResponse>(_mapper.ConfigurationProvider)
				.FirstOrDefaultAsync(x => x.Id == id, ct)
				?? throw new EntityNotFoundException<Transaction>(id);
		}

		public async Task<AddTransactionResponse> AddTransactionAsync(
			AddTransactionRequest request,
			CancellationToken ct)
		{
			if (await _context.Transactions.CountAsync(ct) >= 
					_configuration.GetValue<int>("MaxTransactionsCount"))
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
