using Microsoft.AspNetCore.Mvc;
using NpgsqlTypes;
using ReservationAPI.Domain;
using ReservationAPI.Domain.QueryParameters;
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
        public async Task<ActionResult<PagedResult<ReservationDto>>> GetReservations([FromQuery] ReservationQueryParameters queryParameters)
        {
            var reservations = await reservationService.GetAllReservationsAsync(queryParameters);
            return Ok(reservations);
        }

        [HttpGet("{id}", Name = "GetReservation")]
        public async Task<ActionResult<ReservationDto>> GetReservation(int id)
        {
            var reservation = await reservationService.GetReservationAsync(id);
            return Ok(reservation);
        }

        [HttpPost]
        public async Task<ActionResult> CreateReservation([FromBody] CreateReservationDto dto)
        {
            var reservationId = await reservationService.CreateReservationAsync(dto);
            return CreatedAtRoute(
                routeName: "GetReservation",
                routeValues: new { id = reservationId },
                null);
        }

        [HttpPut("edit/{id}")]
        public async Task<ActionResult> EditReservation([FromRoute] int id, [FromBody] EditReservationDto dto)
        {
            await reservationService.EditReservationDetailsAsync(id, dto);
            return Ok();
        }
    }
}
