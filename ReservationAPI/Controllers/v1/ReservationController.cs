using Application.Interfaces;
using Application.Pagination;
using Application.QueryParameters;
using Contracts.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ReservationAPI.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/reservation")]
    [ApiVersion("1.0")]
    [Authorize(Policy = "Over 18")]
    public class ReservationController : Controller
    {
        private readonly IReservationService reservationService;

        public ReservationController(IReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PagedResult<ReservationDto>>> GetReservations([FromQuery] ReservationQueryParameters queryParameters)
        {
            var reservations = await reservationService.GetAllReservationsAsync(queryParameters);
            return Ok(reservations);
        }

        [HttpGet("{id}", Name = "GetReservation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReservationDto>> GetReservation(int id)
        {
            var reservation = await reservationService.GetReservationAsync(id);
            return Ok(reservation);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateReservation([FromBody] CreateReservationDto dto)
        {
            var reservationId = await reservationService.CreateReservationAsync(dto);
            return CreatedAtRoute(
                routeName: "GetReservation",
                routeValues: new { id = reservationId },
                null);
        }

        [HttpPut("edit/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> EditReservation([FromRoute] int id, [FromBody] EditReservationDto dto)
        {
            await reservationService.EditReservationDetailsAsync(id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteReservation([FromRoute] int id)
        {
            await reservationService.DeleteReservationAsync(id);
            return NoContent();
        }
    }
}
