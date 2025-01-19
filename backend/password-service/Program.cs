using System.Security.Cryptography;
using password_service.infrastructures.interfaces.services;
using password_service.services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

SetupDependencyInjection();
SetupSymmetricKeyData();

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

void SetupSymmetricKeyData()
{
    void CreateDirectory()
    {
        Directory.CreateDirectory("data");
    }

    void CreateSymmtricKey()
    {
        Aes aes = Aes.Create();
        if (!File.Exists("data/symmetricKey.txt"))
        {
            using (FileStream fileStream = new FileStream("data/symmetricKey.txt", FileMode.Append, FileAccess.Write))
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
                {
                    binaryWriter.Write(aes.Key.Length);
                    binaryWriter.Write(aes.Key);
                }
            }
        }
    }
    
    CreateDirectory();
    CreateSymmtricKey();
}
