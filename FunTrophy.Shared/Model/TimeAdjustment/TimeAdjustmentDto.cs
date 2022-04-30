namespace FunTrophy.Shared.Model
{
    public class TimeAdjustmentDto
    {
        public int Id { get; set; }
        public TimeSpan Time { get; set; }
        public string CategoryName { get; set; }
    }
}