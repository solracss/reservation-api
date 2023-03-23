using ReservationAPI.Domain;

namespace ReservationAPI.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();
    }
}
