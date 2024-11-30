using System.Net;

namespace Transactions_Web_API.Exceptions
{
	public class BusinessLogicException : BaseException
	{
		public BusinessLogicException(
			HttpStatusCode statusCode = HttpStatusCode.BadRequest,
			string title = "",
			string details = "")
			: base(statusCode, title, details)
		{
			StatusCode = statusCode;
			Type = typeof(BusinessLogicException).ToString();
			Title = title;
			Details = details;
		}
	}
}
