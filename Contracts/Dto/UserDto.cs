﻿using Contracts.Dto;

namespace ReservationAPI.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<ReservationDto> Reservations { get; set; }
        public string DateOfBirth { get; set; }
    }
}
