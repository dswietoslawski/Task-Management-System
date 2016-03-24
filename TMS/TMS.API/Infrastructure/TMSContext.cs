using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using TMS.API.Models;

namespace TMS.API.Infrastructure {
    public class TmsContext : IdentityDbContext<TmsUser> {
        private IConfigurationRoot _config;

        public TmsContext(IConfigurationRoot config) {
            _config = config;
        }

        public DbSet<UserTask> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(_config["TmsDb:ConnectionString"]);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            OnCreating(builder.Entity<UserTask>());
            base.OnModelCreating(builder);
        }

        private void OnCreating(EntityTypeBuilder<UserTask> bldr) {

        }
    }
}