using Microsoft.EntityFrameworkCore;
using ReservationAPI.Domain;

namespace ReservationAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<User>()
                .Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<User>()
                .Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Role>()
                .Property(x => x.Name)
                .IsRequired();
        }
    }
}
