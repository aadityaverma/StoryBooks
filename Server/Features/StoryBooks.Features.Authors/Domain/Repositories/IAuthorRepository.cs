namespace StoryBooks.Features.Authors.Domain.Repositories;

using StoryBooks.Features.Authors.Domain.Entities;
using StoryBooks.Features.Domain.Interfaces;

internal interface IAuthorRepository : IDomainRepository<Author>
{
    Author Get(string id);

    Book GetBook(string authorId, string bookId);
    
    Book GetChapter(string authorId, string bookId, int chapterId);
}
