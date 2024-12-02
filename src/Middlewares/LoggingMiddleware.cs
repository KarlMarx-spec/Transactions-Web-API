namespace Transactions_Web_API.Middlewares
{
	/// <summary>
	/// Middleware обработки исключений
	/// </summary>
	public class LoggingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<LoggingMiddleware> _logger;

		public LoggingMiddleware(RequestDelegate next,
			ILogger<LoggingMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, 
					"Exception was thrown. TraceIdentifier = {TraceIdentifier}",
					httpContext.TraceIdentifier);
				throw;
			}
		}
	}
}
