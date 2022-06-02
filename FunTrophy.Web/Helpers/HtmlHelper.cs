using FunTrophy.Shared.Model;

namespace FunTrophy.Web.Helpers
{
    public static class HtmlHelper
    {
        public static string GetFaIcon(TeamType teamType)
        {
            return teamType == TeamType.Family ? "fas fa-paw" : "fas fa-shield-halved";
        }

        public static string GetRankSup(int number)
        {
            return number == 1 ? "er" : "e";
        }
    }
}
