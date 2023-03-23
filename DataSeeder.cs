using ReservationAPI.Data;
using ReservationAPI.Domain;

namespace ReservationAPI
{
    public class DataSeeder
    {
        private readonly DataContext dataContext;

        public DataSeeder(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public void SeedDataContext()
        {
            if (dataContext.Database.CanConnect())
            {
                if (!dataContext.Users.Any())
                {
                    var users = GetUsers();
                    dataContext.Users.AddRange(users);
                    dataContext.SaveChanges();
                }
            }
        }

        private IEnumerable<User> GetUsers()
        {
            var users = new List<User>()
            {
                new User()
                {
                    Email = "test@test.com",
                    PasswordHash = "AQAAAAIAAYagAAAAEAXmwNmLn5w5KQnjOUqzvfGEPtQ2M8/SUPLQEAhg/petZIlcmnMuY9+/pbWnJlhISw==",
                    FirstName = "FirstUser",
                    LastName = "FirstUserLN",
                    DateOfBirth = new DateTime(2000, 1 ,1),
                    Reservations = new List<Reservation>()
                    {
                        new Reservation()
                        {
                            StartDate = new DateTime(2023, 3, 22),
                            EndDate = new DateTime(2023, 3, 25),
                            Price = 15M,
                            IsPaid = true
                        }
                    }
                },
                new User()
                {
                    Email = "test2@test.com",
                    PasswordHash ="AQAAAAIAAYagAAAAEIbPBs9KEIfrH67Qz7fDk+MyebfGVkSak+1pLYW5CII4S1D6K2Xq1K/TUezi+dTA0g==",
                    FirstName = "SecondUser",
                    LastName = "SecondUserLN",
                    DateOfBirth = new DateTime(2010, 1 ,1),
                    Reservations = new List<Reservation>()
                    {
                        new Reservation()
                        {
                            StartDate = new DateTime(2023, 4, 22),
                            EndDate = new DateTime(2023, 4, 25),
                            Price = 15M,
                            IsPaid = false
                        }
                    }
                },
            };

            return users;
        }
    }
}
