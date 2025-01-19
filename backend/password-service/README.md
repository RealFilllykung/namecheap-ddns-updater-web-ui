# Password Service

Password service is the service that handle all of password operation.

## Database Migration

Please run the following command for migrate the database.

1. Add new database migration
```
dotnet ef migrations add <your_migration_name>
```

2. Update database
```
dotnet ef database update
```