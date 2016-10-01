using Microsoft.AspNet.Identity.EntityFramework;
using POS.Domain.Entities;
using System.Data.Entity;
namespace POS.Domain.Infrastructure
{

    public class PosContext : IdentityDbContext<ApplicationUser>
    {
        public PosContext()
            : base("con", throwIfV1Schema: false)
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public virtual void SetStatus(object entity, EntityState state)
        {
            Entry(entity).State = state;
        }
        public virtual DbSet<Shift> Shifts { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
    }
}
