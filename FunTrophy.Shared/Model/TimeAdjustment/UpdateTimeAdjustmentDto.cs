namespace FunTrophy.Shared.Model
{
    public class UpdateTimeAdjustmentDto
    {
        public int CategoryId { get; set; }
        public TimeSpan Time { get; set; }
    }
}