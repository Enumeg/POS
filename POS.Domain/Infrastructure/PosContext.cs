using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using POS.Domain.Entities;

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
            modelBuilder.Entity<Point>().HasMany(p => p.InTransfares).WithRequired(t => t.ToPoint);
            modelBuilder.Entity<Point>().HasMany(p => p.OutTransfares).WithRequired(t => t.FromPoint);
        }
        public virtual void SetStatus(object entity, EntityState state)
        {
            Entry(entity).State = state;
        }
        public virtual DbSet<Shift> Shifts { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<Machine> Machines { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<ProductProperty> ProductProperties { get; set; }


    }
}
