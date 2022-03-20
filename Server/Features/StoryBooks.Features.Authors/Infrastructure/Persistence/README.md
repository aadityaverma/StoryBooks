## Add migration
```
Add-Migration 'Authors{MigrationName}' -OutputDir "Infrastructure/Persistence/Migrations" -context AuthorsDbContext
```

## Update database
Update-Database -Context AuthorsDbContext