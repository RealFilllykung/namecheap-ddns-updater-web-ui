using ddns_ip_service.infrastructures.databases;
using ddns_ip_service.infrastructures.interfaces.repositories;
using ddns_ip_service.infrastructures.interfaces.services;
using ddns_ip_service.repositories;
using ddns_ip_service.services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

SetupDependencyInjection();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

void SetupDependencyInjection()
{
    builder.Services.AddTransient<IDdnsService, DdnsService>();
    builder.Services.AddTransient<IIpService, IpService>();
    builder.Services.AddTransient<IDatabaseService, DatabaseService>();
    builder.Services.AddTransient<IPasswordService, PasswordService>();
    builder.Services.AddTransient<INamecheapService, NamecheapService>();
    
    builder.Services.AddDbContext<DatabaseContext>(
        options => options.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseConnectionString")));
    
    builder.Services.AddHttpClient<IIpRepository,IpRepository>(client => client.BaseAddress = new Uri("http://checkip.dyndns.org"));
    builder.Services.AddHttpClient<INamecheapRepository, NamecheapRepository>(client => client.BaseAddress = new Uri(builder.Configuration["Url:NamecheapDynamicDnsUrl"]));
    builder.Services.AddHttpClient<IPasswordRepository, PasswordRepository>(client => client.BaseAddress = new Uri(builder.Configuration["Url:PasswordService"]));
}