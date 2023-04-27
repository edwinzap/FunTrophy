using FunTrophy.Web.Helpers;

namespace FunTrophy.Web.UnitTests
{
    public class ExtensionsTests
    {
        public static IEnumerable<object[]> TimeSpan_FormattedString()
        {
            yield return new object[] { new TimeSpan(0, 0, 0), "00:00" };
            yield return new object[] { new TimeSpan(0, 0, 25), "00:25" };
            yield return new object[] { new TimeSpan(0, 1, 25), "01:25" };
            yield return new object[] { new TimeSpan(0, 25, 25), "25:25" };
            yield return new object[] { new TimeSpan(1, 0, 0), "1:00:00" };
            yield return new object[] { new TimeSpan(40, 0, 0), "40:00:00" };
            yield return new object[] { new TimeSpan(0, 0, 65), "01:05" };
            yield return new object[] { new TimeSpan(0, -10, 0), "-10:00" };
            yield return new object[] { new TimeSpan(-1, 0, 0), "-1:00:00" };
        }

        [Theory, MemberData(nameof(TimeSpan_FormattedString))]
        public void ToTimeString_TimeSpan_ReturnFormattedString(TimeSpan time, string expectedString)
        {
            var result = time.ToTimeString();
            result.Should().Be(expectedString);
        }
    }
}