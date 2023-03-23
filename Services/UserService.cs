using ReservationAPI.Data;
using ReservationAPI.Domain;

namespace ReservationAPI.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext dataContext;

        public UserService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IEnumerable<User> GetUsers()
        {
            var users = dataContext
                .Users.ToList();
            return users;
        }
    }
}
