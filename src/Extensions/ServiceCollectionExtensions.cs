using Microsoft.AspNetCore.Http.Features;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using Transactions_Web_API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Transactions_Web_API.Database;

namespace Transactions_Web_API.Extensions
{
	/// <summary>
	/// Конфигурирование сервисов проекта
	/// </summary>
	public static class ServiceCollectionExtensions
	{
		private const long OneGb = 1073741824;

		public static IServiceCollection AddApiServices(
			this IServiceCollection services,
			IConfiguration configuration)
		{
			services
				.AddEndpointsApiExplorer()
				.AddControllers();

			services.AddHttpContextAccessor();
			services.AddHealthChecks();

			AddSwagger(services);
			AddLogging(services, configuration);

			services.Configure<FormOptions>(x =>
			{
				x.ValueLengthLimit = int.MaxValue;
				x.MultipartBodyLengthLimit = OneGb;
			});

			return services;
		}

		public static IServiceCollection AddInfrastuctureServices(
			this IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddDbContext<IApplicationDBContext, PostgreDbContext>(options =>
				options.UseNpgsql(configuration.GetConnectionString("Database"), builder =>
					builder.MigrationsAssembly(typeof(PostgreDbContext).Assembly.FullName)));

			services.AddAutoMapper(Assembly.GetExecutingAssembly());

			AddExceptionDetails(services);

			return services;
		}

		private static void AddSwagger(IServiceCollection services)
		{
			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo { Title = "Transaction Web API", Version = "v1" });

				// Отображает документацию (summary и т.п.) по API на странице сваггера
				var swaggerXmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, swaggerXmlFile));
			});
		}

		private static void AddLogging(IServiceCollection services, IConfiguration configuration)
		{
			Log.Logger = new LoggerConfiguration()
				.ReadFrom.Configuration(configuration)
				.WriteTo.Console()
				.CreateLogger();

			services.AddSerilog();
		}

		private static void AddExceptionDetails(IServiceCollection services)
		{
			//дополнительное указание деталей для случаев необработанной ошибки
			services.AddProblemDetails(options =>
			{
				options.CustomizeProblemDetails = context =>
				{
					context.ProblemDetails.Instance =
						$"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";

					context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);

					Activity? activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
					context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);
				};
			});
		}
	}
}
