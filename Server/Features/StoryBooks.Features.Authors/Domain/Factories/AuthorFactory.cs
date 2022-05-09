namespace StoryBooks.Features.Authors.Domain.Factories;

using StoryBooks.Features.Authors.Domain.Entities;

using System;

internal class AuthorFactory : IAuthorFactory
{
    public AuthorFactory()
    {

    }

    public Author Build() => throw new NotImplementedException();

    public void Reset() => throw new NotImplementedException();

    public IAuthorFactory SetAlias(string alias) => throw new NotImplementedException();

    public IAuthorFactory SetDescription(string description) => throw new NotImplementedException();

    public IAuthorFactory SetName(string name) => throw new NotImplementedException();

    public IAuthorFactory SetUserId(string userId) => throw new NotImplementedException();
}
