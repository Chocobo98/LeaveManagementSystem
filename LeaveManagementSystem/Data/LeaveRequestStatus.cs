using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Data
{
    public class LeaveRequestStatus : BaseEntity
    {
        [StringLength(20)]
        public string Name { get; set; }
    }
}