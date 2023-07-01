namespace FunTrophy.API.Contracts.Services
{
    public interface IExportService
    {
        Task<byte[]> GetTeamsByTimeAdjustmentCategoryFile(int raceId);

        Task<byte[]> GetTracksByColorFile(int raceId);

        Task<byte[]> GetTimeAdjustmentCategoriesByColor(int raceId);
    }
}