namespace Transactions_Web_API.Models.API.Responses
{
	public class AddTransactionResponse
	{
		public AddTransactionResponse(DateTime insertDateTime)
		{
			InsertDateTime = insertDateTime;
		}

		public DateTime InsertDateTime { get; set; }
	}
}
