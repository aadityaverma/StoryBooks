namespace StoryBooks.Features.Identity.Application.Services;

using StoryBooks.Features.Identity.Domain.Entities;

using System.Collections.Generic;

public interface IAuthTokenGeneratorService
{
    TokenModel GenerateToken(User user, IEnumerable<string> roles);
}

public record TokenModel(string Value, DateTime? Expires);