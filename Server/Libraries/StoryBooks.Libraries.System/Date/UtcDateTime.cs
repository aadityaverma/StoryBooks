namespace System
{
    public class UtcDateTime : IDateTime
    {
        private static UtcDateTime DateInstance => new();

        public DateOnly Date() => DateOnly.FromDateTime(Now());

        public DateTime Now() => DateTime.UtcNow;

        public TimeOnly Time() => TimeOnly.FromDateTime(Now());

        public static UtcDateTime Instance => DateInstance;
    }
}
