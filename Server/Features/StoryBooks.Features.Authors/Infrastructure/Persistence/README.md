## Setup for migration
1. Set 'StoryBooks.Web.CoreAPI' as startup project
2. Open Package Manager Console
3. Set 'StoryBooks.Features.Authors' as default project 

## Add migration
```
Add-Migration 'Authors{MigrationName}' -OutputDir "Infrastructure/Persistence/Migrations" -context AuthorsDbContext
```

## Update database
```
Update-Database -Context AuthorsDbContext
```