namespace System;

/*
 *  ZoneDateTime.Instance.Now();
 *  ZoneDateTime.SetOverride(d => new DateTime(2000, 1, 1))
 *  ZoneDateTime.Instance.Now();
 *  ZoneDateTime.ResetToNormal();
 *  ZoneDateTime.Instance.Now();
 */
public class ZoneDateTime : IDateTime
{
    private static ZoneDateTime DateInstance => new();
    private static readonly AsyncLocal<Func<DateTime>?> overrideUtcNowFunc = new();

    public DateOnly Date() => DateOnly.FromDateTime(this.Now());

    public DateTime Now() =>
        overrideUtcNowFunc.Value != null ?
            overrideUtcNowFunc.Value() :
            DateTime.Now;

    public TimeOnly Time() => TimeOnly.FromDateTime(this.Now());

    public static ZoneDateTime Instance => DateInstance;

    public static void SetOverride(Func<DateTime> func) => overrideUtcNowFunc.Value = func;

    public static void ResetToNormal() => overrideUtcNowFunc.Value = null;
}