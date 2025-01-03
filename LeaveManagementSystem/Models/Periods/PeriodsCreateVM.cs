namespace LeaveManagementSystem.Models.Periods
{
    public class PeriodsCreateVM
    {
        public string Name { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}
