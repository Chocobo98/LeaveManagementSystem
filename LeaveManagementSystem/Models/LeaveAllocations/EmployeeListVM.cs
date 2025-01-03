﻿using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Models.LeaveAllocations
{
    public class EmployeeListVM
    {
        //Base class for EmployeeAllocationVM
        public string Id { get; set; } = string.Empty;

        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;
    }
}
