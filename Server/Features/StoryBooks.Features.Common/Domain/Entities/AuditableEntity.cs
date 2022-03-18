namespace StoryBooks.Features.Common.Domain.Entities;

using System;

using Exceptions;

using StoryBooks.Features.Common.Domain.Interfaces;
using StoryBooks.Libraries.Validation;

public abstract class AuditableEntity<TKey> : Entity<TKey>, IAuditableEntity
{
    protected AuditableEntity()
    {
    }

    protected AuditableEntity(string createdBy)
    {
        this.SetCreated(createdBy);
    }

    protected AuditableEntity(string createdBy, DateTime createdDate)
    {
        this.SetCreated(createdBy, createdDate);
    }

    protected AuditableEntity(
        string createdBy,
        DateTime createdDate,
        string modifiedBy, DateTime
        modifiedDate)
    {
        this.SetCreated(createdBy, createdDate);
        this.SetModified(modifiedBy, modifiedDate);
    }

    public string CreatedBy { get; private set; } = default!;

    public DateTime CreatedOn { get; private set; }

    public string? ModifiedBy { get; private set; }

    public DateTime? ModifiedOn { get; private set; }

    public IAuditableEntity SetCreated(string createdById)
    {
        return this.SetCreated(createdById, DateTime.UtcNow);
    }

    public IAuditableEntity SetCreated(string createdById, DateTime createdDate)
    {
        this.ValidateUserId(createdById);
        this.CreatedOn = createdDate;
        this.CreatedBy = createdById;
        return this;
    }
    public IAuditableEntity SetModified(string modifiedById)
    {
        return this.SetModified(modifiedById, DateTime.UtcNow);
    }

    public IAuditableEntity SetModified(string modifiedById, DateTime modifiedDate)
    {
        this.ValidateUserId(modifiedById);
        this.ModifiedOn = modifiedDate;
        this.ModifiedBy = modifiedById;
        return this;
    }

    protected virtual void ValidateUserId(string userId)
    {
        Guard.ForEmptyString<InvalidEntityException>(userId, name: nameof(userId));
    }
}