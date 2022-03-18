## Add migration
```
Add-Migration {MigrationName} -OutputDir "Infrastructure/Persistence/Migrations" -context AuthorsDbContext
```

## Update database
Update-Database -Context AuthorsDbContext