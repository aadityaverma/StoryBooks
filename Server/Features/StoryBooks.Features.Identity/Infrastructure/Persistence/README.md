## Add migration
```
Add-Migration 'Identity{MigrationName}' -OutputDir "Infrastructure/Persistence/Migrations" -context IdentityUserDbContext
```

## Update database
Update-Database -Context IdentityUserDbContext