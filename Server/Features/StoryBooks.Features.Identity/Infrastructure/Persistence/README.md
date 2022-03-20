## Setup for migration
1. Set 'StoryBooks.Web.CoreAPI' as startup project
2. Open Package Manager Console
3. Set 'StoryBooks.Features.Identity' as default project 


## Add migration
```
Add-Migration 'Identity{MigrationName}' -OutputDir "Infrastructure/Persistence/Migrations" -context IdentityUserDbContext
```

## Update database
```
Update-Database -Context IdentityUserDbContext
```