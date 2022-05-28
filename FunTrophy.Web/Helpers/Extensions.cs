namespace FunTrophy.Web.Helpers
{
    public static class Extensions
    {
        public static string ToMinutesAndSecondsString(this TimeSpan time)
        {
            var formattedTime = (time < TimeSpan.Zero) ? "-" : "";
            if (time.Hours > 0)
            {
                formattedTime += time.ToString(@"h\:mm\:ss");
            }
            else
            {
                formattedTime += time.ToString(@"mm\:ss");
            }

            return formattedTime;
        }
    }
}
