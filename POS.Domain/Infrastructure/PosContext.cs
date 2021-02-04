using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
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
            var context = new PosContext { TenantId = tenantId };
            if (tenantId == 0) return context;
            context.Database.Connection.Open();
            context.Database.ExecuteSqlCommand($"set CONTEXT_INFO {tenantId}");
            return context;
        }
        public void SetTenantId<T>(T entity) where T : class
        {
            var property = typeof(T).GetProperty("TenantId");
            if (property != null)
                property.SetValue(entity, TenantId);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Point>().HasMany(p => p.InTransfares).WithRequired(t => t.ToPoint);
            modelBuilder.Entity<Point>().HasMany(p => p.OutTransfares).WithRequired(t => t.FromPoint);
            modelBuilder.Entity<Transaction>().HasOptional(s => s.BankTransaction).WithOptionalDependent(d => d.Transaction).Map(x => x.MapKey("BankTransactionId"));
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public int TenantId { get; set; }
        public virtual DbSet<Shift> Shifts { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<Machine> Machines { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<ProductProperty> ProductProperties { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<TransactionDetail> TransactionDetails { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Installment> Installments { get; set; }
        public virtual DbSet<Point> Points { get; set; }
        public virtual DbSet<Transfer> Transfares { get; set; }
        public virtual DbSet<TransferDetail> TransfareDetails { get; set; }
        public virtual DbSet<Damaged> Damaged { get; set; }
        public virtual DbSet<BarCode> BarCodes { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<BankAccount> BankAccounts { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<Income> Incomes { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Cheque> Cheques { get; set; }
        public virtual DbSet<Expense> Expenses { get; set; }
    }
}
