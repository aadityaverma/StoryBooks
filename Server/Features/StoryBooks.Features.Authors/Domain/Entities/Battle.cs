namespace StoryBooks.Features.Authors.Domain.Entities;

using StoryBooks.Features.Authors.Domain.Exceptions;
using StoryBooks.Features.Domain.Entities;
using StoryBooks.Libraries.Validation;

using static StoryBooks.Features.Authors.Domain.AuthorDomainConstants;

public class Battle : Entity<int>
{
    private readonly HashSet<Stat> includedStats;

    internal Battle(
        string enemyName,
        bool diceBattle)
    {
        ValidateName(enemyName);

        this.EnemyName = enemyName;
        this.DiceBattle = diceBattle;

        this.includedStats = new HashSet<Stat>();
    }

    public string EnemyName { get; private set; }

    public bool DiceBattle { get; private set; }

    public IReadOnlyCollection<Stat> IncludedStats => this.includedStats.ToList().AsReadOnly();

    internal Battle UpdateEnemyName(string name)
    {
        ValidateName(name);

        this.EnemyName = name;
        return this;
    }

    internal Battle AddDiceToBattle()
    {
        this.DiceBattle = true;
        return this;
    }

    internal Battle RemoveDiceFromBattle()
    {
        this.DiceBattle = false;
        return this;
    }

    internal Battle IncludeBattleStat(Stat stat)
    {
        this.includedStats.Add(stat);
        return this;
    }

    internal Battle RemoveBattleStat(Stat stat)
    {
        this.includedStats.Remove(stat);
        return this;
    }

    private static void ValidateName(string name)
    {
        Guard.ForStringLength<InvalidChoiceException>(
            value: name,
            minLength: MinBattleNameLength,
            maxLength: MaxBattleNameLength,
            name: nameof(name));
    }
}
