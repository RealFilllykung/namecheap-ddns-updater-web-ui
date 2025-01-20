﻿using Microsoft.EntityFrameworkCore;
using record_service.models;

namespace record_service.infrastructures.databases;

public class DatabaseContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DatabaseContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DatabaseConnectionString"));
    }

    public DbSet<RecordModel> Records { get; set; }
}