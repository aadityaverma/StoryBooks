namespace StoryBooks.Features.Authors.Domain.Entities;

using StoryBooks.Features.Domain.Entities;

using System.Diagnostics.CodeAnalysis;

public class StatModifier : Entity<int>
{
    internal StatModifier(decimal change, Stat stat)
    {
        this.Stat = stat;
        this.Change = change;
    }

    [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "For EF")]
    private StatModifier(decimal change)
    {
        this.Change = change;
        this.Stat = default!;
    }

    public Stat Stat { get; private set; }

    public decimal Change { get; private set; }

    internal StatModifier UpdateStatChange(decimal change)
    {
        this.Change = change;
        return this;
    }
}
