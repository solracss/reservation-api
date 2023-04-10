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

            CreateMap<Reservation, ReservationDto>()
                .ForMember(dto => dto.StartDate, startDate =>
                startDate.MapFrom(r => r.StartDate.ToString("yyyy-MM-dd HH:mm")))
                .ForMember(dto => dto.EndDate, endDate =>
                endDate.MapFrom(r => r.EndDate.ToString("yyyy-MM-dd HH:mm")))
                .ForMember(dto => dto.UserEmail, email =>
                email.MapFrom(u => u.User.Email))
                .ForMember(dto => dto.UserFirstName, email =>
                email.MapFrom(u => u.User.FirstName))
                .ForMember(dto => dto.UserLastName, email =>
                email.MapFrom(u => u.User.LastName));

            CreateMap<CreateReservationDto, Reservation>();
        }
    }
}
