namespace LeaveManagementSystem.Models.LeaveType
{
    public interface ILeaveType
    {

        public string Name { get; set; }

        public int NumberOfDays { get; set; }
    }
}
