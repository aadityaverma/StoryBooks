namespace StoryBooks.Features.Common.Domain.Interfaces;

using System;

public interface IAuditableEntity
{
    string CreatedBy { get; }

    DateTime CreatedOn { get; }

    string? ModifiedBy { get; }

    DateTime? ModifiedOn { get; }

    IAuditableEntity SetCreated(string createdBy, DateTime createdDate);

    IAuditableEntity SetModified(string modifiedBy, DateTime modifiedDate);
}