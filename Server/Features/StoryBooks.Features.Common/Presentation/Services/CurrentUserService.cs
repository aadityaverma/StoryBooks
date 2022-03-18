namespace StoryBooks.Features.Common.Presentation.Services;

using Microsoft.AspNetCore.Http;

using StoryBooks.Features.Common.Application.Interfaces;

using System;
using System.Collections.Generic;
using System.Security.Claims;

public class CurrentUserService : ICurrentUser
{
    private readonly List<string> roles;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        var user = httpContextAccessor.HttpContext?.User;

        if (user is null)
        {
            throw new InvalidOperationException("This request does not have an authenticated user.");
        }

        this.UserId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        this.Email = user.FindFirstValue(ClaimTypes.Email);
        this.FirstName = user.FindFirstValue(ClaimTypes.GivenName);
        this.LastName = user.FindFirstValue(ClaimTypes.Surname);
        this.roles = user.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();
    }

    public string UserId { get; }

    public string Email { get; }

    public string FirstName { get; }

    public string LastName { get; }

    public string FullName => $"{FirstName} {LastName}".Trim();

    public IList<string> Roles => roles.AsReadOnly();
}