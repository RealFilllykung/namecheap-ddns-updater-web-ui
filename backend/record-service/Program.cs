using Microsoft.EntityFrameworkCore;
using record_service.infrastructures.databases;
using record_service.infrastructures.interfaces.repositories;
using record_service.infrastructures.interfaces.services;
using record_service.repositories;
using record_service.services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DatabaseContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseConnectionString")));

SetupDependencyInjection();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

MigrateDatabase();

app.Run();

void MigrateDatabase()
{
    using var scope = app.Services.CreateScope();
    var database = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    database.Database.Migrate();
}

void SetupDependencyInjection()
{
    builder.Services.AddTransient<IRecordService, RecordService>();
    builder.Services.AddTransient<IIPService, IpService>();
    
    builder.Services.AddHttpClient<IIpRepository,IpRepository>(client => client.BaseAddress = new Uri("http://checkip.dyndns.org"));
}