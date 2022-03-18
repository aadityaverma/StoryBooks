namespace StoryBooks.Features.Authors.Domain.Factories;

using StoryBooks.Features.Authors.Domain.Entities;
using StoryBooks.Features.Domain.Interfaces;

internal interface IAuthorFactory : IFactory<Author>
{
    IAuthorFactory SetUserId (string userId);

    IAuthorFactory SetName(string name);

    IAuthorFactory SetAlias(string alias);

    IAuthorFactory SetDescription(string description);
}