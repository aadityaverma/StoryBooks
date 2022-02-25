namespace StoryBooks.Features.Identity.Infrastructure.Persistence
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;

    using StoryBooks.Features.Common.Application;
    using StoryBooks.Features.Common.Infrastructure.Persistence;

    using System.Threading.Tasks;

    public class IdentityDbInitializer : IDataInitializer
    {
        private readonly IdentityUserDbContext dbContext;
        private readonly ApplicationSettings settings;
        private readonly RoleManager<IdentityRole> rolesManager;

        public IdentityDbInitializer
            (IdentityUserDbContext dbContext,
            IOptions<ApplicationSettings> settings,
            RoleManager<IdentityRole> rolesManager)
        {
            this.dbContext = dbContext;
            this.settings = settings.Value;
            this.rolesManager = rolesManager;
        }

        public async Task Initialize()
        {
            await this.dbContext.Database.MigrateAsync();
            await this.SeedData();
        }

        public async Task SeedData()
        {
            if (!dbContext.Roles.Any())
            {
                await SeedRoles();
            }
                        
            await dbContext.SaveChangesAsync();
        }

        private async Task SeedRoles()
        {
            await rolesManager.CreateAsync(new IdentityRole(settings.Roles.Admin));
            await rolesManager.CreateAsync(new IdentityRole(settings.Roles.Author));
            await rolesManager.CreateAsync(new IdentityRole(settings.Roles.Moderator));
            await rolesManager.CreateAsync(new IdentityRole(settings.Roles.User));
        }
    }
}
