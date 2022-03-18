namespace StoryBooks.Features.Infrastructure.Persistence;

using System;
using System.Threading.Tasks;

public interface IDataInitializer
{
    Task Initialize();

    Task SeedData();
}