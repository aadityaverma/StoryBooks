namespace StoryBooks.Features.Identity.Application.Services;

using StoryBooks.Features.Identity.Domain.Entities;

using System.Collections.Generic;

public interface IAuthTokenGeneratorService
{
    string GenerateToken(User user, IEnumerable<string> roles);
}