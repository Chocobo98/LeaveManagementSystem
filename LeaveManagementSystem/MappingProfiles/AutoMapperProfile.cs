using AutoMapper;
using LeaveManagementSystem.Models.LeaveType;
using LeaveManagementSystem.Models.Periods;

namespace LeaveManagementSystem.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<LeaveType, LeaveTypeReadOnlyVM>();

            CreateMap<LeaveTypeCreateVM, LeaveType>();

            CreateMap<LeaveType, LeaveTypeEditVM>().ReverseMap(); //Mapea de LeaveType a LeaveTypeEditVM y viceversa

            CreateMap<Period, PeriodsVM>().ReverseMap();

            CreateMap<PeriodsCreateVM, Period>();


            // In case from both models have differents properties names...
            //CreateMap<LeaveType, LeaveTypeReadOnlyVM>();
            //.ForMember(dest => dest.Days, opt => opt.MapFrom(src => src.NumberOfDays));

        }
    }
}
