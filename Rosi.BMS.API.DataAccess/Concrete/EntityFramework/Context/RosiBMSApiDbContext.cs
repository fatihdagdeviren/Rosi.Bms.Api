using Microsoft.EntityFrameworkCore;
using Rosi.BMS.API.Core.Entities.Concrete;
using Rosi.BMS.API.Entities.Concrete;

namespace Rosi.BMS.API.DataAccess.Concrete.EntityFramework.Context
{
    public class RosiBMSApiDbContext : DbContext
    {
        public RosiBMSApiDbContext()
        {
        }

        public RosiBMSApiDbContext(DbContextOptions<RosiBMSApiDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(@"server=localhost;database=rosibmsapi;user=root;password=root");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<Zone> Zones { get; set; }
    }
}