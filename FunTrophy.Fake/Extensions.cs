namespace FunTrophy.Fake
{
    public static class Extensions
    {
        private static Random random = new Random();

        public static IList<T> Shuffle<T>(this IList<T> list)
        {
            var clonedList = new List<T>(list);
            int n = clonedList.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = clonedList[k];
                clonedList[k] = clonedList[n];
                clonedList[n] = value;
            }
            return clonedList;
        }
    }
}