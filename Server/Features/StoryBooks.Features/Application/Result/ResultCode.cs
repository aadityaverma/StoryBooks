namespace StoryBooks.Features.Application;

using StoryBooks.Features.Domain.Entities;

public class ResultCode : Enumeration
{
    public static readonly ResultCode Ok = new(200, nameof(Ok));
    public static readonly ResultCode Created = new(201, nameof(Created));
    public static readonly ResultCode BadRequest = new(400, nameof(BadRequest));
    public static readonly ResultCode NotAuthorized = new(403, nameof(NotAuthorized));
    public static readonly ResultCode NotFound = new(404, nameof(NotFound));

    private ResultCode(int value, string name) : base(value, name)
    {
    }
}