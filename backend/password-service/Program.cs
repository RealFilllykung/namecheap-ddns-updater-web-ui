using System.Security.Cryptography;
using password_service.infrastructures.interfaces.services;
using password_service.services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

SetupDependencyInjection();
SetupAsymmetricKey();

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

void SetupAsymmetricKey()
{
    void CreateDirectory()
    {
        Directory.CreateDirectory("data");
    }

    void CreateSymmtricKey()
    {
        RSA rsa = RSA.Create();
        string filePath = Path.Combine("data", "asymmetricKey.pem");
        
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, rsa.ExportRSAPrivateKeyPem());
        }
    }
    
    CreateDirectory();
    CreateSymmtricKey();
}
