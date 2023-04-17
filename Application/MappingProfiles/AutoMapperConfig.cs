using AutoMapper;
using Contracts.Dto;
using Domain.Entities;
using ReservationAPI.Dto;

namespace Application.MappingProfiles
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>()
                .ForMember(dto => dto.DateOfBirth, dob =>
                dob.MapFrom(u => u.DateOfBirth.ToString("yyyy-MM-dd")));

                cfg.CreateMap<Reservation, ReservationDto>()
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

                cfg.CreateMap<CreateReservationDto, Reservation>();
            }).CreateMapper();
    }
}
