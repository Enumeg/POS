using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlClient;
using Microsoft.AspNet.Identity.EntityFramework;
using POS.Domain.Entities;

namespace POS.Domain.Infrastructure
{
    public class PosContext : IdentityDbContext<ApplicationUser> 
    {
        public PosContext()
            : base("pos", throwIfV1Schema: false)
        {
            Configuration.LazyLoadingEnabled = false;
        }
        public static PosContext CreateContext(int tenantId)
        {            
            var context = new PosContext();
            if (tenantId == 0) return context;
            context.Database.Connection.Open();
            context.Database.ExecuteSqlCommand($"set CONTEXT_INFO {tenantId}");
            return context;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Point>().HasMany(p => p.InTransfares).WithRequired(t => t.ToPoint);
            modelBuilder.Entity<Point>().HasMany(p => p.OutTransfares).WithRequired(t => t.FromPoint);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }               
        public virtual DbSet<Shift> Shifts { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<Machine> Machines { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<ProductProperty> ProductProperties { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerInstallment> CustomerInstallments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Installment> Installments { get; set; }
        public virtual DbSet<Point> Points { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }
        public virtual DbSet<PurchaseDetail> PurchasesDetails { get; set; }
        public virtual DbSet<PurchaseBack> PurchasesBack { get; set; }
        public virtual DbSet<PurchaseBackDetail> PurchaseBackDetails { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<SaleBack> SaleBacks { get; set; }   
        public virtual DbSet<SaleDetail> SaleDetails { get; set; }
        public virtual DbSet<SaleBackDetail> SaleBackDetails { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<SupplierInstallment> SupplierInstallments { get; set; }
        public virtual DbSet<Transfer> Transfares { get; set; }
        public virtual DbSet<TransferDetail> TransfareDetails { get; set; }   
        public virtual DbSet<Damaged> Damaged { get; set; }
    }
}
