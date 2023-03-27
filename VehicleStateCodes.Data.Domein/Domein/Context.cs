using Microsoft.EntityFrameworkCore;
using VehicleStateCodes.Data.Domein.Data;
using VehicleStateCodes.Data.Domein.Domein.EntityModelBuilders;

namespace VehicleStateCodes.Data.Domein.Domein
{
    public class Context : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<UserPasswordHistory> UserPasswordHistory { get; set; }
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.ApplyConfiguration(new UserMB());
            modelBuilder.ApplyConfiguration(new UserPasswordHistoryMB());
            modelBuilder.ApplyConfiguration(new StateNumberMB());
            modelBuilder.ApplyConfiguration(new StateNumberOrderMB());
            modelBuilder.ApplyConfiguration(new StateNumberReservationMB());

        }
    }
}
