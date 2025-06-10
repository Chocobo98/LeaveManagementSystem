using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Application.Models.LeaveRequests
{
    public class LeaveRequestCreateVM : IValidatableObject
    {
        [DisplayName("Start Date")]
        [Required]
        public DateOnly StarDate { get; set; }

        [DisplayName("End Date")]
        [Required]
        public DateOnly EndDate { get; set; }

        [DisplayName("Desired Leave Type")]
        [Required]
        public int LeaveTypeId { get; set; }

        [DisplayName("Additional Information")]
        [StringLength(150)]
        public string? Comments { get; set; }

        public SelectList? LeaveTypes { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StarDate > EndDate)
            {
                yield return new ValidationResult("The Start Date Must be Before the End Date", [nameof(StarDate), nameof(EndDate)]);
            }


        }
    }
}