var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<IApplicationDBContext, PostgreDbContext>(options =>
//				options.UseNpgsql(builder.Configuration.GetConnectionString("Database"), builder =>
//					builder.MigrationsAssembly(typeof(PostgreDbContext).Assembly.FullName)));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
