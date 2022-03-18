## Add migration
```
Add-Migration '{FeatureName}{MigrationName}' -OutputDir "Infrastructure/Persistence/Migrations" -context AuthorsDbContext
```

## Update database
Update-Database -Context AuthorsDbContext