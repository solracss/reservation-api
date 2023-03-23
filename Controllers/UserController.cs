using Microsoft.AspNetCore.Mvc;
using ReservationAPI.Domain;
using ReservationAPI.Services;

namespace ReservationAPI.Controllers.V1;

[ApiController]
[Route("api/users")]
public class UserController : Controller
{
    private readonly IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<User>>> GetUsers()
    {
        var usersDtos = await userService.GetAllUsersAsync();
        return Ok(usersDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await userService.GetUserAsync(id);
        return Ok(user);
    }
}
