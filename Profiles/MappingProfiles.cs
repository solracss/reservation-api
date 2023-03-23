using AutoMapper;
using ReservationAPI.Domain;
using ReservationAPI.Dtos;

namespace ReservationAPI.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>()
                .ForMember(dto => dto.DateOfBirth, dob =>
                dob.MapFrom(u => u.DateOfBirth.ToString("yyyy-MM-dd")));
            CreateMap<Reservation, ReservationDto>();
        }
    }
}
