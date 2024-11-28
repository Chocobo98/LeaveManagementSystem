using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagementSystem.Data
{
    public class LeaveType : BaseEntity
    {

        [Column(TypeName = "nvarchar(150)")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Number of Days")]
        public int NumberOfDays { get; set; }
    }
}
