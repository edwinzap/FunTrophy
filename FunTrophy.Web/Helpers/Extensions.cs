namespace FunTrophy.Web.Helpers
{
    public static class Extensions
    {
        public static string ToMinutesAndSecondsString(this TimeSpan time)
        {
            return ((time < TimeSpan.Zero) ? "-" : "") + time.ToString(@"mm\:ss");
        }
    }
}
