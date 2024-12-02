using System.Net;

namespace Transactions_Web_API.Exceptions
{
	public class BusinessLogicException : BaseBusinessException
	{
		public BusinessLogicException(
			HttpStatusCode statusCode = HttpStatusCode.BadRequest,
			string title = "",
			string details = "")
			: base(statusCode, title, details)
		{
			StatusCode = statusCode;
			Type = nameof(BusinessLogicException);
			Title = title;
			Details = details;
		}
	}
}
