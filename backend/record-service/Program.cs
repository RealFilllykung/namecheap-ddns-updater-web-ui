using Microsoft.EntityFrameworkCore;
using record_service.infrastructures.databases;
using record_service.infrastructures.interfaces.repositories;
using record_service.infrastructures.interfaces.services;
using record_service.repositories;
using record_service.services;
using record_service.services.middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

SetupDependencyInjection();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseMiddleware<ExceptionHandlerMiddleware>();

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
    builder.Services.AddTransient<IDdnsService, DdnsService>();
    
    builder.Services.AddDbContext<DatabaseContext>(
        options => options.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseConnectionString")));
    
    builder.Services.AddHttpClient<IIpRepository,IpRepository>(client => client.BaseAddress = new Uri("http://checkip.dyndns.org"));
    builder.Services.AddHttpClient<IPasswordRepository, PasswordRepository>(client => client.BaseAddress = new Uri(builder.Configuration["Url:PasswordService"]));
    builder.Services.AddHttpClient<IDdnsRepository, DdnsRepository>(client => client.BaseAddress = new Uri(builder.Configuration["Url:DdnsService"]));
}