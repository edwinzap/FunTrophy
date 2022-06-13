namespace FunTrophy.API.Contracts.Services
{
    public interface IExportService
    {
        Task<byte[]> GetTeamsByTimeAdjustmentCategoryFile(int raceId);
    }
}