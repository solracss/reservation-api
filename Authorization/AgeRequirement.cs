using Microsoft.AspNetCore.Authorization;

namespace ReservationAPI.Authorization
{
    public class AgeRequirement : IAuthorizationRequirement
    {
        public int MinimumAge { get; }

        public AgeRequirement(int minimumAge)
        {
            MinimumAge = minimumAge;
        }
    }
}
