namespace FunTrophy.API.Contracts.Services
{
    public interface IExportService
    {
        Task GetTeamsByTimeAdjustmentCategory(int raceId);
    }
}
