using Transactions_Web_API.Extensions;
using Transactions_Web_API.Interfaces;
using Transactions_Web_API.Middlewares;
using Transactions_Web_API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiServices(builder.Configuration);
builder.Services.AddInfrastuctureServices(builder.Configuration);

builder.Services.AddScoped<ITransactionService, TransactionService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.ApplyMigrations();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseStaticFiles();

app.UseStatusCodePages();
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<LoggingMiddleware>();

await app.RunAsync();
