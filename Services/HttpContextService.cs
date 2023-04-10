using System.Security.Claims;

namespace ReservationAPI.Services
{
    public class HttpContextService : IHttpContextService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public HttpContextService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId()
        {
            var id = httpContextAccessor.HttpContext.User
                .FindFirstValue(ClaimTypes.NameIdentifier);
            return id is null ? 0 : int.Parse(id);
        }
    }
}
