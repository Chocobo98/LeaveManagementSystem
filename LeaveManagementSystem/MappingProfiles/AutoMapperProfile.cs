using AutoMapper;
using LeaveManagementSystem.Data;
using LeaveManagementSystem.Models.LeaveType;

namespace LeaveManagementSystem.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<LeaveType, LeaveTypeReadOnlyVM>();

            CreateMap<LeaveTypeCreateVM, LeaveType>();

            CreateMap<LeaveType, LeaveTypeEditVM>().ReverseMap(); //Mapea de LeaveType a LeaveTypeEditVM y viceversa

            // In case from both models have differents properties names...
            //CreateMap<LeaveType, LeaveTypeReadOnlyVM>();
            //.ForMember(dest => dest.Days, opt => opt.MapFrom(src => src.NumberOfDays));

        }
    }
}
