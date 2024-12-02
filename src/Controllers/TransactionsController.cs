using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Transactions_Web_API.Interfaces;
using Transactions_Web_API.Models.API.Requests;
using Transactions_Web_API.Models.API.Responses;

namespace Transactions_Web_API.Controllers
{
	/// <summary>
	/// ����������
	/// </summary>
	[ApiController]
	[Route("/api/v1/[controller]")]
	public class TransactionsController : ControllerBase
	{
		private readonly ITransactionService _transactionService;

		public TransactionsController(ITransactionService transactionService)
		{
			_transactionService = transactionService;
		}

		/// <summary>
		/// �������� ��� ����������
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <returns>������ ����������</returns>
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(List<TransactionResponse>))]
		public async Task<IActionResult> GetTransactions(CancellationToken cancellationToken)
		{
			return Ok(await _transactionService.GetAllTransactionsAsync(cancellationToken));
		}

		/// <summary>
		/// �������� ����������
		/// </summary>
		/// <param name="request">������ ����������</param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpPost]
		[ProducesResponseType(200, Type = typeof(AddTransactionResponse))]
		public async Task<IActionResult> AddTransaction(
			[Required] AddTransactionRequest request,
			CancellationToken cancellationToken)
		{
			return Ok(await _transactionService.AddTransactionAsync(request, cancellationToken));
		}

		/// <summary>
		/// �������� ����������
		/// </summary>
		/// <param name="id">������������� ����������</param>
		/// <param name="cancellationToken"></param>
		/// <returns>������ ����������</returns>
		[HttpGet("{id}")]
		[ProducesResponseType(200, Type = typeof(TransactionResponse))]
		public async Task<IActionResult> GetTransaction(
			[Required] Guid id,
			CancellationToken cancellationToken)
		{
			return Ok(await _transactionService.GetTransactionAsync(id, cancellationToken));
		}
	}
}
