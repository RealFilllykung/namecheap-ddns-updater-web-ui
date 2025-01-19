using Microsoft.EntityFrameworkCore;
using password_service.models;

namespace password_service.infrastructures.database;

public class CreatePasswordTable :  DbContext{
    
    private readonly IConfiguration _configuration;

    public CreatePasswordTable(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DatabaseConnectionString"));
    }
    
    public DbSet<PasswordRecord> Passwords { get; set; }
}