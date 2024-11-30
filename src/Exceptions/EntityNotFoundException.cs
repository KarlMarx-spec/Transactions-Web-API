using System.Net;

namespace Transactions_Web_API.Exceptions
{
	public class EntityNotFoundException : BaseException
	{
		public EntityNotFoundException(
			HttpStatusCode statusCode = HttpStatusCode.NotFound,
			string title = "�������� �� �������",
			string details = "")
			: base(statusCode, title, details)
		{
			StatusCode = statusCode;
			Type = typeof(EntityNotFoundException).ToString();
			Title = title;
			Details = details;
		}
	}

	public class EntityNotFoundException<T> : EntityNotFoundException
	{
		public EntityNotFoundException(Guid? id)
			: base(details: $"�������� {typeof(T).Name} � Id = {id} �� �������")
		{
		}

		public EntityNotFoundException(string paramName, object paramValue)
			: base(details: $"�������� {typeof(T).Name} � {paramName} = {paramValue} �� �������")
		{
		}
	}
}
