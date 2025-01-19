using password_service.infrastructures.interfaces.services;
using password_service.services;

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

app.Run();

void SetupDependencyInjection()
{
    builder.Services.AddSingleton<IPasswordService, PasswordService>();
}
