namespace FunTrophy.Web.Helpers
{
    public static class Extensions
    {
        public static string ToTimeString(this TimeSpan time)
        {
            var formattedTime = (time < TimeSpan.Zero) ? "-" : "";
            if (time.Hours != 0)
            {
                formattedTime += string.Format("{0}:{1}:{2}", 
                    Math.Abs((int)time.TotalHours), 
                    time.Minutes.ToString("00"),
                    time.Seconds.ToString("00"));
            }
            else
            {
                formattedTime += time.ToString(@"mm\:ss");
            }

            return formattedTime;
        }
    }
}
