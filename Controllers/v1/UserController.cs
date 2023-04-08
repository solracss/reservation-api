using Microsoft.AspNetCore.Mvc;
using ReservationAPI.Domain;
using ReservationAPI.Dtos;
using ReservationAPI.Services;

namespace ReservationAPI.Controllers.v1

{
    [ApiController]
    [Route("api/v{version:apiVersion}/users")]
    [ApiVersion("1.0")]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<UserDto>>> GetUsers([FromQuery] QueryParameters queryParameters)
        {
            var users = await userService.GetAllUsersAsync(queryParameters);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await userService.GetUserAsync(id);
            return Ok(user);
        }
    }
}
