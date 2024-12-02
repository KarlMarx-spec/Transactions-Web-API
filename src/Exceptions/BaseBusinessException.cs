using System.Net;

namespace Transactions_Web_API.Exceptions
{
	public abstract class BaseBusinessException : Exception
	{
		public HttpStatusCode StatusCode { get; protected set; }
		public string Type { get; protected set; }
		public string Title { get; protected set; }
		public string Details { get; protected set; }

		public BaseBusinessException(
			HttpStatusCode statusCode = HttpStatusCode.BadRequest,
			string type = "Default",
			string title = "",
			string details = "")
			: base()
		{
			StatusCode = statusCode;
			Type = type;
			Title = title;
			Details = details;
		}
	}
}
