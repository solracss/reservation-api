using Microsoft.AspNetCore.Mvc;
using ReservationAPI.Domain;
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
        public async Task<ActionResult<IEnumerable<User>>> GetUsers([FromQuery] QueryParameters queryParameters)
        {
            var usersDtos = await userService.GetAllUsersAsync(queryParameters);
            return Ok(usersDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await userService.GetUserAsync(id);
            return Ok(user);
        }
    }
}
