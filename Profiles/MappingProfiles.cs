using AutoMapper;
using ReservationAPI.Domain;
using ReservationAPI.Dtos;

namespace ReservationAPI.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>();
            CreateMap<Reservation, ReservationDto>();
        }
    }
}
