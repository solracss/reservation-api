using Microsoft.AspNetCore.Mvc;
using ReservationAPI.Domain;
using ReservationAPI.Dtos;
using ReservationAPI.Services;

namespace ReservationAPI.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/reservation")]
    [ApiVersion("1.0")]
    public class ReservationController : Controller
    {
        private readonly IReservationService reservationService;

        public ReservationController(IReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<ReservationDto>>> GetReservations([FromQuery] QueryParameters queryParameters)
        {
            var reservations = await reservationService.GetAllReservationsAsync(queryParameters);
            return Ok(reservations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDto>> GetReservation(int id)
        {
            var reservation = await reservationService.GetReservationAsync(id);
            return Ok(reservation);
        }
    }
}
