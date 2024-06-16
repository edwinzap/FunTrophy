namespace FunTrophy.Shared.Model
{
    public class AddTimeAdjustmentDto
    {
        public int TeamId { get; set; }
        public int CategoryId { get; set; }
        public int? Seconds { get; set; }
    }
}