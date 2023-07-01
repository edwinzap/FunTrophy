namespace FunTrophy.Web.Contracts.Services
{
    public interface IExportService
    {
        string GetTeamsByTimeAdjustmentCategoryFileUrl(int raceId);
        string GetTracksByColorFileUrl(int raceId);

        string GetTimeAdjustmentCategoriesByColor(int raceId);
    }
}
