using Microsoft.AspNetCore.Mvc;
using Transactions_Web_API.Interfaces;

namespace Transactions_Web_API.Controllers
{
	/// <summary>
	/// ����������
	/// </summary>
	[ApiController]
	[Route("[controller]")]
	public class TransactionsController : ControllerBase
	{
		private readonly ITransactionService _transactionService;

		public TransactionsController(ITransactionService transactionService)
		{
			_transactionService = transactionService;
		}

		/// <summary>
		/// ����������
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <returns>������ ����������</returns>
		[HttpGet]
		public async Task<IActionResult> GetTransactions(CancellationToken cancellationToken)
		{
			return Ok(await _transactionService.GetAllTransactionsAsync(cancellationToken));
		}
	}
}
