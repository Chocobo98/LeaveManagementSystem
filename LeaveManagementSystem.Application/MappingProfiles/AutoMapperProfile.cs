using AutoMapper;
using LeaveManagementSystem.Application.Models.LeaveAllocations;
using LeaveManagementSystem.Application.Models.LeaveRequests;
using LeaveManagementSystem.Application.Models.LeaveType;
using LeaveManagementSystem.Application.Models.Periods;

namespace LeaveManagementSystem.Application.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Leave Type
            CreateMap<LeaveType, LeaveTypeReadOnlyVM>();
            CreateMap<LeaveTypeCreateVM, LeaveType>();
            CreateMap<LeaveType, LeaveTypeEditVM>().ReverseMap(); //Mapea de LeaveType a LeaveTypeEditVM y viceversa

            //Periods
            CreateMap<Period, PeriodsVM>().ReverseMap();
            CreateMap<PeriodsCreateVM, Period>();

            //LeaveAllocation
            CreateMap<LeaveAllocation, LeaveAllocationVM>();
            CreateMap<LeaveAllocation, LeaveAllocationEditVM>();
            CreateMap<ApplicationUser, EmployeeListVM>();

            //LeaveRequest
            CreateMap<LeaveRequestCreateVM, LeaveRequest>();

            // In case from both models have differents properties names...
            //CreateMap<LeaveType, LeaveTypeReadOnlyVM>();
            //.ForMember(dest => dest.Days, opt => opt.MapFrom(src => src.NumberOfDays));

        }
    }
}
