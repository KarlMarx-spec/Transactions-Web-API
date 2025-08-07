using System.Net;

namespace Domain.Exceptions
{
	public class EntityNotFoundException : BaseBusinessException
	{
		public EntityNotFoundException(
			HttpStatusCode statusCode = HttpStatusCode.NotFound,
			string title = "Сущность не найдена",
			string details = "")
			: base(statusCode, title, details)
		{
			StatusCode = statusCode;
			Type = nameof(EntityNotFoundException);
			Title = title;
			Details = details;
		}
	}

	public class EntityNotFoundException<T> : EntityNotFoundException
	{
		public EntityNotFoundException(Guid? id)
			: base(details: $"Сущность {typeof(T).Name} с Id = {id} не найдена")
		{
		}

		public EntityNotFoundException(string paramName, object paramValue)
			: base(details: $"Сущность {typeof(T).Name} с {paramName} = {paramValue} не найдена")
		{
		}
	}
}
