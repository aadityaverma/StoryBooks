namespace System
{
    /*
     *  UtcDateTime.Instance.Now();
     *  UtcDateTime.SetOverride(d => new DateTime(2000, 1, 1))
     *  UtcDateTime.Instance.Now();
     *  UtcDateTime.ResetToNormal();
     *  UtcDateTime.Instance.Now();
     */
    public class UtcDateTime : IDateTime
    {
        private static UtcDateTime DateInstance => new();
        private static AsyncLocal<Func<DateTime>?> overrideUtcNowFunc = new();

        public DateOnly Date() => DateOnly.FromDateTime(Now());

        public DateTime Now() => 
            overrideUtcNowFunc.Value != null ?
                overrideUtcNowFunc.Value() :
                DateTime.UtcNow;

        public TimeOnly Time() => TimeOnly.FromDateTime(Now());

        public static UtcDateTime Instance => DateInstance;

        public static void SetOverride(Func<DateTime> func)
        {
            overrideUtcNowFunc.Value = func;
        }

        public static void ResetToNormal()
        {
            overrideUtcNowFunc.Value = null;
        }
    }
}
